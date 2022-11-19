using System.Collections.Generic;

namespace Parser.BL.Data.Interfaces
{
    public interface ITransfer<T>
    {
        void Transfer(IEnumerable<T> objects);
    }
}
