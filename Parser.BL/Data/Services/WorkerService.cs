using Microsoft.Extensions.Configuration;
using Parser.BL.Data.Interfaces;
using Parser.BL.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Parser.BL.Data.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IParserService _parserService;
        private readonly IConfiguration _configuration;

        public WorkerService(IParserService parserService, IConfiguration configuration)
        {
            _parserService = parserService;
            _configuration = configuration;
        }

        public async Task RunAsync()
        {
            var products = new List<ProductInfo>(100);

            var path = _configuration["InputFileName"];
            using StreamReader reader = new StreamReader(path);

            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                line = line.Trim();

                products = await _parserService.GetProductsAsync(line);
            }
        }
    }
}
