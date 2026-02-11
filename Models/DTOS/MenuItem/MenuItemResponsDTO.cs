using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.MenuItem
{
    public class MenuItemResponsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public List<string> Images { get; set; } = new();
        public int CategoryId { get; set; }

        public int BranchId { get; set; }

        public bool IsAvailable { get; set; }
    }
}
