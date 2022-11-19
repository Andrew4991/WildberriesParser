using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Parser.BL.Data.Models.Options
{
    public class ApiOptions
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("__tmp")]
        public string Tmp { get; set; }

        [JsonPropertyName("appType")]
        public string AppType { get; set; }

        [JsonPropertyName("couponsGeo")]
        public string CouponsGeo { get; set; }

        [JsonPropertyName("curr")]
        public string Curr { get; set; }

        [JsonPropertyName("dest")]
        public string Dest { get; set; }

        [JsonPropertyName("emp")]
        public string Emp { get; set; }

        [JsonPropertyName("lang")]
        public string Lang { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("pricemarginCoeff")]
        public string PricemarginCoeff { get; set; }

        [JsonPropertyName("reg")]
        public string Reg { get; set; }

        [JsonPropertyName("regions")]
        public string Regions { get; set; }

        [JsonPropertyName("resultset")]
        public string Resultset { get; set; }

        [JsonPropertyName("sort")]
        public string Sort { get; set; }

        [JsonPropertyName("spp")]
        public string Spp { get; set; }

        [JsonPropertyName("suppressSpellcheck")]
        public bool SuppressSpellcheck { get; set; }

        [JsonPropertyName("query")]
        public string Query { get; set; }
    }
}
