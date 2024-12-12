using FluentValidation;
using static GTL.Domain.Common.Errors.General;

namespace GTL.Customer.Application.Features.Customer.Commands.Delete;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(x=> x.Id)
            .NotEmpty().WithMessage(ValueIsEmpty(nameof(DeleteCustomerCommand.Id)).Message);
    }
}