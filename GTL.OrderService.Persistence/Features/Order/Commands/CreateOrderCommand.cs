using GTL.OrderService.Persistence.Features.Order.Request;
using GTL.OrderService.Persistence.Repositories;
using GTL.OrderService.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.OrderService.Persistence.Features.Order.Commands
{
    public class CreateOrderCommand
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommand(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        //public async Task<IAsyncResult> Handle(CreateOrderRequest request)
        //{
        //    Order order = new Order(request.UserId);
        //    var command = await _orderRepository.CreateAsync(order);
        //    return command.status;
        //}

    }
}
