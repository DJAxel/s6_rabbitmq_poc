using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace sender
{
    public sealed class MessageSender
    {
        MessageSender()
        {
            this._factory = new ConnectionFactory() { HostName = "localhost" };
        }
        private static MessageSender instance = null;

        private ConnectionFactory _factory;
        private Timer _timer = null;

        public static MessageSender Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MessageSender();
                }
                return instance;
            }
        }

        internal void startSending()
        {
            Console.WriteLine("Starting timer, sending message every 1 second.");
            this._timer = new Timer(SendMessage, "Some state", TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        }

        internal void stopSending()
        {
            _timer.Change(-1, -1);
            this._timer = null;
        }

        private void SendMessage(object state) {
            using (var connection = this._factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                string datetime = DateTime.Now.ToString("HH:mm:ss");
                string message = datetime + ": Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}