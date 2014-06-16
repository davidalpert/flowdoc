using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using ManyConsole;

namespace FlowDoc.CLI.Commands
{
    public class BuildCommand : FlowDocCommand
    {
        public BuildCommand(IFileSystem fileSystem)
            : base(fileSystem, "build", "builds output from the given source tree")
        {
            HasAdditionalArguments(1, " the source tree");
        }

        public override void Execute(string[] remainingArguments)
        {
            var sourceDir = FileSystem.DirectoryInfo.FromDirectoryName(remainingArguments[0]);
            if (!IsFlowDocSource(sourceDir))
            {
                FailWith("'{0}' is not a FlowDoc source root.", sourceDir.FullName);
            }
        }

        private bool IsFlowDocSource(DirectoryInfoBase sourceDir)
        {
            return sourceDir.GetFiles(".config").Any() && sourceDir.GetFiles("toc.md").Any();
        }

        private void FailWith(string msg, params object[] args)
        {
            throw new ConsoleHelpAsException(String.Format(msg, args));
        }
    }
}
