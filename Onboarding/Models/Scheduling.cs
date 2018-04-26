using System;

namespace onboarding.Models
{
    public class Scheduling : BaseModel
    {
        public int OnboardingId { get; set; }
        public virtual Onboarding Onboarding { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Intervals { get; set; }
        public string Breaks { get; set; }
    }
}