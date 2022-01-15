using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace sender
{
    public sealed class CommandInterpreter
    {
        CommandInterpreter()
        {
            this._messageSender = MessageSender.Instance;
        }
        private static CommandInterpreter instance = null;

        private MessageSender _messageSender = null;

        public static CommandInterpreter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CommandInterpreter();
                }
                return instance;
            }
        }

        internal bool execute(string command)
        {
            command = command.Trim();
            if(command.ToLower() == "exit") {
                return true;
            }

            string[] words = command.Split(':');
            if(words.Length < 2) {
                return false;
            }
            words[0] = words[0].Trim().ToLower();
            words[1] = words[1].Trim();
            if(words[0] == "add") {
                this._messageSender.send("add:" + words[1], "add");
            }
            if(words[0] == "delete") {
                this._messageSender.send("delete:" + words[1], "delete");
            }
            return false;
        }
    }
}