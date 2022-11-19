using System;

namespace Parser.BL.Data.Exceptions
{
    /// <summary>
    /// Exception fo httpClient
    /// </summary>
    public class ApiErrorException : Exception
    {
        public ApiErrorException(string message, Exception innerException)
            : base(message, innerException)
        { 
            
        }
    }
}
