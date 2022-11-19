using System;

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
