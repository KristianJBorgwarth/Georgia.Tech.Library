using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.OrderService.Persistence.Features.Order.Request
{
    public class DeleteOrderFromItemRequest
    {
        public Guid OrderItemId { get; set; }

        public Guid BookID { get; set; }

        public decimal Quantity { get; set; }
    }
}
