using System;
using System.Text.RegularExpressions;

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

        public override string CreateExternalId()
        {
            string semester = DateTime.Now.Month > 6 ? "2" : "1";
            return DateTime.Now.Year  + semester + Regex.Replace(PersonalData.CPF, @"\D" , string.Empty); ;
        }
    }
}

