using System.Text;

namespace Parser.BL.Data.Extensions
{
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Genereted Url by param name and value
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StringBuilder SetQueryParam(this StringBuilder builder, string name, string value)
        {
            return builder.Append($"{name}={value}&");
        }
    }
}
