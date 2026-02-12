using Models.DTOS.CartItem;
using Models.DTOS.MenuItem;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.Cart
{
    public class CartResponseDTO
    {
        public int CartId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public List<CartItemResponseDTO> Items { get; set; } = new();

        public decimal SubTotal { get; set; }      // مجموع كل العناصر
        public decimal Tax { get; set; }           // ضريبة لو عندك
        public decimal DeliveryFee { get; set; }   // رسوم التوصيل لو عندك
        public decimal TotalAmount { get; set; }   // SubTotal + Tax + DeliveryFee
    }

}
