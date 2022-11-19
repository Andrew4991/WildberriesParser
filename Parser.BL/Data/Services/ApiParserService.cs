using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Parser.BL.Data.Helpers.Api;
using Parser.BL.Data.Interfaces;
using Parser.BL.Data.Models;
using Parser.BL.Data.Models.Api;
using Parser.BL.Data.Models.Options;
using Parser.BL.Data.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.BL.Data.Services
{
    public class ApiParserService : IParserService
    {
        private readonly string _baseUrl;

        private readonly IMapper _mapper;
        private readonly IOptions<ProjectOptions> _options;

        public ApiParserService(IOptions<ProjectOptions> options, IMapper mapper, IConfiguration configuration)
        {
            _baseUrl = GetBaseUrl(options.Value.ApiOptions);

            _mapper = mapper;
            _options = options;
        }

        public async Task<IEnumerable<ProductInfo>> GetProductsAsync(string productName)
        {
            var apiSearchInfo = await ApiHelper.GetAsync<ApiSearchInfo>(_baseUrl + productName);

            return _mapper.Map<IEnumerable<ProductInfo>>(apiSearchInfo?.Data?.Products?.Where(x => x.PromoTextCat?.Trim().ToLower() != _options.Value.AdsName));
        }

        /// <summary>
        /// Genereted base url for api
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private string GetBaseUrl(ApiOptions options)
        {
            var urlBuilder = new StringBuilder(options.Url);

            return  urlBuilder.SetQueryParam("__tmp", options.Tmp)
                .SetQueryParam("appType", options.AppType)
                .SetQueryParam("couponsGeo", options.CouponsGeo)
                .SetQueryParam("curr", options.Curr)
                .SetQueryParam("dest", options.Dest)
                .SetQueryParam("emp", options.Emp)
                .SetQueryParam("lang", options.Lang)
                .SetQueryParam("locale", options.Locale)
                .SetQueryParam("pricemarginCoeff", options.PricemarginCoeff)
                .SetQueryParam("reg", options.Reg)
                .SetQueryParam("regions", options.Regions)
                .SetQueryParam("resultset", options.Resultset)
                .SetQueryParam("sort", options.Sort)
                .SetQueryParam("spp", options.Spp)
                .SetQueryParam("suppressSpellcheck", options.SuppressSpellcheck.ToString())
                .ToString() + "&query=";
        }
    }
}
