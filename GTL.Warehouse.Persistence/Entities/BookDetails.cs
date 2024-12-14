using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GTL.Warehouse.Persistence.Entities
{
    public class BookDetails
    {
        public Guid Id { get; set; }
        public required string ISBN { get; set; }
        public required string Publisher { get; set; }
        public required DateTime PublishedDate { get; set; }

    }
}
