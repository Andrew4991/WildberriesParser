using System.Collections.Generic;

namespace Parser.BL.Data.Interfaces
{
    public interface ITransfer<T>
    {
        /// <summary>
        /// Base method for transfer
        /// </summary>
        /// <param name="objects"></param>
        void Transfer(IEnumerable<T> objects);
    }
}
