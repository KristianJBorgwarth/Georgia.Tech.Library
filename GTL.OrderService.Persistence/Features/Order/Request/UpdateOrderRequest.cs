using GTL.OrderService.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.OrderService.Persistence.Features.Order.Request
{
    public class UpdateOrderRequest
    {
        public Guid Id { get;  set; }
        public Guid UserId { get;  set; }
        public Guid? OrderItemId { get;  set; }
        public OrderStatus OrderStatus { get;  set; }
    }
}
