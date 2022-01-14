using System;

namespace sender
{
    class Program
    {
        

        static void Main(string[] args)
        {
            CommandInterpreter commandInterpreter = CommandInterpreter.Instance;

            Console.WriteLine("Welcome to my user frontend!");
            while(true) {
                Console.WriteLine("What can I do for you? 'add:<name>' or 'delete:<name>' or 'exit'");
                String command = Console.ReadLine();
                if(commandInterpreter.execute(command) == true) {
                    break;
                }
            }
        }
    }
}
