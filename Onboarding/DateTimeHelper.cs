using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onboarding
{
    static class DateTimeHelper
    {
        public static string Format(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToString("dd/MM/yyyy") : null;
        }
    }
}
