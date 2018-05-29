using FluentValidation;
using onboarding.Models;

namespace onboarding.Validations.Contract
{
    public class ContractValidator : AbstractValidator<Models.Contract>
    {

        public ContractValidator()
        {
            RuleFor(contract => contract.AcceptedAt).NotEmpty().WithName(onboarding.Resources.Models.Contract.AcceptedAt);
            RuleFor(contract => contract.Signature).NotEmpty().WithName(onboarding.Resources.Models.Contract.Signature);
        }
    }
}
