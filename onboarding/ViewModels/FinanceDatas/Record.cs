using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace onboarding.ViewModels.FinanceDatas
{
    public class Record
    {
        public Record()
        {
            Guarantors = new HashSet<GuarantorViewModel>();
            Pendencies = new HashSet<Pendency>();
        }

        [Required]
        public int? Id { get; set; }
        [Required]
        public int? EnrollmentId { get; set; }
        public RepresentativeViewModel Representative { get; set; }
        public ICollection<GuarantorViewModel> Guarantors { get; set; }
        [Required]
        public int? PlanId { get; set; }
        [Required]
        public int? PaymentMethodId { get; set; }
        [JsonIgnore]
        public string UpdatedAt { get; set; }
        public bool Editable { get; set; }
        public IEnumerable<Pendency> Pendencies { get; set; }

    }
}
