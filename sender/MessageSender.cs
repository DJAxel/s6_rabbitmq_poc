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

        internal void send(string message, string type)
        {
            using (var connection = this._factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "users_topic", type: ExchangeType.Topic);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "users_topic", routingKey: "users."+type, basicProperties: null, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}