using System;

namespace Parser.BL.Data.Exceptions
{
    public class InputFileException : Exception
    {
        public InputFileException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
