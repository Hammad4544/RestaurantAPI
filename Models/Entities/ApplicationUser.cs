using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Models.Entities
{
    public class ApplicationUser : IdentityUser
    {   
        public string FullName { get; set; } = null!;
       

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<Cart> Carts { get; set; } = new HashSet<Cart>();
    }

}
