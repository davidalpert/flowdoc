using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;

namespace FlowDoc.CLI.Commands
{
    public class InitCommand : ICommand<InitOptions>
    {
        private readonly IFileSystem fileSystem;

        public InitCommand(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public void Execute()
        {
            var targetPath = Options.TargetDirectory;


            WriteConfig(targetPath);
            WriteTOC(targetPath);
        }

        private void WriteConfig(string targetPath)
        {
            var configPath = Path.Combine(targetPath, ".config");
            fileSystem.File.WriteAllText(configPath, "# Configure flowdoc settings here");
        }

        private void WriteTOC(string targetPath)
        {
            var path = Path.Combine(targetPath, @"toc.md");
            fileSystem.File.WriteAllText(path, @"<!---
Welcome to FlowDoc, a markdown-based documentation system for describing software workflows.
--->
");
        }

        public InitOptions Options { get; set; }
    }

    public class InitOptions
    {
        public string TargetDirectory { get; set; }
    }

    public interface ICommand<TOptions>
    {
        void Execute();
        TOptions Options { get; set; }
    }
}
