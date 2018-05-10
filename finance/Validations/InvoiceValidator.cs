using finance.Models;
using FluentValidation;

namespace finance.Validations
{
    public class InvoiceValidator : AbstractValidator<Models.Invoice>
    {
        public InvoiceValidator(DatabaseContext databaseContext)
        {
            RuleFor(invoice => invoice.InvoiceNumber).NotNull().WithName(Resources.Models.Invoice.InvoiceNumber);
            RuleFor(invoice => invoice.Value).NotNull().WithName(Resources.Models.Invoice.Value);
            RuleFor(invoice => invoice.DueDate).NotNull().WithName(Resources.Models.Invoice.DueDate);
        }
    }
}
