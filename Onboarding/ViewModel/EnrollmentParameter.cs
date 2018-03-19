using System;
using System.Collections.Generic;

namespace Onboarding.ViewModel
{
    public class EnrollmentParameter
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<string> Emails { get; set; }
    }
}