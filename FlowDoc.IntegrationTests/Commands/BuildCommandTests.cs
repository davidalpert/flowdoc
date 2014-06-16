using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FlowDoc.IntegrationTests.Helpers;
using NUnit.Framework;
using ObjectApproval;

namespace FlowDoc.IntegrationTests.Commands
{
    [TestFixture]
    public class BuildCommandTests
    {
        [Test]
        public void Execute_against_an_empty_directory()
        {
            var currentDir = Environment.CurrentDirectory;
            var flowExe = Path.Combine(currentDir, "flow.exe");
            Assert.IsTrue(File.Exists(flowExe));

            using (var targetPath = new DisposableTempFolder())
            using (var console = new RedirectedConsole())
            {
                var args = new string[] {"build", targetPath};

                var result = CLI.Program.Main(args);

                Debug.WriteLine(console.Output);

                Assert.AreEqual(-1, result);
            }
        }

        [Test]
        public void Execute_against_a_source_directory()
        {
            var currentDir = Environment.CurrentDirectory;
            var flowExe = Path.Combine(currentDir, "flow.exe");
            Assert.IsTrue(File.Exists(flowExe));

            using (var targetPath = new DisposableTempFolder())
            using (var console = new RedirectedConsole())
            {
                CLI.Program.Main("init", targetPath);

                var result = CLI.Program.Main("build", targetPath);

                Debug.WriteLine(console.Output);

                Assert.AreEqual(0, result);

                TestHelpers.VerifyFileNames(targetPath);
            }
        }
    }
}