using System.Linq;
using Microsoft.EntityFrameworkCore;
using onboarding.Models;

namespace onboarding.Services
{
    public class SectionService : BaseService<Section>
    {
        public SectionService(DatabaseContext context) : base(context)
        {
        }

        public override IQueryable<Section> List()
        {
            return base.List();
        }
    }
}
