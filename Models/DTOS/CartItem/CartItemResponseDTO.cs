using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.CartItem
{
    public class CartItemResponseDTO
    {
        public int CartItemId { get; set; }      // Id الخاص بالـ CartItem
        public int MenuItemId { get; set; }      // Id الخاص بالمنتج
        public string MenuItemName { get; set; } = string.Empty;  // اسم المنتج
        public string? ImageUrl { get; set; }    // أول صورة للمنتج
        public int Quantity { get; set; }        // الكمية
        public decimal UnitPrice { get; set; }   // سعر الوحدة
        public decimal ItemTotal { get; set; }   // TotalPrice = Quantity * UnitPrice
    }

}
