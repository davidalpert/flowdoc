using System.IO;
using NUnit.Framework;

namespace FlowDoc.IntegrationTests.Helpers
{
    [TestFixture]
    public class DisposableTempFolderTests
    {
        [Test]
        public void Creates_and_disposes()
        {
            string tempFolderPath = "";

            using (var tempFolder = new DisposableTempFolder())
            {
                Assert.IsTrue(Directory.Exists(tempFolder));
                tempFolderPath = tempFolder;
            }

            Assert.IsFalse(Directory.Exists(tempFolderPath));
        }
    }
}