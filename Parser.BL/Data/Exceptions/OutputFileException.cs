using System;

namespace Parser.BL.Data.Exceptions
{
    /// <summary>
    /// Exception for check output file
    /// </summary>
    public class OutputFileException : Exception
    {
        public OutputFileException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
