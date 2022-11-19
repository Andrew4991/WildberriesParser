using OfficeOpenXml;
using Parser.BL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Parser.BL.Data.Services
{
    public class ExcelTransferService<T> : IExcelTransferService<T>
    {
        private readonly FileInfo _fileInfo;
        private readonly string _worksheetName;

        public ExcelTransferService(FileInfo fileInfo, string worksheetName)
        {
            _fileInfo = fileInfo;
            _worksheetName = worksheetName;
        }

        public void Transfer(IEnumerable<T> objects)
        {
            using var package = new ExcelPackage(_fileInfo);

            if (package.Workbook.Worksheets.Any(x => x.Name == _worksheetName)) return;

            var ws = CreateWorksheet(package);

            var index = 1;

            SetHeader(ws, index);

            SetData(ws, objects, ++index);
            package.Save();
        }

        private ExcelWorksheet CreateWorksheet(ExcelPackage package)
        {
            return package.Workbook.Worksheets.Add(_worksheetName);
        }

        private void SetHeader(ExcelWorksheet ws, int index)
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            for (int row = 0; row < propertyInfos.Length; row++)
            {
                ws.Cells[index, row + 1].Value = propertyInfos[row].Name;
            }
        }

        private void SetData(ExcelWorksheet ws, IEnumerable<T> objects, int index)
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            foreach (var item in objects)
            {
                for (int row = 0; row < propertyInfos.Length; row++)
                {
                    ws.Cells[index, row + 1].Value = propertyInfos[row].GetValue(item);
                }

                index++;
            }
        }
    }
}
