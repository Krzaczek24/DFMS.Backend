using System;

namespace DFMS.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string input)
        {
            return char.ToLower(input[0]) + input.Substring(1);
        }

        public static string ToCamelCase(this Enum input)
        {
            return ToCamelCase(input.ToString());
        }
    }
}
