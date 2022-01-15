using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ConsoleTables;
using UserSecretService.domain;

namespace UserSecretService
{
    class Program
    {
        static void Main(string[] args)
        {
            SecretRepository secretRepository = SecretRepository.Instance;
            secretRepository.Seed();
            printCurrentDatabaseTable();

            CommandInterpreter commandInterpreter = CommandInterpreter.Instance;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "users_topic", type: ExchangeType.Topic);

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName, exchange: "users_topic", routingKey: "users.delete");

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

            SecretRepository secretRepository = SecretRepository.Instance;
            var table = new ConsoleTable("User's name", "secret");
            UserSecret[] secrets = secretRepository.getAll();

            foreach (UserSecret secret in secrets)
            {
                table.AddRow(secret.User.Name, secret.Secret);
            }

            table.Write();
            Console.WriteLine();
        }
    }
}
