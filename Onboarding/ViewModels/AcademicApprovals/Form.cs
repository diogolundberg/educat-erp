using System.Collections.Generic;

namespace onboarding.ViewModels.AcademicApprovals
{
    public class Form
    {
        public Form()
        {
            Pendencies = new HashSet<AcademicPendency>();
        }

        public string EnrollmentNumber { get; set; }

        public IEnumerable<AcademicPendency> Pendencies { get; set; }
    }
}

