using FluentValidation;

namespace GTL.Customer.Application.Features.Customer.Commands.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        
    }
}