using FluentValidation;
using onboarding.ViewModels.Enrollments;
using System;

namespace onboarding.Statuses
{
    public class InvoiceStatus : BaseStatus<Invoice>
    {
        public InvoiceStatus(AbstractValidator<Invoice> validator, Invoice entity) : base(validator, entity)
        {
        }

        public override string GetStatus()
        {
            if (_entity != null && _entity.Compensated)
            {
                return "approved";
            }
            else if (_entity != null && !_entity.Compensated && DateTime.Parse(_entity.DueDate) >= DateTime.Now)
            {
                return "pending";
            }
            else if (_entity != null && !_entity.Compensated && DateTime.Now >= DateTime.Parse(_entity.DueDate))
            {
                return "expired";
            }
            else
            {
                return "inReview";
            }
        }

        public string GetStatusName()
        {
            string status = GetStatus();

            switch (status)
            {
                case "approved":
                    return "Pago";
                case "pending":
                    return "Pendente";
                default:
                    return "Não pago";
            }
        }
    }
}
