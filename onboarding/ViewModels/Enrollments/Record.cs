using System.Collections.Generic;

namespace onboarding.ViewModels.Enrollments
{
    public class Record
    {
        public Record()
        {
            Steps = new HashSet<Step>();
        }

        public string DaysRemaining { get; set; }
        public string OnboardingYear { get; set; }
        public string Photo { get; set; }
        public IEnumerable<Step> Steps { get; set; }
    }
}
