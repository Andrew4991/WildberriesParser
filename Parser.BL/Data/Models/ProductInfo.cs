using Parser.BL.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.BL.Data.Models
{
    public class ProductInfo
    {
        [WidthExcelCell(85)]
        public string Title { get; set; }

        [WidthExcelCell(30)]
        public string Brand { get; set; }

        [WidthExcelCell(15)]
        public int Id { get; set; }

        [WidthExcelCell(15)]
        public int Feedbacks { get; set; }

        [WidthExcelCell(15)]
        public decimal Price { get; set; }
    }
}
