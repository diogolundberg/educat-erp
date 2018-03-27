using System.Collections.Generic;

namespace Onboarding.ViewModel
{
    public class FinanceDataPatchViewModel
    {
        public FinanceDataPatchViewModel()
        {
            Guarantors = new HashSet<GuarantorPatchViewModel>();
        }

        public RepresentativePatchViewModel Representative { get; set; }

        public ICollection<GuarantorPatchViewModel> Guarantors { get; set; }
    }
}
