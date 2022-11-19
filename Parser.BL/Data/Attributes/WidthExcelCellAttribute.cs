using System;

namespace Parser.BL.Data.Attributes
{
    public class WidthExcelCellAttribute : Attribute
    {
        private double _width;

        public WidthExcelCellAttribute(double width)
        {
            _width = width;
        }
    }
}
