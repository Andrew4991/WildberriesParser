using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Parser.BL.Data.Models.Options
{
    public class ProjectOptions
    {
        public bool UseApiParser { get; set; }

        public string SearchProductsUrl { get; set; }

        public string InputFileName { get; set; }

        public string OutputFileName { get; set; }

        public string DiscountName { get; set; }

        public PhantomJsCloudOptions PhantomJsCloudOptions { get; set; }
       
        public ApiOptions ApiOptions { get; set; }

        public HtmlParserOptions HtmlParserOptions { get; set; }

        [NotMapped]
        public string LowerDiscountName => DiscountName?.Trim().ToLower();
    }
}
