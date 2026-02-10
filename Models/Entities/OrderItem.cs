using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
