using GTL.Customer.API.Base;
using GTL.Customer.Application.Features.Customer.Commands.Create;
using GTL.Customer.Application.Features.Customer.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GTL.Customer.API.Controllers;

[Route("api/[controller]")]
public class CustomerController(ISender mediator) : BaseController
{
    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(CreateCustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
        var result = await mediator.Send(command);
        return result.Success ? Ok(result.Value) : Error(result.Error);
    }

    [HttpDelete]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteCustomer([FromQuery] DeleteCustomerCommand command)
    {
        var result = await mediator.Send(command);
        return result.Success ? Ok() : Error(result.Error);
    }
}