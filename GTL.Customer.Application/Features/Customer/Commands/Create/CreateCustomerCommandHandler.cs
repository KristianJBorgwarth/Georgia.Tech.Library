using GTL.Application.Abstractions;
using GTL.Application.Contracts;
using GTL.Customer.Application.Contracts;
using GTL.Domain.Common;
using Microsoft.Extensions.Logging;

namespace GTL.Customer.Application.Features.Customer.Commands.Create;

public class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    ILogger<CreateCustomerCommandHandler> logger)
    : ICommandHandler<CreateCustomerCommand, CreateCustomerDto>
{
    public async Task<Result<CreateCustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await customerRepository.Exists(request.Email);
            if (exists)
            {
                return Result.Fail<CreateCustomerDto>(Errors.General.UnspecifiedError("Customer with this email already exists"));
            }

            var customer = Domain.Aggregates.Customer.Create(request.Username, request.FirstName, request.LastName, request.Email, request.Password);

            if (customer.Success is false)
            {
                return Result.Fail<CreateCustomerDto>(customer.Error);
            }

            var newCustomer = await customerRepository.AddAsync(customer.Value!, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = CreateCustomerDto.Map(newCustomer);

            return Result.Ok(dto);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error occurred while creating a customer");
            return Result.Fail<CreateCustomerDto>(Errors.General.UnspecifiedError("An exception occurred while creating a customer"));
        }
    }
}