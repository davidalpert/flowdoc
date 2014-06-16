using System;
using System.IO;
using System.Linq;

namespace FlowDoc.IntegrationTests.Helpers
{
    public class DisposableTempFolder : IDisposable
    {
        protected DirectoryInfo BackingDirectory { get; set; }

        public DisposableTempFolder(string path = null)
        {
            var targetPath = path ?? TargetPath();
            this.BackingDirectory = new DirectoryInfo(targetPath);
            if (this.BackingDirectory.Exists == false)
            {
                this.BackingDirectory.Create();
            }
        }

        private static string TargetPath()
        {
            var targetPath = Path.GetTempFileName();
            File.Delete(targetPath);
            return targetPath;
        }

        public void Dispose()
        {
            if (this.BackingDirectory != null)
            {
                this.BackingDirectory.Delete(true);
            }
        }

        public static implicit operator string(DisposableTempFolder folder)
        {
            return folder.BackingDirectory.FullName;
        }

        public static implicit operator DisposableTempFolder(string path)
        {
            return new DisposableTempFolder(path);
        }
    }
}
