using FluentValidation;
using Onboarding.Models;

namespace Onboarding.Validations
{
    public class RepresentativeValidator : AbstractValidator<Representative>
    {
        public RepresentativeValidator()
        {
            RuleFor(representative => representative.Name).NotEmpty();
            RuleFor(representative => representative.StreetAddress).NotEmpty();
            RuleFor(representative => representative.ComplementAddress).NotEmpty();
            RuleFor(representative => representative.Neighborhood).NotEmpty();
            RuleFor(representative => representative.PhoneNumber).NotEmpty();
            RuleFor(representative => representative.Landline).NotEmpty();
            RuleFor(representative => representative.Email).NotEmpty().EmailAddress();
            RuleFor(representative => representative.CityId).NotEmpty();
            RuleFor(representative => representative.StateId).NotEmpty();
            RuleFor(representative => representative.AddressKindId).NotEmpty();
            RuleFor(representative => representative.Zipcode).NotEmpty();
            RuleFor(representative => representative).Custom((representative, context) =>
            {
                if (representative is RepresentativePerson)
                {
                    if (((RepresentativePerson)representative).RelationshipId != null)
                    {
                        context.AddFailure("representative.relationship", "'Relacionamento' deve ser informado.");
                    }
                    if (!Cpf.ValidCPF(((RepresentativePerson)representative).Cpf))
                    {
                        context.AddFailure("representative.cpf", "'Cpf' não é válido.");
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(((RepresentativeCompany)representative).Cnpj))
                    {
                        context.AddFailure("cnpj", "'Cnpj' deve ser informado.");
                    }
                    if (string.IsNullOrEmpty(((RepresentativeCompany)representative).Contact))
                    {
                        context.AddFailure("relationship", "'Contato' deve ser informado.");
                    }
                }
            });
        }
    }
}
