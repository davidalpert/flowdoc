using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace FlowDoc.CLI.Commands
{
    public class InitCommand : FlowDocCommand
    {
        public InitCommand(IFileSystem fileSystem) 
            : base(fileSystem, "init", "initializes a FlowDoc source tree")
        {
            HasAdditionalArguments(1, "[targetDirectory]");
        }

        public override void Execute(string[] remainingArguments)
        {
            var targetPath = new DirectoryInfo(remainingArguments[0]);

            if (targetPath.Exists == false)
            {
                targetPath.Create();
            }

            WriteConfig(targetPath);
            WriteTOC(targetPath);
        }

        private void WriteConfig(DirectoryInfo targetDir)
        {
            var configPath = Path.Combine(targetDir.FullName, ".config");
            FileSystem.File.WriteAllText(configPath, "# Configure flowdoc settings here");
        }

        private void WriteTOC(DirectoryInfo targetDir)
        {
            var path = Path.Combine(targetDir.FullName, @"toc.md");
            FileSystem.File.WriteAllText(path, @"<!---
Welcome to FlowDoc, a markdown-based documentation system for describing software workflows.
--->
");
        }
    }
}
