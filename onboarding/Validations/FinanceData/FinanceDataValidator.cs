using FluentValidation;
using Newtonsoft.Json;
using onboarding.Enums;
using onboarding.Models;
using System.Collections.Generic;
using System.Linq;

namespace onboarding.Validations.FinanceData
{
    public class FinanceDataValidator : AbstractValidator<Models.FinanceData>
    {
        private List<GuarantorDocumentType> _documentTypes { get; set; }

        public FinanceDataValidator(DatabaseContext databaseContext)
        {
            _documentTypes = databaseContext.Set<GuarantorDocumentType>().ToList();

            RuleFor(financeData => financeData.Representative).SetValidator(new RepresentativeValidator());
            RuleFor(financeData => financeData.Guarantors).SetCollectionValidator(new GuarantorValidator());

            RuleFor(financeData => financeData.PlanId).NotNull().WithName(Resources.Models.FinanceData.PlanId);
            RuleFor(financeData => financeData.PaymentMethodId).NotNull().WithName(Resources.Models.FinanceData.PaymentMethodId);

            RuleFor(financeData => financeData.Guarantors).Custom((guarantors, context) =>
            {
                for (int i = 0; i < guarantors.Count; i++)
                {
                    Guarantor guarantor = guarantors.ToArray()[i];

                    foreach (GuarantorDocumentType documentType in _documentTypes)
                    {
                        List<string> errors = Validate(guarantor, documentType);

                        foreach (string error in errors)
                        {
                            context.AddFailure(string.Format("guarantors[{1}].documents.{0}", documentType.Id, i), string.Format(Resources.Shared.RequiredDocumentMessage, documentType.Name));
                        }
                    }
                }
            });
        }

        public List<string> Validate(Models.Guarantor guarantor, DocumentType documentType)
        {
            List<string> errors = new List<string>();
            List<string> validations = documentType.Validations != null ? JsonConvert.DeserializeObject<List<string>>(documentType.Validations) : new List<string>();

            if (documentType.Validations == null)
            {
                if (!guarantor.GuarantorDocuments.Any(x => x.Document.DocumentTypeId == documentType.Id))
                {
                    errors.Add(string.Format(onboarding.Resources.Shared.RequiredDocumentMessage, documentType.Name));
                }
            }

            foreach (string validation in validations)
            {
                if (validation == DocumentValidations.Spouse.ToString())
                {
                    if (!CheckSpouse(guarantor))
                    {
                        errors.Add(GetMessageError(validation));
                    }
                }
            }

            return errors;
        }

        private bool CheckSpouse(Guarantor guarantor)
        {
            return (guarantor.Relationship != null && guarantor.Relationship.CheckSpouse && guarantor.GuarantorDocuments.Any(x => x.Document.DocumentTypeId != null && _documentTypes.SingleOrDefault(o => o.Id == x.Document.DocumentTypeId).ValidationList.Contains(DocumentValidations.Spouse.ToString()))) || guarantor.Relationship == null || !guarantor.Relationship.CheckSpouse;
        }

        private string GetMessageError(string validation)
        {
            if (validation == DocumentValidations.Spouse.ToString())
            {
                return string.Format(onboarding.Resources.Shared.RequiredDocumentMessage, onboarding.Resources.Enums.DocumentValidations.Spouse);
            }

            return string.Empty;
        }
    }
}
