using Parser.BL.Data.Interfaces;
using System.IO;
using System.Linq;

namespace Parser.BL.Data.Services
{
    public class FileService : IFileService
    {
        public void DeleteFile(FileInfo file)
        {
            if (file.Exists)
            {
                file.Delete();
            }
        }

        public void CreateDirectory(string fullPath)
        {
            var dir = fullPath.Split("/").ToList();
            dir.RemoveAt(dir.Count - 1);

            string fulldir = "";
            foreach (var part in dir)
            {
                fulldir += (string.IsNullOrEmpty(fulldir) ? "" : "\\") + part;

                if (!Directory.Exists(fulldir))
                {
                    Directory.CreateDirectory(fulldir);
                }
            }
        }
    }
}
