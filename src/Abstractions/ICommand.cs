
namespace Console.CommandHandler
{
    public interface ICommand
    {
        string ShortName { get; }

        string LongName { get; }
    }
}
