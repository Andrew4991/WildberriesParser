using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Parser.BL.Data.Models.Api
{
    public class ApiProductInfo
    {
        [JsonPropertyName("__sort")]
        public int Sort { get; set; }

        [JsonPropertyName("ksort")]
        public int KSort { get; set; }


        [JsonPropertyName("ksale")]
        public int KSale { get; set; }


        [JsonPropertyName("id")]
        public int Id { get; set; }


        [JsonPropertyName("root")]
        public int Root { get; set; }


        [JsonPropertyName("kindId")]
        public int KindId { get; set; }


        [JsonPropertyName("subjectId")]
        public int SubjectId { get; set; }

        [JsonPropertyName("subjectParentId")]
        public int SubjectParentId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        [JsonPropertyName("brandId")]
        public int BrandId { get; set; }

        [JsonPropertyName("siteBrandId")]
        public int SiteBrandId { get; set; }

        [JsonPropertyName("sale")]
        public int Sale { get; set; }

        [JsonPropertyName("priceU")]
        public int PriceU { get; set; }

        [JsonPropertyName("salePriceU")]
        public int SalePriceU { get; set; }

        [JsonPropertyName("averagePrice")]
        public int AveragePrice { get; set; }

        [JsonPropertyName("benefit")]
        public int Benefit { get; set; }

        [JsonPropertyName("pics")]
        public int Pics { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("feedbacks")]
        public int Feedbacks { get; set; }

        [JsonPropertyName("diffPrice")]
        public bool DiffPrice { get; set; }

        [JsonPropertyName("promoTextCat")]
        public string PromoTextCat { get; set; }
    }
}
