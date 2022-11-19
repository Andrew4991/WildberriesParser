using System.IO;

namespace Parser.BL.Data.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Delete file by file info
        /// </summary>
        /// <param name="file"></param>
        public void DeleteFile(FileInfo file);

        /// <summary>
        /// Check and create directories
        /// </summary>
        /// <param name="fullPath"></param>
        public void CreateDirectory(string fullPath);
    }
}
