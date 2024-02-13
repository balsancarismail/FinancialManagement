using FluentValidation;

namespace Application.Features.FinancialTransaction.Commands.Delete;

public class DeleteFinancialTransactionCommandValidator : AbstractValidator<DeleteFinancialTransactionCommand>
{
    public DeleteFinancialTransactionCommandValidator()
    {
        RuleFor(c => c.Id).NotNull().NotEmpty().GreaterThan(0);
    }
}