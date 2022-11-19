using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Parser.BL.Data.Interfaces;
using Parser.BL.Data.Models;
using Parser.BL.Data.Models.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.BL.Data.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IParserService _parserService;
        private readonly IFileService _fileService;
        private readonly IOptions<ProjectOptions> _options;

        public WorkerService(IParserService parserService, IOptions<ProjectOptions> options, IFileService fileService)
        {
            _parserService = parserService;
            _options = options;
            _fileService = fileService;
        }

        public async Task RunAsync()
        {            
            var uniqueKeys = await GetKeywords();     

            var outFile = GetOutputFileInfoWithCheck();

            foreach (var key in uniqueKeys)
            {
                var products = await _parserService.GetProductsAsync(key);
                var transfer = new ExcelTransferService<ProductInfo>(outFile, key);

                transfer.Transfer(products);
            }
        }

        private async Task<HashSet<string>> GetKeywords()
        {
            var line = default(string);
            var uniqueKeys = new HashSet<string>();

            //read keywords from file
            using StreamReader reader = new StreamReader(_options.Value.InputFileName);
            while ((line = await reader.ReadLineAsync()) != null)
            {
                uniqueKeys.Add(line.Trim());
            }

            return uniqueKeys;
        }

        private FileInfo GetOutputFileInfoWithCheck()
        {
            var outFile = new FileInfo(_options.Value.OutputFileName);

            //check directory and old file
            _fileService.CreateDirectory(_options.Value.OutputFileName);
            _fileService.DeleteFile(outFile);

            return outFile;
        }
    }
}
