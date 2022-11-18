using OfficeOpenXml;
using Parser.BL.Data.Interfaces;
using Parser.BL.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Parser.BL.Data.Services
{
    public class ExcelService : IExcelService
    {

        public void AddWorksheet(FileInfo file, string worksheetName, List<ProductInfo> products)
        {
            using var package = new ExcelPackage(file);

            if (package.Workbook.Worksheets.Any(x => x.Name == worksheetName)) return;

            var index = 2;
            var ws = CreateWorksheet(package, worksheetName);

            foreach (var product in products)
            {
                ws.Cells["A" + index].Value = product.Title;
                ws.Cells["B" + index].Value = product.Brand;
                ws.Cells["C" + index].Value = product.Id;
                ws.Cells["D" + index].Value = product.Feedbacks;
                ws.Cells["E" + index].Value = product.Price;

                index++;
            }

            package.Save();
        }

        public void DeleteOldFile(FileInfo file)
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

        private ExcelWorksheet CreateWorksheet(ExcelPackage package, string worksheetName)
        {
            var ws = package.Workbook.Worksheets.Add(worksheetName);

            ws.Cells["A1"].Value = "Title";
            ws.Cells["B1"].Value = "Brand";
            ws.Cells["C1"].Value = "Id";
            ws.Cells["D1"].Value = "Feedbacks";
            ws.Cells["E1"].Value = "Price";

            return ws;
        }
    }
}
