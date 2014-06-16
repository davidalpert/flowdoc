using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using ManyConsole;

namespace FlowDoc.CLI.Commands
{
    public class InitCommand : ConsoleCommand
    {
        private readonly IFileSystem fileSystem;

        public InitCommand(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;

            IsCommand("init", "initializes a FlowDoc source tree");

            HasAdditionalArguments(1, "[targetDirectory]");

            SkipsCommandSummaryBeforeRunning();
        }

        public override int Run(string[] remainingArguments)
        {
            try
            {
                var targetPath = new DirectoryInfo(remainingArguments[0]);

                if (targetPath.Exists == false)
                {
                    targetPath.Create();
                }

                WriteConfig(targetPath);
                WriteTOC(targetPath);
            }
            catch (Exception ex)
            {
                throw new ConsoleHelpAsException(ex.Message);
            }

            return 0;
        }

        private void WriteConfig(DirectoryInfo targetDir)
        {
            var configPath = Path.Combine(targetDir.FullName, ".config");
            fileSystem.File.WriteAllText(configPath, "# Configure flowdoc settings here");
        }

        private void WriteTOC(DirectoryInfo targetDir)
        {
            var path = Path.Combine(targetDir.FullName, @"toc.md");
            fileSystem.File.WriteAllText(path, @"<!---
Welcome to FlowDoc, a markdown-based documentation system for describing software workflows.
--->
");
        }
    }
}
