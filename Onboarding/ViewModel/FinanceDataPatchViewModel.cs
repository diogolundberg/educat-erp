using System.Collections.Generic;

namespace Onboarding.ViewModel
{
    public class FinanceDataPatchViewModel
    {
        public RepresentativePatchViewModel Representative { get; set; }

        public ICollection<GuarantorPatchViewModel> Guarantors { get; set; }
    }
}
