using System;

namespace Onboarding.Models
{
    public class Enrollment : BaseModel
    {
        public Enrollment()
        {
        }

        public virtual PersonalData PersonalData { get; set; }

        public virtual FinanceData FinanceData { get; set; }

        public DateTime? SendDate { get; set; }

        public bool AcademicApproval { get; set; }

        public bool FinanceApproval { get; set; }
    }
}

