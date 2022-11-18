using Parser.BL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parser.BL.Data.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IParserService _parserService;

        public WorkerService(IParserService parserService)
        {
            _parserService = parserService;
        }

        public async Task RunAsync()
        {
            throw new NotImplementedException();
        }
    }
}
