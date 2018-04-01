using System.Collections.Generic;

namespace Onboarding.ViewModels.AcademicApprovals
{
    public class Form
    {
        public Form()
        {
            AcademicPendencies = new HashSet<AcademicPendency>();
        }

        public string EnrollmentNumber { get; set; }

        public IEnumerable<AcademicPendency> AcademicPendencies { get; set; }
    }
}

