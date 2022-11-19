using Parser.BL.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parser.BL.Data.Interfaces
{
    public interface IParserService
    {
        Task<IEnumerable<ProductInfo>> GetProductsAsync(string productName);
    }
}
