using System.Collections.Generic;

namespace Onboarding.ViewModels.Onboarding
{
    public class Form
    {
        public List<EnrollmentForm> Enrollments { get; set; }
        public string StartAt { get; set; }
        public string Year { get; set; }
        public string Semester { get; set; }
    }
}
