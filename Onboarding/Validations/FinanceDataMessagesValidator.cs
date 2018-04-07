using FluentValidation;
using FluentValidation.Validators;
using Newtonsoft.Json;
using Onboarding.Enums;
using Onboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Onboarding.Validations
{
    public class FinanceDataMessagesValidator : AbstractValidator<FinanceData>
    {
        private List<GuarantorDocumentType> documentTypes { get; set; }

        public FinanceDataMessagesValidator(DatabaseContext databaseContext)
        {
            documentTypes = databaseContext.Set<GuarantorDocumentType>().Where(x => x.Validations == null).ToList();

            RuleFor(financeData => financeData).Custom((financeData, context) =>
            {
                databaseContext.Entry(financeData).Reference(x => x.Plan).Load();
                databaseContext.Entry(financeData).Collection(x => x.Guarantors).Load();
                databaseContext.Entry(financeData).Reference(x => x.Representative).Load();

                foreach (Guarantor guarantor in financeData.Guarantors)
                {
                    databaseContext.Entry(guarantor).Reference(x => x.Relationship).Load();
                    CheckGuarantors(guarantor, context);
                }

                CheckPlan(financeData, context);
                CheckRepresentative(financeData, context);
            });
        }

        private string BeSpouse(Guarantor guarantor)
        {
            if (guarantor.Relationship != null && guarantor.Relationship.CheckSpouse)
            {
                return DocumentValidations.Spouse.ToString();
            }
            return string.Empty;
        }

        private List<string> GetGuarantorDocumentValidations(List<Document> documents)
        {
            List<string> documentTypeValidations = new List<string>();

            foreach (Document document in documents)
            {
                if (!string.IsNullOrEmpty(document.DocumentType.Validations))
                {
                    List<string> documentValidations = JsonConvert.DeserializeObject<List<string>>(document.DocumentType.Validations);
                    documentTypeValidations.AddRange(documentValidations.Where(x => !documentTypeValidations.Contains(x)));
                }
            }

            return documentTypeValidations;
        }

        private void CheckPlan(FinanceData financeData, CustomContext context)
        {
            if (financeData.Plan != null && financeData.Plan.Guarantors > 0)
            {
                if (financeData.Plan.Guarantors > financeData.Guarantors.Count)
                {
                    context.AddFailure(string.Format("Para o plano {0} é necessário preencher {1} fiador(es)", financeData.Plan.Name, financeData.Plan.Guarantors));
                }
            }
        }

        private void CheckGuarantors(Guarantor guarantor, CustomContext context)
        {
            List<string> validations = new List<string>();
            validations.Add(BeSpouse(guarantor));
            validations = validations.Where(x => !string.IsNullOrEmpty(x)).ToList();

            List<Document> documents = guarantor.GuarantorDocuments.Select(x => x.Document).ToList();
            List<string> documentTypeValidations = GetGuarantorDocumentValidations(documents);
            List<string> requiredDocumentValidations = validations.Where(x => !documentTypeValidations.Contains(x)).ToList();
            List<string> requiredDocumentTypes = documentTypes.Where(x => !documents.Any(o => o.DocumentTypeId == x.Id)).Select(x => x.Name).ToList();

            foreach (string requiredDocument in requiredDocumentValidations)
            {
                context.AddFailure("guarantors[0]", GetMessageError(requiredDocument));
            }
            foreach (string documentType in requiredDocumentTypes)
            {
                context.AddFailure("guarantors[0]", string.Format("Documento {0} é obrigatório.", documentType));
            }
        }

        private void CheckRepresentative(FinanceData financeData, CustomContext context)
        {
            PersonalData personalData = financeData.Enrollment.PersonalData;
            Representative representative = financeData.Representative;

            if (personalData.BirthDate.HasValue && GetAge(personalData.BirthDate.Value) > 18)
            {
                bool isDiff = false;

                if (representative is RepresentativePerson)
                {
                    isDiff = representative.Name != personalData.RealName ? true : isDiff;
                    isDiff = representative.StreetAddress != personalData.StreetAddress ? true : isDiff;
                    isDiff = representative.Neighborhood != personalData.Neighborhood ? true : isDiff;
                    isDiff = representative.PhoneNumber != personalData.PhoneNumber ? true : isDiff;
                    isDiff = representative.Landline != personalData.Landline ? true : isDiff;
                    isDiff = representative.Email != personalData.Email ? true : isDiff;
                    isDiff = representative.CityId != personalData.CityId ? true : isDiff;
                    isDiff = representative.StateId != personalData.StateId ? true : isDiff;
                    isDiff = ((RepresentativePerson)representative).Cpf != personalData.CPF ? true : isDiff;

                }

                if (isDiff)
                {
                    context.AddFailure("Dados do responsável não são os mesmos dos dados pessoais.");
                }
            }
        }

        private int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            if (birthDate > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        private string GetMessageError(string validation)
        {
            if (validation == DocumentValidations.Spouse.ToString())
            {
                return "Certidão de casamento obrigatória.";
            }

            return string.Empty;
        }
    }
}
