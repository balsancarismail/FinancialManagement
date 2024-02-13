using FluentValidation;

namespace Application.Features.FinancialTransaction.Commands.Update;

public class UpdateFinancialTransactionCommandValidator : AbstractValidator<UpdateFinancialTransactionCommand>
{
    public UpdateFinancialTransactionCommandValidator()
    {
        RuleFor(c => c.Id).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(c => c.Amount).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(c => c.Date).NotNull().NotEmpty();
        RuleFor(c => c.Description).NotNull().NotEmpty();
        RuleFor(c => c.CategoryId).NotNull().NotEmpty().GreaterThan(0);
    }
}