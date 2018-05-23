using System.Collections.Generic;

namespace onboarding.ViewModels.Onboarding
{
    public class Form
    {
        public string StartAt { get; set; }
        public string EndAt { get; set; }
        public string Semester { get; set; }
        public string Year { get; set; }
        public List<EnrollmentForm> Enrollments { get; set; }
    }
}
