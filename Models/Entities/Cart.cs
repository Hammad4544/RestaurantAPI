using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }

}
