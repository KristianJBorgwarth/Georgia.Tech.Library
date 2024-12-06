using FluentValidation;
using GTL.Domain.Common;

namespace GTL.Customer.Application.Features.Customer.Commands.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Username)
            .SetValidator(new NameValidator(nameof(CreateCustomerCommand.Username)));

        RuleFor(x => x.FirstName)
            .SetValidator(new NameValidator(nameof(CreateCustomerCommand.FirstName)));

        RuleFor(x => x.LastName)
            .SetValidator(new NameValidator(nameof(CreateCustomerCommand.LastName)));

        RuleFor(x => x.Email)
            .NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(CreateCustomerCommand.Email)).Message)
            .NotEmpty().WithMessage(Errors.General.ValueIsEmpty(nameof(CreateCustomerCommand.Email)).Message);

        RuleFor(x => x.Password)
            .NotNull().WithMessage(Errors.General.ValueIsRequired(nameof(CreateCustomerCommand.Password)).Message)
            .NotEmpty().WithMessage(Errors.General.ValueIsEmpty(nameof(CreateCustomerCommand.Password)).Message);
    }
}

internal class NameValidator : AbstractValidator<string>
{
    public NameValidator(string propertyName)
    {
        RuleFor(x => x)
            .NotNull().WithMessage(Errors.General.ValueIsRequired(propertyName).Message)
            .NotEmpty().WithMessage(Errors.General.ValueIsEmpty(propertyName).Message)
            .MaximumLength(50).WithMessage(Errors.General.ValueTooLarge(propertyName, 50).Message)
            .MinimumLength(2).WithMessage(Errors.General.ValueTooSmall(propertyName, 2).Message);
    }
}