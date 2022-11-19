using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.BL.Data.Interfaces
{
    public interface ITransfer<T>
    {
        void Transfer(IEnumerable<T> objects);
    }
}
