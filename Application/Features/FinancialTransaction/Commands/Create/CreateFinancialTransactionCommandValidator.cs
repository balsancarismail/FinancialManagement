using FluentValidation;

namespace Application.Features.FinancialTransaction.Commands.Create;

public class CreateFinancialTransactionCommandValidator : AbstractValidator<CreateFinancialTransactionCommand>
{
    public CreateFinancialTransactionCommandValidator()
    {
        RuleFor(c => c.Amount).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(c => c.Date).NotNull().NotEmpty();
        RuleFor(c => c.Description).NotNull().NotEmpty();
        RuleFor(c => c.CategoryId).NotNull().NotEmpty().GreaterThan(0);
    }
}