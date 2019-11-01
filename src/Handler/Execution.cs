using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Console.CommandHandler
{
    public static partial class Handler
    {
        public static void Execute(string commandString, IEnumerable<string> Arguments = null)
        {
            var command = new Command(commandString);

            ExecuteAll(new[] { new ExecutableCommand() { Arguments = Arguments, Command = command } });
        }

        public static void ExecuteAll(IEnumerable<IExecutableCommand> exeCommands)
        {
            foreach (var exeCommand in exeCommands)
            {
                var matchedCommands = GetAllCommandsProperties().Where(c => c.IsCommand(exeCommand.Command));
                if (!matchedCommands.Any())
                {
                    ConsoleX.WriteWarningLine($"The command { exeCommand.Command.ShortName ?? exeCommand.Command.LongName } cannot be found", ConsoleX.LogLevel.basic);
                    continue;
                }
                foreach (var cmd in matchedCommands)
                {
                    var result = cmd.Invoke(exeCommand.Arguments?.ToArray());
                    switch (result)
                    {
                        case null:
                            continue;
                        case string str:
                        {
                            ConsoleX.WriteLine(str);
                            break;
                        }
                        case IEnumerable enumerable:
                        {
                            foreach (var s in enumerable.Cast<object>().Select(i => i.ToString()))
                            {
                                ConsoleX.WriteLine(s);
                            }

                            break;
                        }
                        default:
                            ConsoleX.WriteLine(result.ToString());
                            break;
                    }
                }
            }
        }


    }
}
