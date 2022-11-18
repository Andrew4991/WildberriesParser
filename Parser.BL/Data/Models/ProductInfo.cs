using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.BL.Data.Models
{
    public class ProductInfo
    {
        public string Title { get; set; }

        public string Brand { get; set; }

        public int Id { get; set; }

        public int Feedbacks { get; set; }

        public decimal Price { get; set; }
    }
}
