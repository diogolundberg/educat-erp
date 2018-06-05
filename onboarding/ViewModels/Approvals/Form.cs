using System.Collections.Generic;

namespace onboarding.ViewModels.Approvals
{
    public class Form
    {
        public Form()
        {
            Pendencies = new HashSet<Pendency>();
        }

        public ICollection<Pendency> Pendencies { get; set; }
    }
}
