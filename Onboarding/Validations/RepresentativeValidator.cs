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
        }
    }
}
