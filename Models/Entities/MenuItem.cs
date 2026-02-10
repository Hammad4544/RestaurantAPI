using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Models.Entities
{
    public class MenuItem
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public ICollection<MenuItemImage> Images { get; set; } = new HashSet<MenuItemImage>();
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
        public bool IsAvailable { get; set; }
    }
}
