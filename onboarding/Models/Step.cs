using System.Collections.Generic;

namespace onboarding.Models
{
    public class Step : BaseModel
    {
        public Step()
        {
            EnrollmentSteps = new HashSet<EnrollmentStep>();
        }

        public string Name { get; set; }
        public string Resource { get; set; }
        public int Order { get; set; }
        public bool HasApproval { get; set; }

        public ICollection<EnrollmentStep> EnrollmentSteps { get; set; }
    }
}
