using GTL.Warehouse.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Warehouse.Persistence.DTO
{
    public class CreateBookDTO
    {
        public string Title { get; set; }
        public string Price { get; set; }
        public Guid SellerId { get; set; }
        public Guid BookDetailsId { get; set; }
        

    }
}
