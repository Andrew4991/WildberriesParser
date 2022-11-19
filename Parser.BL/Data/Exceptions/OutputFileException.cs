using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.BL.Data.Exceptions
{
    public class OutputFileException : Exception
    {
        public OutputFileException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
