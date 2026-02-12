using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }   
        public decimal TotalPrice { get; set; } //  Quantity * UnitPrice
    }

}
