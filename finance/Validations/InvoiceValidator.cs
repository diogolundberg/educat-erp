using finance.Models;
using FluentValidation;
using System;

namespace finance.Validations
{
    public class InvoiceValidator : AbstractValidator<Models.Invoice>
    {
        public InvoiceValidator(DatabaseContext databaseContext)
        {
            RuleFor(invoice => invoice.InvoiceNumber).NotNull().WithName(Resources.Models.Invoice.InvoiceNumber);
            RuleFor(invoice => invoice.Value).NotNull().WithName(Resources.Models.Invoice.Value);
            RuleFor(invoice => invoice.DueDate).NotNull().WithName(Resources.Models.Invoice.DueDate);
            RuleFor(invoice => invoice.DueDate).Custom((dueDate, context) =>
            {
                if (!string.IsNullOrEmpty(dueDate))
                {
                    try
                    {
                        DateTime.Parse(dueDate);
                    }
                    catch (Exception)
                    {
                        context.AddFailure("DueDate", string.Format(Resources.Shared.NotValidField, Resources.Models.Invoice.DueDate));
                    }
                }
            });
        }
    }
}
