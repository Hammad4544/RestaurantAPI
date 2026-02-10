using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Models.Entities
{
    
    public class Category
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int BranchId {  get; set; }
        public Branch Branch { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<MenuItem> MenuItems { get; set; } = new HashSet<MenuItem>();

    }
}
