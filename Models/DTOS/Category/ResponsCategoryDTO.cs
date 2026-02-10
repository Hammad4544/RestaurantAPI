using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.Category
{
    public class ResponsCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public bool IsActive { get; set; }
    }
}
