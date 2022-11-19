using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.BL.Data.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder SetQueryParam(this StringBuilder builder, string name, string value)
        {
            return builder.Append($"{name}={value}&");
        }
    }
}
