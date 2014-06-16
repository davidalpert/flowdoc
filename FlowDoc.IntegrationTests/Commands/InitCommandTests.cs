using System;
using System.IO;
using System.Linq;
using ApprovalTests;
using FlowDoc.IntegrationTests.Helpers;
using NUnit.Framework;
using ObjectApproval;

namespace FlowDoc.IntegrationTests.Commands
{
    [TestFixture]
    public class InitCommandTests
    {
        [Test]
        public void Execute()
        {
            var currentDir = Environment.CurrentDirectory;
            var flowExe = Path.Combine(currentDir, "flow.exe");
            Assert.IsTrue(File.Exists(flowExe));

            using (var targetPath = new DisposableTempFolder())
            {
                var args = new string[] {"init", targetPath};

                CLI.Program.Main(args);

                var allFiles = Directory.EnumerateFiles(targetPath, "*.*", SearchOption.AllDirectories)
                                        .Select(f => f.Replace(targetPath, "~"))
                                        .ToList();

                ObjectApprover.VerifyWithJson(allFiles);
            }
        }
    }
}