using System.Collections.Generic;

namespace onboarding.ViewModels.Onboarding
{
    public class Record
    {
        public int Id { get; set; }
        public string Semester { get; set; }
        public string Year { get; set; }
        public string StartAt { get; set; }
        public string EndAt { get; set; }
        public List<EnrollmentForm> Enrollments { get; set; }
    }
}
