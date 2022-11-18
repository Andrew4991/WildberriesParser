using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Parser.BL.Data.Helpers.Api;
using Parser.BL.Data.Interfaces;
using Parser.BL.Data.Models;
using Parser.BL.Data.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Parser.BL.Data.Services
{
    public class ApiParserService : IParserService
    {
        private readonly string _baseUrl;
        private readonly string _discountName;

        private readonly IMapper _mapper;

        public ApiParserService(IOptions<ApiOptions> options, IMapper mapper, IConfiguration configuration)
        {
            _baseUrl = $"{options.Value.Url}?__tmp={options.Value.Tmp}&appType={options.Value.AppType}&couponsGeo={options.Value.CouponsGeo}" +
            $"&curr={options.Value.Curr}&dest={options.Value.Dest}&emp={options.Value.Emp}&lang={options.Value.Lang}&locale={options.Value.Locale}" +
            $"&pricemarginCoeff={options.Value.PricemarginCoeff}&reg={options.Value.Reg}&regions={options.Value.Regions}&resultset={options.Value.Resultset}" +
            $"&sort={options.Value.Sort}&spp={options.Value.Spp}&suppressSpellcheck={options.Value.SuppressSpellcheck}&query=";

            _discountName = configuration["DiscountName"]?.Trim().ToLower();

            _mapper = mapper;
        }

        public async Task<List<ProductInfo>> GetProductsAsync(string productName)
        {
            var apiSearchInfo = await ApiHelper.GetAsync<ApiSearchInfo>(_baseUrl + productName);

            return _mapper.Map<List<ProductInfo>>(apiSearchInfo?.Data?.Products?.Where(x => x.PromoTextCat?.Trim().ToLower() != _discountName));
        }
    }
}
