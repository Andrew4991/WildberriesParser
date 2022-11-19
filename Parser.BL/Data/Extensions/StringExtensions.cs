using System.Linq;

namespace Parser.BL.Data.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// transform string to int. Delete bads chars
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int ToInt(this string @this)
        {
            int.TryParse(string.Join("", @this.Where(c => char.IsDigit(c))), out int value);

            return value;
        }

        /// <summary>
        /// transform string to decimal. Delete bads chars
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string @this)
        {
            decimal.TryParse(string.Join("", @this.Where(c => char.IsDigit(c) || c == ',')), out decimal value);

            return value;
        }

        /// <summary>
        /// Remove spatial space
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string RemoveSpetialSpace(this string @this)
        {
            return @this?.Replace("&nbsp;", "");
        }
    }
}
