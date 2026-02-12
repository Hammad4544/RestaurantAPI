using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.Cart
{
    public class AddTOCartDto
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}
