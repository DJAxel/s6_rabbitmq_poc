using System;
using System.Text;
using RabbitMQ.Client;

namespace sender {
    public sealed class MessageSender {
        private static MessageSender instance;
        private ConnectionFactory _factory;

        MessageSender() {
            this._factory = new ConnectionFactory() { HostName = "localhost" };
        }
        
        public static MessageSender Instance {
            get {
                if(instance == null) {
                    instance = new MessageSender();
                }
                return instance;
            }
        }

        internal void send(string message)
        {
            using (var connection = this._factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "users", type: ExchangeType.Fanout);
                // channel.QueueDeclare(queue: "users", durable: true, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "users", routingKey: "", basicProperties: null, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}