using System;

namespace Parser.BL.Data.Exceptions
{
    /// <summary>
    /// Exception for check input file
    /// </summary>
    public class InputFileException : Exception
    {
        public InputFileException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
