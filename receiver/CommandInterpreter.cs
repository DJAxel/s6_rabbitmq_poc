using System;

namespace receiver
{
    public sealed class CommandInterpreter
    {
        private static CommandInterpreter instance = null;
        private UserRepository _userRepository = null;

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

        CommandInterpreter()
        {
            this._userRepository = UserRepository.Instance;
        }

        internal String execute(string command)
        {
            command = command.Trim();
            string[] words = command.Split(':');
            if(words.Length >= 2) {
                words[0] = words[0].Trim().ToLower();
                if(words[0] == "add") {
                    return this._userRepository.add(words[1]);
                }
                if(words[0] == "delete") {
                    return this._userRepository.delete(words[1]);
                }
            }
            return string.Format("Invalid command received: {0}", command);
        }
    }
}