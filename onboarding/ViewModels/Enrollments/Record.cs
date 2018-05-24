using System.Collections.Generic;

namespace onboarding.ViewModels.Enrollments
{
    public class Record
    {
        public Record()
        {
            Cards = new HashSet<Card>();
        }

        public string DaysRemaining { get; set; }
        public string OnboardingYear { get; set; }
        public string Photo { get; set; }
        public IEnumerable<Card> Cards { get; set; }
    }
}
