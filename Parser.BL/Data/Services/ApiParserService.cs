using Parser.BL.Data.Helpers.Api;
using Parser.BL.Data.Interfaces;
using Parser.BL.Data.Models;
using Parser.BL.Data.Models.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parser.BL.Data.Services
{
    public class ApiParserService : IParserService
    {
        public async Task<List<ProductInfo>> GetProductsAsync(string productName)
        {
            var url = "https://search.wb.ru/exactmatch/sng/common/v4/search?__tmp=by&appType=1&couponsGeo=12,7,3,21&curr=byn&dest=12358386,12358403,-70563,-8139704&emp=0&lang=ru&locale=ru&pricemarginCoeff=1&query=мебель&reg=0&regions=80,83,4,33,70,82,69,68,86,30,40,48,1,22,66,31&resultset=catalog&sort=popular&spp=0&suppressSpellcheck=false";
            
            var xx = await ApiHelper.GetAsync<ApiSearchInfo>(url);

            return null;
        }
    }
}
