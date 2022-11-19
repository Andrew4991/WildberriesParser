using AutoMapper;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Parser.BL.Data.Extensions;
using Parser.BL.Data.Helpers.Api;
using Parser.BL.Data.Interfaces;
using Parser.BL.Data.Models;
using Parser.BL.Data.Models.Api;
using Parser.BL.Data.Models.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Parser.BL.Data.Services
{
    public class HtmlParserService : IParserService
    {
        private const string _searchByClass = "(.//{0}[contains(@class,'{1}')])";
        private const string _searchByClassAndNext = "(.//{0}[contains(@class,'{1}')]/{2})";

        private readonly IOptions<ProjectOptions> _options;

        public HtmlParserService(IOptions<ProjectOptions> options)
        {
            _options = options;
        }

        public async Task<List<ProductInfo>> GetProductsAsync(string productName)
        {
            var products = new List<ProductInfo>(100);
            using StreamReader reader = new StreamReader("Inputs\\Text.txt");
            var text = reader.ReadToEnd();
            var page = new HtmlDocument();
            page.LoadHtml(text);

            //var page = await ApiHelper.GetHtmlDocumentByPhantomJsCloud(_options.Value.SearchProductsUrl + productName, _options.Value.PhantomJsCloudOptions.FullUrl);
            var blokProductList = page.DocumentNode.SelectSingleNode(
                string.Format(_searchByClass,
                _options.Value.HtmlParserOptions.CardList.TagName,
                _options.Value.HtmlParserOptions.CardList.Class));

            if (blokProductList == null) return products;

            foreach (var item in blokProductList.ChildNodes.Where(x => x.Name == _options.Value.HtmlParserOptions.CardItem.TagName))
            {
                if (item.HasClass(_options.Value.HtmlParserOptions.AdvertClassName) 
                    && item.SelectSingleNode(
                        string.Format(_searchByClass,
                        _options.Value.HtmlParserOptions.Promo.TagName,
                        _options.Value.HtmlParserOptions.Promo.Class))?.InnerText == _options.Value.LowerDiscountName
                   )
                {
                    continue;
                }

                products.Add(new ProductInfo
                {
                    Id = item.Attributes.FirstOrDefault(x => x.Name == _options.Value.HtmlParserOptions.IdAttributeName).Value.ToInt(),

                    Brand = item.SelectSingleNode(
                        string.Format(_searchByClass,
                        _options.Value.HtmlParserOptions.Brand.TagName,
                        _options.Value.HtmlParserOptions.Brand.Class))?.InnerText,

                    Title = item.SelectSingleNode(
                        string.Format(_searchByClass,
                        _options.Value.HtmlParserOptions.Title.TagName,
                        _options.Value.HtmlParserOptions.Title.Class))?.InnerText,

                    Feedbacks = item.SelectSingleNode(
                        string.Format(_searchByClass,
                        _options.Value.HtmlParserOptions.Feedbacks.TagName,
                        _options.Value.HtmlParserOptions.Feedbacks.Class))?.InnerText?.ToInt() ?? 0,

                    Price = (item.SelectSingleNode(
                        string.Format(_searchByClass,
                        _options.Value.HtmlParserOptions.PriceWithoutDiscount.TagName,
                        _options.Value.HtmlParserOptions.PriceWithoutDiscount.Class))?.InnerText 
                    ?? item.SelectSingleNode(
                        string.Format(_searchByClassAndNext,
                        _options.Value.HtmlParserOptions.PriceWithDiscount.TagName
                        , _options.Value.HtmlParserOptions.PriceWithDiscount.Class,
                        _options.Value.HtmlParserOptions.DeleteTagName))?.InnerText).RemoveSpetialSpace().ToDecimal()
                });
            }

            return products;
        }
    }
}
