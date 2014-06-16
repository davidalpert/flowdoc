using System;
using System.IO.Abstractions;
using System.Linq;
using ManyConsole;

namespace FlowDoc.CLI
{
    public abstract class FlowDocCommand : ConsoleCommand
    {
        protected readonly IFileSystem FileSystem;

        protected FlowDocCommand(IFileSystem fileSystem, string command, string description)
        {
            this.FileSystem = fileSystem;

            IsCommand(command, description);

            SkipsCommandSummaryBeforeRunning();
        }

        public abstract void Execute(string[] remainingArguments);

        public override int Run(string[] remainingArguments)
        {
            try
            {
                Execute(remainingArguments);
            }
            catch (Exception ex)
            {
                throw new ConsoleHelpAsException(ex.Message);
            }

            return 0;
        }
    }
}