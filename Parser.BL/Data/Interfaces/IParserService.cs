using Parser.BL.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parser.BL.Data.Interfaces
{
    public interface IParserService
    {
        /// <summary>
        /// Method for return array products
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductInfo>> GetProductsAsync(string productName);
    }
}
