using System.IO;
using System.Linq;
using FlowDoc.IntegrationTests.Helpers;
using NUnit.Framework;

namespace FlowDoc.IntegrationTests.Commands
{
    [TestFixture]
    public class InitCommandTests 
    {
        [Test]
        public void Execute()
        {
            using (var targetPath = new DisposableTempFolder())
            {
                var args = new string[] {"init", targetPath};

                CLI.Program.Main(args);

                TestHelpers.VerifyFileNames(targetPath);
            }
        }

        [Test]
        public void Execute_against_a_folder_that_does_not_exist()
        {
            using (var rootPath = new DisposableTempFolder())
            {
                var targetPath = Path.Combine(rootPath, "foo");

                var args = new[] {"init", targetPath};

                CLI.Program.Main(args);

                TestHelpers.VerifyFileNames(targetPath);
            }
        }
    }
}