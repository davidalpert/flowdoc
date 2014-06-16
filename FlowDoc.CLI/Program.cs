using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using System.Text;
using FlowDoc.CLI.Commands;
using ManyConsole;

namespace FlowDoc.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var commands = FindCommands();

            ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }

        public static IEnumerable<ConsoleCommand> FindCommands()
        {
            var assembly = typeof(InitCommand).Assembly;

            var commandTypes = assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ConsoleCommand)))
                .Where(t => !t.IsAbstract)
                .OrderBy(t => t.FullName);

            List<ConsoleCommand> result = new List<ConsoleCommand>();

            var fileSystem = new FileSystem();

            foreach(var commandType in commandTypes)
            {
                var constructor = commandType.GetConstructor(new [] {typeof (IFileSystem)});

                if (constructor == null)
                    continue;

                result.Add((ConsoleCommand)constructor.Invoke(new object[] { fileSystem }));
            }

            return result;
        }
    }
}
