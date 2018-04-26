using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using onboarding.ViewModels;
using System;

namespace onboarding
{
    public static class StringExtension
    {
        public static string UnCapitalize(this String str)
        {
            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }

}
