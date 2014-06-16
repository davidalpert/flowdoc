using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using ApprovalTests;
using FlowDoc.CLI.Commands;
using ManyConsole;
using NUnit.Framework;
using ObjectApproval;

namespace FlowDoc.Tests.Commands
{
    [TestFixture]
    public class InitCommandTests
    {
        private MockFileSystem RunCommandWith(params string[] args)
        {
            return RunCommandWith(null, args);
        }

        private MockFileSystem RunCommandWith(MockFileSystem fileSystem, params string[] args)
        {
            fileSystem = fileSystem ?? new MockFileSystem();
            var cmd = new InitCommand(fileSystem);
            var remaining = cmd.Options.Parse(args);
            cmd.Run(remaining.ToArray());
            return fileSystem;
        }

        [Test]
        public void Execute()
        {
            var fileSystem = RunCommandWith(Path.Combine(@"c:\", "dev", "mydocs"));

            ObjectApprover.VerifyWithJson(fileSystem.AllFiles);
        }

        [Test]
        public void Execute_creates_a_table_of_contents()
        {
            var targetPath = Path.Combine(@"c:\", "dev", "mydocs");
            var tocPath = Path.Combine(targetPath, "toc.md");

            var fileSystem = RunCommandWith(targetPath);

            Assert.IsTrue(fileSystem.File.Exists(tocPath));
            Approvals.Verify(fileSystem.File.ReadAllText(tocPath));
        }

        [Test]
        public void NewMockFileSystem()
        {
            var fileSystem = new MockFileSystem();
            ObjectApprover.VerifyWithJson(fileSystem);
        }
    }
}
