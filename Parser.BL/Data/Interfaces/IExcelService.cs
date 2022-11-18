using Parser.BL.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Parser.BL.Data.Interfaces
{
    public interface IExcelService
    {
        void AddWorksheet(FileInfo file, string worksheetName, List<ProductInfo> products);

        void DeleteOldFile(FileInfo file);

        void CreateDirectory(string fullPath);
    }
}
