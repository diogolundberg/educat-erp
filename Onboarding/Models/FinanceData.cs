using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        [ForeignKey("Plan")]
        public int? PlanId { get; set; }

        public virtual Plan Plan { get; set; }

        [ForeignKey("PaymentMethod")]
        public int? PaymentMethodId { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }

        [NotMapped]
        public bool Editable
        {
            get
            {
                return !Enrollment.SentAt.HasValue || Enrollment.FinancePendencies.Count() == 0;
            }
        }
    }
}
