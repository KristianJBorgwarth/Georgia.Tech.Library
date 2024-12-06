using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.OrderService.Persistence.Abstractions
{
    public abstract class Entity
    {
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public void SetUpdatedTime()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDeletedTime()
        {
            DeletedAt = DateTime.UtcNow;
        }
    }
}
