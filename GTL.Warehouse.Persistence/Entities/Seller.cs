using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTL.Warehouse.Persistence.Entities;

namespace GTL.Warehouse.Persistence.Entities
{
    public class Seller
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();

        // Computed property to derive total quantity of books
        public int TotalBooks => Books.Count;
    }
}
