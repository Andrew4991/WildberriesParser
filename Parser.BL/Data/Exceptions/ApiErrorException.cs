using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.BL.Data.Exceptions
{
    public class ApiErrorException : Exception
    {
        public ApiErrorException(string message, Exception innerException)
            : base(message, innerException)
        { 
            
        }
    }
}
