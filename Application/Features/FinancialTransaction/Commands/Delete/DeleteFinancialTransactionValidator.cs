using FluentValidation;

namespace Application.Features.FinancialTransaction.Commands.Delete;

public class DeleteFinancialTransactionValidator : AbstractValidator<DeleteFinancialTransactionCommand>
{
    public DeleteFinancialTransactionValidator()
    {
        RuleFor(c => c.Id).NotNull().NotEmpty().GreaterThan(0);
    }
}