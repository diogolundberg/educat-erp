using System.Linq;
using Microsoft.EntityFrameworkCore;
using onboarding.Models;

namespace onboarding.Services
{
    public class ContractService : BaseService<Contract>
    {
        public ContractService(DatabaseContext context) : base(context)
        {
        }

        public override IQueryable<Contract> List()
        {
            return base.List().Include("Enrollment");
        }
    }
}
