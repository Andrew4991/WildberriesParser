using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Parser.BL.Data.Exceptions;

namespace Parser.BL.Data.Helpers.Api
{
    public static class ApiHelper
    {
        public static async Task<T> GetAsync<T>(string url)
        {
            try
            {
                using var httpClientHandler = new HttpClientHandler();
                HttpClient client = new HttpClient(httpClientHandler);

                var response = await client.GetAsync(url);
                var responseStream = await response.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<T>(responseStream);
            }
            catch (Exception e)
            {
                throw new ApiErrorException(url, e);
            }
        }

        public static async Task<HtmlDocument> GetHtmlDocumentAsync(string url)
        {
            try
            {
                using var httpClientHandler = new HttpClientHandler();
                HttpClient client = new HttpClient(httpClientHandler);

                var response = await client.GetByteArrayAsync(url);
                var responseStr = Encoding.UTF8.GetString(response, 0, response.Length - 1);

                var pageDocument = new HtmlDocument();
                pageDocument.LoadHtml(responseStr);
                return pageDocument;
            }
            catch (Exception e)
            {
                throw new ApiErrorException(url, e);
            }
        }

        public static async Task<HtmlDocument> GetHtmlDocumentByPhantomJsCloud(string url, string urlPhantomJsCloud)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.ExpectContinue = false;

                var pageRequestData = new
                {
                    url = url,
                    renderType = "html",
                    outputAsJson = false
                };

                var pageRequestJson = new StringContent(JsonSerializer.Serialize(pageRequestData));
                var response = await client.PostAsync(urlPhantomJsCloud, pageRequestJson);

                var pageDocument = new HtmlDocument();
                pageDocument.LoadHtml(response.Content.ReadAsStringAsync().Result);
                return pageDocument;
            }
            catch (Exception e)
            {
                throw new ApiErrorException(url, e);
            }
        }
    }
}
