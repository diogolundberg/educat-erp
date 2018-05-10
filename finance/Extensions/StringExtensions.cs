using System;

namespace finance
{
    public static class StringExtension
    {
        public static string UnCapitalize(this String str)
        {
            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }
}
