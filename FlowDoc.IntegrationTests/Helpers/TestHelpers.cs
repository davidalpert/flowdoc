using System.IO;
using System.Linq;
using ObjectApproval;

namespace FlowDoc.IntegrationTests.Helpers
{
    public static class TestHelpers
    {
        public static void VerifyFileNames(DisposableTempFolder targetPath)
        {
            var allFiles = Directory.EnumerateFiles(targetPath, "*.*", SearchOption.AllDirectories)
                                    .Select(f => f.Replace(targetPath, "~"))
                                    .ToList();

            ObjectApprover.VerifyWithJson(allFiles);
        }
    }
}