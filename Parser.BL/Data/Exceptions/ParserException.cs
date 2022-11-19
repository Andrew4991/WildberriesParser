using System;

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
