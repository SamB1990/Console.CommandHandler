using System;
using System.Collections.Generic;
using System.Text;

namespace Console.CommandHandler
{
    public interface IFullCommand : ICommand
    {
        string Description { get; }
        string ArgDescriptionArray { get; }
    }

}
