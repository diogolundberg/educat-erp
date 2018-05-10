using System;

namespace onboarding
{
    static class DateTimeHelper
    {
        public static string Format(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToString("dd/MM/yyyy") : null;
        }

        public static string Format(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }
    }
}
