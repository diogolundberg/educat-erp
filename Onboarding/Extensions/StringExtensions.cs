using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Onboarding.ViewModels;
using System;

namespace Onboarding
{
    public static class StringExtension
    {
        public static string UnCapitalize(this String str)
        {
            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }

}
