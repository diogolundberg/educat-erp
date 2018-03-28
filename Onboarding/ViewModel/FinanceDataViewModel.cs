using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboarding.ViewModel
{
    public class FinanceDataViewModel
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public int? EnrollmentId { get; set; }

        public string State { get; set; }

        public FinanceDataViewModel()
        {
            Guarantors = new HashSet<GuarantorViewModel>();
        }

        public RepresentativeViewModel Representative { get; set; }

        public ICollection<GuarantorViewModel> Guarantors { get; set; }
    }
}
