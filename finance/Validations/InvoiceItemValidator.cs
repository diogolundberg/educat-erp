using finance.Models;
using FluentValidation;

namespace finance.Validations
{
    public class InvoiceItemValidator : AbstractValidator<Models.InvoiceItem>
    {
        public InvoiceItemValidator(DatabaseContext databaseContext)
        {
            RuleFor(invoice => invoice.Description).NotNull().WithName(Resources.Models.InvoiceItem.Description);
        }
    }
}
