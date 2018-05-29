using FluentValidation;
using onboarding.Models;

namespace onboarding.Statuses
{
    public class ContractStatus : BaseStatus<Contract>
    {
        public ContractStatus(AbstractValidator<Contract> validator, Contract entity) : base(validator, entity)
        {
        }

        public override string GetStatus()
        {
            if(!string.IsNullOrEmpty(_entity.Signature) && _entity.AcceptedAt.HasValue)
            {
                return "valid";
            }
            else
            {
                return "invalid";
            }
        }
    }
}
