using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class MenuItemImage
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; } = null!;

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; } = null!;
    }

}
