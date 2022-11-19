using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Parser.BL.Data.Models.Api
{
    public class ApiSearchDataInfo
    {
        [JsonPropertyName("products")]
        public IEnumerable<ApiProductInfo> Products { get; set; }
    }
}
