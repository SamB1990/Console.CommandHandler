using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Console.CommandHandler")]
namespace Console.CommandHandler
{
    internal class CommandProperty
    {
        public CommandProperty(PropertyInfo propertyInfo, ICommandContainer commandContainer)
        {
            _commandContainer = commandContainer;
            _propertyInfo = propertyInfo;
            Command = propertyInfo.GetCustomAttribute<CommandAttribute>();
        }

        internal readonly ICommandContainer _commandContainer;

        internal readonly PropertyInfo _propertyInfo;

        internal string ShortName => Command.ShortName;

        internal string LongName => Command.LongName;

        internal string ArgDescriptionArray => Command.ArgDescriptionArray;

        internal IFullCommand Command { get; }

    }
}
