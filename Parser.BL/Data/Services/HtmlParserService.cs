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

        public async Task<IEnumerable<ProductInfo>> GetProductsAsync(string productName)
        {
            using StreamReader reader = new StreamReader("Inputs\\Text.txt");
            var text = reader.ReadToEnd();
            var page = new HtmlDocument();
            page.LoadHtml(text);

            //var page = await ApiHelper.GetHtmlDocumentByPhantomJsCloud(_options.Value.SearchProductsUrl + productName, _options.Value.PhantomJsCloudOptions.FullUrl);
            var blokProductList = GetProductListNode(page.DocumentNode);

            if (blokProductList == null) return new List<ProductInfo>();

            return blokProductList.ChildNodes
                .Where(x => x.Name == _options.Value.HtmlParserOptions.CardItem.TagName && !IsAdsProduct(x))
                .Select(x => new ProductInfo
                {
                    Id = GetProductId(x),
                    Brand = GetProductBrand(x),
                    Title = GetProductTitle(x),
                    Feedbacks = GetProductFeedbacks(x),
                    Price = GetProductPrice(x)
                });
        }

        private HtmlNode GetProductListNode(HtmlNode document)
        {
            return document.SelectSingleNode(
                string.Format(_searchByClass,
                _options.Value.HtmlParserOptions.CardList.TagName,
                _options.Value.HtmlParserOptions.CardList.Class)); ;
        }

        private bool IsAdsProduct(HtmlNode item)
        {
            return item.HasClass(_options.Value.HtmlParserOptions.AdvertClassName)
                    && item.SelectSingleNode(
                        string.Format(_searchByClass,
                        _options.Value.HtmlParserOptions.Promo.TagName,
                        _options.Value.HtmlParserOptions.Promo.Class))?.InnerText == _options.Value.LowerAdsName;
        }

        private int GetProductId(HtmlNode item)
        {
            return item.Attributes.FirstOrDefault(y => y.Name == _options.Value.HtmlParserOptions.IdAttributeName).Value.ToInt();
        }

        private string GetProductBrand(HtmlNode item)
        {
            return item.SelectSingleNode(
                        string.Format(_searchByClass,
                        _options.Value.HtmlParserOptions.Brand.TagName,
                        _options.Value.HtmlParserOptions.Brand.Class))?.InnerText;
        }

        private string GetProductTitle(HtmlNode item)
        {
            return item.SelectSingleNode(
                        string.Format(_searchByClass,
                        _options.Value.HtmlParserOptions.Title.TagName,
                        _options.Value.HtmlParserOptions.Title.Class))?.InnerText;
        }

        private int GetProductFeedbacks(HtmlNode item)
        {
            return item.SelectSingleNode(
                        string.Format(_searchByClass,
                        _options.Value.HtmlParserOptions.Feedbacks.TagName,
                        _options.Value.HtmlParserOptions.Feedbacks.Class))?.InnerText?.ToInt() ?? 0;
        }

        private decimal GetProductPrice(HtmlNode item)
        {
            return (item.SelectSingleNode(
                        string.Format(_searchByClass,
                        _options.Value.HtmlParserOptions.PriceWithoutDiscount.TagName,
                        _options.Value.HtmlParserOptions.PriceWithoutDiscount.Class))?.InnerText
                    ?? item.SelectSingleNode(
                        string.Format(_searchByClassAndNext,
                        _options.Value.HtmlParserOptions.PriceWithDiscount.TagName
                        , _options.Value.HtmlParserOptions.PriceWithDiscount.Class,
                        _options.Value.HtmlParserOptions.DeleteTagName))?.InnerText).RemoveSpetialSpace().ToDecimal();
        }
    }
}
