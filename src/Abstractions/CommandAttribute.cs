using System;

namespace Console.CommandHandler
{
    /// <summary>
    /// Attribute used to mark and describe commands 
    /// </summary>
    public abstract class CommandAttribute : Attribute, IFullCommand
    {
        /// <summary>
        /// Command Attribute constructor
        /// </summary>
        /// <param name="shortName">short command name</param>
        /// <param name="longName">long command name</param>
        /// <param name="description">description of the command</param>
        /// <param name="argDescription">array string for descriptions of all arguments first will be return if returned
        /// <example>{"return description", "argument 1 description" , "argument 2 description"}</example></param>
        public CommandAttribute(string shortName, string longName, string description, string argDescriptionArray = "")
        {
            ShortName = shortName;
            LongName = longName;
            Description = description;
            ArgDescriptionArray = argDescriptionArray;
        }

        /// <summary>
        /// short command name
        /// </summary>
        public string ShortName { get; protected set; }

        /// <summary>
        /// long command name
        /// </summary>
        public string LongName { get; protected set; }

        /// <summary>
        /// description of the command
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// array string for all arguments first will be return if returned
        /// <example>{"return description", "argument 1 description" , "argument 2 description"}</example>
        /// </summary>
        public string ArgDescriptionArray { get; }
    }
}
