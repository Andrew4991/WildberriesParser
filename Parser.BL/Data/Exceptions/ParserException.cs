using System;

namespace Parser.BL.Data.Exceptions
{
    /// <summary>
    /// Exception for parser worker
    /// </summary>
    public class ParserException : Exception
    {
        public ParserException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
