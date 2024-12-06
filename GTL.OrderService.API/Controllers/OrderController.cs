using GTL.Messaging.RabbitMq.Messages.OrderMessages;
using GTL.Messaging.RabbitMq.Producer;
using GTL.OrderService.Persistence.Entities;
using GTL.OrderService.Persistence.Features.Order.Request;
using GTL.OrderService.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace GTL.OrderService.API.Controllers;

[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    public IProducer<OrderProcessedMessage> _producer;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderController(IProducer<OrderProcessedMessage> producer, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
    {
        _producer = producer;
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
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

    #region CRUD
    [HttpPost]
    [Route("Create-order")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        if (request == null)
        {
            return BadRequest("User data is required.");
        }
        var order = new Order(request.UserId, OrderStatus.Pending);

        await _orderRepository.CreateAsync(order);

        return Ok();
    }

    [HttpGet]
    [Route("Get-order")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Invalid ID provided.");
        }

        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
        {
            return NotFound("Order not found.");
        }

        return Ok(order);
    }

    [HttpGet]
    [Route("GetAll-order")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllOrders()
    {
        return Ok(await _orderRepository.GetAll());
    }

    [HttpPut]
    [Route("Update-order")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateOrder(UpdateOrderRequest request)
    {
        if (request == null)
        {
            return BadRequest("User data is required.");
        }

        return Ok();
    }

    [HttpDelete]
    [Route("Delete-order")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Invalid ID provided.");
        }
        await _orderRepository.DeleteAsync(id);
        return Ok();
    }

    #endregion

    #region OrderLine
    [HttpPost]
    [Route("AddItemToOrder")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddItemToOrder(AddOrderItemRequest request)
    {
        if (request == null)
        {
            return BadRequest("User data is required.");
        }

        var orderitem = new OrderItem(request.OrderId, request.BookId, request.BookTitle, request.Price, request.Quantity);

        await _orderItemRepository.AddAsync(orderitem);

        return Ok();
    }


    [HttpDelete]
    [Route("DeleteItemFromOrder")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteItemFromOrder(DeleteOrderFromItemRequest request)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderItemId);

        order.DeleteOrderItem(request.OrderItemId);

        await _orderRepository.UpdateAsync(order);

        return Ok();
    }

    [HttpGet]
    [Route("GetAllItemsFromOrder")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllItemsFromOrder(Guid orderId)
    {
        if (orderId == Guid.Empty)
        {
            return BadRequest("Invalid ID provided.");
        }

        return Ok(await _orderItemRepository.GetAllItemsFromOrder(orderId));
    }

    #endregion









}