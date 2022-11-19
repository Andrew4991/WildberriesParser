using System.Linq;

namespace Parser.BL.Data.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt(this string @this)
        {
            int.TryParse(string.Join("", @this.Where(c => char.IsDigit(c))), out int value);

            return value;
        }

        public static decimal ToDecimal(this string @this)
        {
            decimal.TryParse(string.Join("", @this.Where(c => char.IsDigit(c) || c == ',')), out decimal value);

            return value;
        }

        public static string RemoveSpetialSpace(this string @this)
        {
            return @this?.Replace("&nbsp;", "");
        }
    }
}
