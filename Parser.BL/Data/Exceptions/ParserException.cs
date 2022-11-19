using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.BL.Data.Exceptions
{
    public class ParserException : Exception
    {
        public ParserException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
