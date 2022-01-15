using System.Globalization;
using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ConsoleTables;
using UserService.domain;

namespace UserService
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandInterpreter commandInterpreter = CommandInterpreter.Instance;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "users_topic", type: ExchangeType.Topic);

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName, exchange: "users_topic", routingKey: "users.*");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    Console.WriteLine(commandInterpreter.execute(message));
                    Console.WriteLine();
                    printCurrentDatabaseTable();
                };
                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        static void printCurrentDatabaseTable()
        {
            Console.WriteLine("This is what the current database looks like:");
            Console.WriteLine();

            UserRepository userRepository = UserRepository.Instance;
            var table = new ConsoleTable("name", "age", "city");
            User[] users = userRepository.getAll();

            foreach (User user in users)
            {
                table.AddRow(user.Name, user.Age, user.City);
            }

            table.Write();
            Console.WriteLine();
        }
    }
}
