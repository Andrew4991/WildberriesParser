using Parser.BL.Data.Interfaces;
using Parser.BL.Data.Services;
using System;
using System.Threading.Tasks;

namespace Parser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IParserService xx = new ApiParserService();

            var xxc = await xx.GetProductsAsync("sd");

            Console.ReadKey();
        }
    }
}
