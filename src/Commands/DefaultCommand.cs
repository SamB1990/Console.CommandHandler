
namespace Console.CommandHandler.Commands
{
    public class DefaultCommand : CommandAttribute
    {
        public DefaultCommand(string shortName, string longName, string description, string argDescriptionArray = "") 
            : base(shortName, longName, description, argDescriptionArray)
        {
        }
    }
}
