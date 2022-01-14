using System;
using RabbitMQ.Client;
using System.Text;
using System.Threading;

namespace sender
{
    class Program
    {
        

        static void Main(string[] args)
        {
            MessageSender messageSender = MessageSender.Instance;
            messageSender.startSending();

            Console.WriteLine(" Press [enter] to exit.");
            Console.WriteLine("");
            Console.ReadLine();
            messageSender.stopSending();
        }
    }
}
