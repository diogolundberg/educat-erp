using System.Collections.Generic;

namespace Onboarding.ViewModel
{
    public class FinanceDataViewModel
    {
        public FinanceDataViewModel()
        {
            Guarantors = new HashSet<GuarantorViewModel>();
        }

        public RepresentativeViewModel Representative { get; set; }

        public ICollection<GuarantorViewModel> Guarantors { get; set; }
    }
}
