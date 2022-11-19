using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Parser.BL.Data.Interfaces
{
    public interface IFileService
    {
        public void DeleteFile(FileInfo file);

        public void CreateDirectory(string fullPath);
    }
}
