using onboarding.Models;
using onboarding.Validations.PersonalData;

namespace onboarding.Statuses
{
    public class PersonalDataStatus : BaseStatus<PersonalData>
    {
        public PersonalDataStatus(PersonalDataValidator validator, PersonalData entity) : base(validator, entity)
        {
        }

        public override string GetStatus()
        {
            FluentValidation.Results.ValidationResult results = _validator.Validate(_entity);

            if (!_entity.UpdatedAt.HasValue)
            {
                return "empty";
            }
            if (results.IsValid)
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
