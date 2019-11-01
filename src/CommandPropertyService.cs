using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Console.CommandHandler
{
    internal static class CommandPropertyService
    {
        internal static IEnumerable<string> GetArgDescriptions(this CommandProperty cmdProperty)
        {
            string[] desc = null;
            try
            {
                desc = JsonConvert.DeserializeObject<string[]>(cmdProperty.ArgDescriptionArray);
            }
            catch (Exception)
            {
                ConsoleX.WriteError(new ArgumentException($"The commands Argument Description of \"{ cmdProperty.ArgDescriptionArray }\" is not of string format containing an array or strings"));
            }

            for (var i = 0; i < cmdProperty._propertyInfo.PropertyType.GetGenericArguments().Length; i++)
            {
                var argument = cmdProperty._propertyInfo.PropertyType.GetGenericArguments()[i];

                yield return cmdProperty._propertyInfo.PropertyType.Name.Contains("Func") && i != 0
                    ? $"return[{i}]({argument.Name}): { desc?.ElementAtOrDefault(i) ?? "No return description" }"
                    : $"argument[{i}]({argument.Name}): { desc?.ElementAtOrDefault(i) ?? "No argument description" }";
            }
        }


        internal static bool HasArguments(this CommandProperty cmdProperty)
        {
            return cmdProperty._propertyInfo.PropertyType.IsGenericType;
        }

        internal static bool IsCommand(this CommandProperty cmdProperty, string commandName)
        {
            return (cmdProperty.ShortName == commandName.Trim()) ||
                   (cmdProperty.LongName == commandName.Trim());
        }

        internal static bool IsCommand(this CommandProperty cmdProperty, ICommand command)
        {
            return (cmdProperty.ShortName == command.ShortName && command.ShortName != null) ||
                   (cmdProperty.LongName == command.LongName && command.LongName != null);
        }


        public static object Invoke(this CommandProperty cmdProperty, IEnumerable<object> arguments)
        {
            try
            {
                var cmdMethod = cmdProperty._propertyInfo.PropertyType.GetMethod("Invoke");

                var cmdObj = cmdProperty._propertyInfo.GetValue(cmdProperty._commandContainer);

                return cmdMethod.Invoke(cmdObj, cmdProperty.CheckArguments(cmdMethod, arguments ?? new object[] { }).ToArray());
            }
            catch (TargetParameterCountException)
            {
                ConsoleX.WriteError(new TargetParameterCountException($"The argument count for the command '{ cmdProperty.LongName ?? cmdProperty.ShortName  }' does not match. " +
                                                      $"Single arguments should be passed in as a single string and multiple should be passed in as an array. " +
                                                      $"eg [ arg1, arg2 , arg3 ]"), ConsoleX.LogLevel.basic);
                return null;
            }
            catch (Exception e)
            {
                ConsoleX.WriteError(new MethodAccessException($"There has been an unexpected error with the command '{ cmdProperty.LongName ?? cmdProperty.ShortName  }'", e), ConsoleX.LogLevel.basic);
                return null;
            }
        }

        private static IEnumerable<object> CheckArguments(this CommandProperty cmdProperty, MemberInfo mInfo, IEnumerable<object> arguments)
        {
            var actionType = mInfo.DeclaringType;

            return (cmdProperty.Command is IEnsuredCommandArguments) && !DoArgumentsMatch(actionType, arguments)
                ? EnsureArguments(actionType, arguments)
                : arguments;
        }

        private static bool DoArgumentsMatch(Type actionType, IEnumerable<object> arguments)
        {
            return actionType.IsGenericType && actionType.GetGenericArguments().Length == arguments.Count();
        }

        private static IEnumerable<object> EnsureArguments(Type actionType, IEnumerable<object> arguments)
        {
            for (var i = 0; i < actionType.GetGenericArguments().Length; i++)
            {
                yield return arguments.ElementAtOrDefault(i) ?? "";
            }
        }

    }
}
