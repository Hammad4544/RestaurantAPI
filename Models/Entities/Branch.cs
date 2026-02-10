using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Models.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone {get; set;}
        public bool IsOpen { get; set;}= true;
        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        public ICollection<MenuItem> MenuItems { get; set; } = new HashSet<MenuItem>();
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<Cart> Carts { get; set; } = new HashSet<Cart>();

    }
}
