using System.Collections.Generic;

namespace Console.CommandHandler
{
    public class ExecutableCommand : IExecutableCommand
    {
        public ICommand Command { get; set; }
        public IEnumerable<string> Arguments { get; set; }
    }
}
