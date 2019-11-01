using System;
using System.Collections.Generic;
using System.Text;

namespace Console.CommandHandler
{
    public class Command : ICommand
    {
        public Command(string commandString)
        {
            if (commandString.StartsWith("--"))
                LongName = commandString.Replace("--", "");
            else
                ShortName = commandString.Trim('-');
        }

        public string ShortName { get; set; }

        public string LongName { get; set; }
    }
}
