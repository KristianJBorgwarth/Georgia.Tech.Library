using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GTL.Warehouse.Persistence.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Price { get; set; }

        //scalar properties
        public required Guid BookDetailsId { get; set; }
        public required Guid SellerId { get; set; }
      
    }
}
