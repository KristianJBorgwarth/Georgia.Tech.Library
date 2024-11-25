using GTL.Messaging.RabbitMq.Messages.OrderMessages;
using GTL.Messaging.RabbitMq.Producer;
using Microsoft.AspNetCore.Mvc;

namespace GTL.OrderService.API.Controllers;

[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    public IProducer<OrderProcessedMessage> _producer;

    public OrderController(IProducer<OrderProcessedMessage> producer)
    {
        _producer = producer;
    }

    [HttpPost]
    [Route("process-order")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ProcessOrder()
    {
        await _producer.PublishMessageAsync(new OrderProcessedMessage(100, "thisworks", Guid.NewGuid()));
        return Ok();
    }
}