using System.IO;

namespace Parser.BL.Data.Interfaces
{
    public interface IFileService
    {
        public void DeleteFile(FileInfo file);

        public void CreateDirectory(string fullPath);
    }
}
