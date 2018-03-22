using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboarding.Models
{
    public class FinanceData : BaseModel
    {
        public FinanceData()
        {
            Guarantors = new HashSet<Guarantor>();
        }

        [ForeignKey("Enrollment")]
        public int? EnrollmentId { get; set; }

        [JsonIgnore]
        public virtual Enrollment Enrollment { get; set; }

        public virtual Representative Representative { get; set; }

        public virtual ICollection<Guarantor> Guarantors { get; set; }
    }
}
