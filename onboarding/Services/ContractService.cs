using onboarding.Models;

namespace onboarding.Services
{
    public class ContractService : BaseService<Contract>
    {
        public ContractService(DatabaseContext context) : base(context)
        {
        }
    }
}
