using System;

namespace Parser.BL.Data.Attributes
{
    /// <summary>
    /// Attribute for set width cell
    /// </summary>
    public class WidthExcelCellAttribute : Attribute
    {
        private double _width;

        public WidthExcelCellAttribute(double width)
        {
            _width = width;
        }
    }
}
