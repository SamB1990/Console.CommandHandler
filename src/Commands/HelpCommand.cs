using System;
using System.Linq;

namespace Console.CommandHandler.Commands
{
    /// <summary>
    /// The default help command to be used by the command handler <see cref="CommandHandler"/>
    /// </summary>
    internal class HelpCommand : CommandAttribute, IEnsuredCommandArguments
    {

        public HelpCommand() : 
            base("h", "help", 
                "list help for the current handler ('-h h' for more info)", 
                "[ 'The command name you wish to view help on.' ]")
        {
        }

        /// <summary>
        /// executes the help command for the current command handler <see cref="CommandHandler"/>
        /// </summary>
        public static void WriteHelp(string arg)
        {
            if (arg == string.Empty)
            {
                System.Console.WriteLine("{0,-20} {1,-20} {2,10}", "Short Name", "Long Name", "Description");

                ConsoleX.WriteSplitter("-", 100);
                
                foreach (var commandProp in Handler.GetAllCommandsProperties())
                {
                    System.Console.WriteLine("-{0,-20} -{1,-20} {2,10}", commandProp.Command.ShortName, commandProp.Command.LongName,
                        commandProp.Command.Description);
                }

                ConsoleX.WriteSplitter("-", 100);
            }
            else
            {
                WriteCommandHelp(arg);
            }
        }

        public static void WriteCommandHelp(string arg)
        {
            var commandProps = Handler.GetAllCommandsProperties().Where(c => c.IsCommand(arg));
            if (!commandProps.Any())
            {
                ConsoleX.WriteError(new Exception($"command does not exist"), ConsoleX.LogLevel.basic);
                return;
            }
            foreach (var commandProp in commandProps)
            {
                ConsoleX.WriteLine($"short name : { commandProp.Command.ShortName }");
                ConsoleX.WriteLine($"long name : { commandProp.Command.LongName }");
                ConsoleX.WriteLine($"description : { commandProp.Command.Description } ");
                if (!commandProp.HasArguments())
                {
                    ConsoleX.WriteLine($"no arguments");
                    continue;
                }
                ConsoleX.WriteLines(commandProp.GetArgDescriptions());
            }
        }

    }
}
