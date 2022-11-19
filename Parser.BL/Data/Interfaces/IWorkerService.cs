using System.Threading.Tasks;

namespace Parser.BL.Data.Interfaces
{
    public interface IWorkerService
    {
        /// <summary>
        /// Worker for program
        /// </summary>
        /// <returns></returns>
        Task RunAsync();
    }
}
