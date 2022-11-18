using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Parser.BL.Data.Interfaces
{
    public interface IWorkerService
    {
        Task RunAsync();
    }
}
