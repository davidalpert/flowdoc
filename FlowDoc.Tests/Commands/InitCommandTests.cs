using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using ApprovalTests;
using FlowDoc.CLI.Commands;
using NUnit.Framework;
using ObjectApproval;

namespace FlowDoc.Tests.Commands
{
    [TestFixture]
    public class InitCommandTests
    {
        [Test]
        public void Execute()
        {
            var targetPath = Path.Combine("c:", "dev", "mydocs");

            var fileSystem = new MockFileSystem();

            var cmd = new InitCommand(fileSystem)
                      {
                          Options = new InitOptions()
                                    {
                                        TargetDirectory = targetPath
                                    }
                      };

            cmd.Execute();

            ObjectApprover.VerifyWithJson(fileSystem.AllFiles);
        }

        [Test]
        public void Execute_creates_a_table_of_contents()
        {
            var targetPath = Path.Combine("c:", "dev", "mydocs");
            var tocPath = Path.Combine(targetPath, "toc.md");

            var fileSystem = new MockFileSystem();

            var cmd = new InitCommand(fileSystem)
                      {
                          Options = new InitOptions()
                                    {
                                        TargetDirectory = targetPath
                                    }
                      };

            cmd.Execute();

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
