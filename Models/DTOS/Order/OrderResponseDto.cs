using Models.DTOS.OrderItem;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.Order
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new();
        public string PaymentMethod { get; set; } = null!;
        public string? user { get; set; }

    }
}
