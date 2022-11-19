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
        public void AddWorksheet(FileInfo file, string worksheetName, IEnumerable<ProductInfo> products)
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

        private ExcelWorksheet CreateWorksheet(ExcelPackage package, string worksheetName)
        {
            var ws = package.Workbook.Worksheets.Add(worksheetName);

            ws.Cells["A1"].Value = "Title";
            ws.Column(1).Width = 85;

            ws.Cells["B1"].Value = "Brand";
            ws.Column(2).Width = 30;

            ws.Cells["C1"].Value = "Id";
            ws.Column(3).Width = 15;

            ws.Cells["D1"].Value = "Feedbacks";
            ws.Column(4).Width = 15;

            ws.Cells["E1"].Value = "Price";
            ws.Column(5).Width = 15;

            return ws;
        }
    }
}
