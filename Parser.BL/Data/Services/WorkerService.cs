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
        private readonly IExcelService _excelService;
        private readonly IConfiguration _configuration;

        public WorkerService(IParserService parserService, IConfiguration configuration, IExcelService excelService)
        {
            _parserService = parserService;
            _configuration = configuration;
            _excelService = excelService;
        }

        public async Task RunAsync()
        {
            var line = default(string);
            var products = new List<ProductInfo>(100);

            var inputPath = _configuration["InputFileName"];
            var outPath = _configuration["OutputFileName"];
            var outFile = new FileInfo(outPath);
            using StreamReader reader = new StreamReader(inputPath);

            _excelService.CreateDirectory(outPath);
            _excelService.DeleteOldFile(outFile);

            while ((line = await reader.ReadLineAsync()) != null)
            {
                line = line.Trim();

                products = await _parserService.GetProductsAsync(line);

                _excelService.AddWorksheet(outFile, line, products);
            }
        }
    }
}
