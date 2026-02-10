using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
 
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;

        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public Payment Payment { get; set; } = null!;
    }

}
