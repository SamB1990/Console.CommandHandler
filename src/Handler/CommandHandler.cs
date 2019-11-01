using System;
using System.Collections.Generic;
using System.Forest;
using System.Forest.Services;
using System.Linq;
using System.Reflection;


namespace Console.CommandHandler
{
    public static partial class Handler
    {

        internal static IBranchNavigationService BranchNavigation { get; } = TreeFactory.InitializeBranchNavigation();
        
        public static void AddCommands(IEnumerable<ICommandContainer> commandContainers)
        {
            treeService.AddLeaves(GetTree(), commandContainers.ToDictionary(c => c.GetType().FullName, c => (object)c));
        }

        public static void StepIn(string commandBranchName)
        {
            treeService.AddBranch<Branch>(GetTree(), commandBranchName,true);

            treeService.AddLeaf(GetTree(), typeof(DefaultCommandContainer).FullName, new DefaultCommandContainer());
        }
        
        internal static IEnumerable<CommandProperty> GetAllCommandsProperties()
        {
            var props = new List<CommandProperty>();
            foreach (var container in treeService.GetNearbyLeaves(GetTree()))
            {
                props.AddRange(container.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    .Where(p => Attribute.IsDefined(p, typeof(CommandAttribute))).Select(p => new CommandProperty(p, (ICommandContainer)container)));

            }
            return props;
        }

        public static string CurrentCommandBranchName => GetTree().CurrentBranch?.Name ?? "Base App Commands";
        
    }
}
