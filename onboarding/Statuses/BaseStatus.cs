using FluentValidation;
using onboarding.Models;

namespace onboarding.Statuses
{
    public abstract class BaseStatus<T> where T : BaseModel
    {
        public AbstractValidator<T> _validator { get; set; }
        public T _entity { get; set; }

        public BaseStatus(AbstractValidator<T> validator, T entity)
        {
            _validator = validator;
            _entity = entity;
        }

        public virtual string GetStatus()
        {
            FluentValidation.Results.ValidationResult results = _validator.Validate(_entity);

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
