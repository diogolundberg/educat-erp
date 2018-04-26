using System.Collections.Generic;

namespace onboarding.ViewModels.Scheduling
{
    public class Form
    {
        public int OnboardingId { get; set; }
        public string StartAt { get; set; }
        public string EndAt { get; set; }
        public string Intervals { get; set; }
        public List<FormBreak> Breaks { get; set; }
    }

    public class FormBreak
    {
        public string Start { get; set; }
        public string End { get; set; }
    }
}
