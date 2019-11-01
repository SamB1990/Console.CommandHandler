using System;
using Console.CommandHandler.Commands;

namespace Console.CommandHandler
{
    public sealed class DefaultCommandContainer : ICommandContainer
    {
        /// <summary>
        /// The default close command used to close the command handler <see cref="CommandHandler"/>
        /// </summary>
        [CloseCommand]
        internal Action CloseListener => CloseCommand.ExecuteClose;

        ///// <summary>
        ///// The default force close command used to close the command handler <see cref="CommandHandler"/> without informing the console
        ///// </summary>
        //[Command("", "forceclose", "force closes the current command handler")]
        //internal Action ForceCloseListener { get; set; }

        /// <summary>
        /// The default Help command 
        /// </summary>
        [HelpCommand]
        internal Action<string> Help => HelpCommand.WriteHelp;

    }
}
