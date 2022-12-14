using OfficeOpenXml;
using Parser.BL.Data.Attributes;
using Parser.BL.Data.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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

        /// <summary>
        /// Create new worksheet by name
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        private ExcelWorksheet CreateWorksheet(ExcelPackage package)
        {
            return package.Workbook.Worksheets.Add(_worksheetName);
        }

        /// <summary>
        /// Set default header 
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="index"></param>
        private void SetHeader(ExcelWorksheet ws, int index)
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            for (int row = 0; row < propertyInfos.Length; row++)
            {
                ws.Cells[index, row + 1].Value = propertyInfos[row].Name;
                ws.Column(row + 1).Width = (double)propertyInfos[row].CustomAttributes
                    .FirstOrDefault(x => x.AttributeType == typeof(WidthExcelCellAttribute)).ConstructorArguments.FirstOrDefault().Value;
            }
        }

        /// <summary>
        /// Set data from collection
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="objects"></param>
        /// <param name="index"></param>
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
