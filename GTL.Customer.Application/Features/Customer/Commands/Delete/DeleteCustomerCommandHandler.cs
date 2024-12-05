using GTL.Application.Abstractions;
using GTL.Application.Contracts;
using GTL.Customer.Application.Contracts;
using GTL.Customer.Domain.Services;
using GTL.Domain.Common;
using Microsoft.Extensions.Logging;
using static GTL.Domain.Common.Errors.General;

namespace GTL.Customer.Application.Features.Customer.Commands.Delete;

public class DeleteCustomerCommandHandler(
    ICustomerRepository customerRepository,
    ICustomerLifeCycleService customerLifeCycleService,
    IUnitOfWork unitOfWork,
    ILogger<DeleteCustomerCommandHandler> logger)
    : ICommandHandler<DeleteCustomerCommand>
{
    public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await customerRepository.GetByIdAsync(request.Id, cancellationToken);
            if (customer is null)
            {
                logger.LogWarning("Customer with id {Id} was not found", request.Id);
                return Result.Fail(NotFound(request.Id));
            }

            customerLifeCycleService.MarkCustomerForDeletion(customer);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}