using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using Onboarding.Models;
using Onboarding.Validations.FinanceData;

namespace Onboarding.Validations
{
    public class GuarantorValidator : AbstractValidator<Guarantor>
    {
        public GuarantorValidator()
        {
            RuleFor(guarantor => guarantor.Name).NotEmpty().WithName(onboarding.Resources.Models.Guarantor.Name);
            RuleFor(guarantor => guarantor.StreetAddress).NotEmpty().WithName(onboarding.Resources.Models.Guarantor.StreetAddress);
            RuleFor(guarantor => guarantor.AddressNumber).NotEmpty().WithName(onboarding.Resources.Models.Guarantor.AddressNumber);
            RuleFor(guarantor => guarantor.Neighborhood).NotEmpty().WithName(onboarding.Resources.Models.Guarantor.Neighborhood);
            RuleFor(guarantor => guarantor.PhoneNumber).NotEmpty().WithName(onboarding.Resources.Models.Guarantor.PhoneNumber);
            RuleFor(guarantor => guarantor.Landline).NotEmpty().WithName(onboarding.Resources.Models.Guarantor.Landline);
            RuleFor(guarantor => guarantor.Email).NotEmpty().WithName(onboarding.Resources.Models.Guarantor.Email);
            RuleFor(guarantor => guarantor.CityId).NotEmpty().WithName(onboarding.Resources.Models.Guarantor.CityId);
            RuleFor(guarantor => guarantor.StateId).NotEmpty().WithName(onboarding.Resources.Models.Guarantor.StateId);
            RuleFor(guarantor => guarantor.FinanceDataId).NotEmpty().WithName(onboarding.Resources.Models.Guarantor.FinanceDataId);
            RuleFor(guarantor => guarantor.RelationshipId).NotEmpty().WithName(onboarding.Resources.Models.Guarantor.RelationshipId);
            RuleFor(guarantor => guarantor.AddressKindId).NotEmpty().WithName(onboarding.Resources.Models.Guarantor.AddressKindId);
            RuleFor(guarantor => guarantor.Zipcode).NotEmpty().WithName(onboarding.Resources.Models.Guarantor.Zipcode);

            RuleFor(guarantor => guarantor.Cpf).Must(x => Cpf.ValidCPF(x));
            RuleFor(guarantor => guarantor.Email).NotEmpty();
            RuleFor(guarantor => guarantor.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(guarantor => guarantor.GuarantorDocuments).SetCollectionValidator(new GuarantorDocumentsValidator());
        }
    }
}
