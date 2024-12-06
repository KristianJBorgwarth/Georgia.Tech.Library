using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Warehouse.Persistence.Entities.Book
{
    public class Book
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required int Quantity { get; set; }
        public required string Price { get; set; }
        public Guid UserId { get; set; }

        
    }
}
