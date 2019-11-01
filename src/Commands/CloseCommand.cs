using System;

namespace Console.CommandHandler.Commands
{
    /// <summary>
    /// The default close command to be used by the command handler <see cref="CommandHandler"/>
    /// </summary>
    internal class CloseCommand : CommandAttribute, IEnsuredCommandArguments
    {

        public CloseCommand() 
            : base("c", "close",
                "closes the current command handler", "")
        {
        }
        
        /// <summary>
        /// executes the close of the current command handler <see cref="CommandHandler"/>
        /// </summary>
        public static void ExecuteClose()
        {
            var response = ConsoleX.WriteCheck($"Are you sure you want to close the { Handler.CurrentCommandBranchName } command handler?");
            if (response)
                Handler.CloseCurrentListener = true;
        }
    }
}
