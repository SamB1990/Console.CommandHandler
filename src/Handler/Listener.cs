using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Console.CommandHandler
{
    public static partial class Handler
    {
        public static bool CloseCurrentListener = false;
        
        public static void Listen()
        {
            var tree = GetTree();
            ConsoleX.WriteInfoLine($"listening for calls in the command named branch ({ CurrentCommandBranchName }) (use -h or -help for a list of commands)", ConsoleX.LogLevel.basic);
            do
            {
                if (CloseCurrentListener)
                {
                    break;
                }

                var commands = ConsoleX.ReadCommands().ToArray();
                ExecuteAll(commands);

                continue;
            } while (true);
            CloseCurrentListener = false;
            Handler.BranchNavigation.StepUp(tree);
        }
    }
}
