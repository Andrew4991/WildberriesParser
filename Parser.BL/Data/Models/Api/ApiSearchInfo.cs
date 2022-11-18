using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Parser.BL.Data.Models.Api
{
    public class ApiSearchInfo
    {
        [JsonPropertyName("data")]
        public ApiSearchDataInfo Data { get; set; }
    }
}
