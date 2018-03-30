using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Onboarding.ViewModel
{
    public class FinanceDataViewModel
    {
        public FinanceDataViewModel()
        {
            Guarantors = new HashSet<GuarantorViewModel>();
        }

        [Required]
        public int? Id { get; set; }

        [Required]
        public int? EnrollmentId { get; set; }

        public string State { get; set; }

        public RepresentativeViewModel Representative { get; set; }

        public ICollection<GuarantorViewModel> Guarantors { get; set; }

        [Required]
        public int? PlanId { get; set; }

        [Required]
        public int? PaymentMethodId { get; set; }

        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
    }
}
