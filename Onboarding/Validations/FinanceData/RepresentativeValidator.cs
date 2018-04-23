using FluentValidation;
using Onboarding.Models;

namespace Onboarding.Validations.FinanceData
{
    public class RepresentativeValidator : AbstractValidator<Representative>
    {
        public RepresentativeValidator()
        {
            RuleFor(representative => representative.Name).NotEmpty().WithName(onboarding.Resources.Models.Representative.Name);
            RuleFor(representative => representative.StreetAddress).NotEmpty().WithName(onboarding.Resources.Models.Representative.StreetAddress);
            RuleFor(representative => representative.AddressNumber).NotEmpty().WithName(onboarding.Resources.Models.Representative.AddressNumber);
            RuleFor(representative => representative.Neighborhood).NotEmpty().WithName(onboarding.Resources.Models.Representative.Neighborhood);
            RuleFor(representative => representative.PhoneNumber).NotEmpty().WithName(onboarding.Resources.Models.Representative.PhoneNumber);
            RuleFor(representative => representative.Landline).NotEmpty().WithName(onboarding.Resources.Models.Representative.Landline);
            RuleFor(representative => representative.CityId).NotEmpty().WithName(onboarding.Resources.Models.Representative.CityId);
            RuleFor(representative => representative.StateId).NotEmpty().WithName(onboarding.Resources.Models.Representative.StateId);
            RuleFor(representative => representative.AddressKindId).NotEmpty().WithName(onboarding.Resources.Models.Representative.AddressKindId);
            RuleFor(representative => representative.Zipcode).NotEmpty().WithName(onboarding.Resources.Models.Representative.Zipcode);

            RuleFor(representative => representative.Email).NotEmpty();
            RuleFor(representative => representative.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(representative => representative).Custom((representative, context) =>
            {
                if (representative is RepresentativePerson)
                {
                    if (((RepresentativePerson)representative).RelationshipId == null)
                    {
                        context.AddFailure("representative.relationshipId", string.Format(onboarding.Resources.Shared.RequiredField, onboarding.Resources.Models.Representative.Relationship));
                    }
                    if (!Cpf.ValidCPF(((RepresentativePerson)representative).Cpf))
                    {
                        context.AddFailure("cnpj", string.Format(onboarding.Resources.Shared.NotValidField, onboarding.Resources.Models.Representative.CPF));
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(((RepresentativeCompany)representative).Cnpj))
                    {
                        context.AddFailure("cnpj", string.Format(onboarding.Resources.Shared.RequiredField, onboarding.Resources.Models.Representative.CNPJ));
                    }
                    if (string.IsNullOrEmpty(((RepresentativeCompany)representative).Contact))
                    {
                        context.AddFailure("cnpj", string.Format(onboarding.Resources.Shared.RequiredField, onboarding.Resources.Models.Representative.Contact));
                    }
                }
            });
        }
    }
}
