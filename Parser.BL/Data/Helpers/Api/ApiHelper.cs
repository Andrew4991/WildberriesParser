using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
                var message = $"error: {e.Message}, url: {url}";
                throw new Exception(message, e);
            }
        }
    }
}
