using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Parser.BL.Data.Interfaces;
using Parser.BL.Data.Models;
using Parser.BL.Data.Models.Options;
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
        private readonly IOptions<ProjectOptions> _options;

        public WorkerService(IParserService parserService, IConfiguration configuration, IExcelService excelService, IOptions<ProjectOptions> options)
        {
            _parserService = parserService;
            _configuration = configuration;
            _excelService = excelService;
            _options = options;
        }

        public async Task RunAsync()
        {
            var line = default(string);
            var products = new List<ProductInfo>(100);

            var outFile = new FileInfo(_options.Value.OutputFileName);
            using StreamReader reader = new StreamReader(_options.Value.InputFileName);

            _excelService.CreateDirectory(_options.Value.OutputFileName);
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
