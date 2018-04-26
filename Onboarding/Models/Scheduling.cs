using onboarding.Models;

namespace onboarding.Models
{
    public class Scheduling : BaseModel
    {
        public int OnboardingId { get; set; }

        public virtual Models.Onboarding Onboarding { get; set; }
    }
}
