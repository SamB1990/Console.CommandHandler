
using System.Collections.Generic;

namespace Console.CommandHandler
{
    public interface IExecutableCommand
    {
        ICommand Command { get; }
        IEnumerable<string> Arguments { get; }
    }
}
