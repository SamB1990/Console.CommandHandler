using System.Forest;
using System.Forest.Services;

namespace Console.CommandHandler
{
    public static partial class Handler
    {
        private static CommandTree commandtree;

        internal static ITreeService treeService { get; } = TreeFactory.InitializeTreeService();

        public static CommandTree GetTree()
        {
            if (commandtree != null) return commandtree;

            commandtree = new CommandTree();
            treeService.AddLeaf(commandtree, typeof(DefaultCommandContainer).FullName, new DefaultCommandContainer());
            return commandtree;
        }
    }
}
