using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS.Barnch
{
    public class AddBranchDTO
    {
        [Required(ErrorMessage = "Branch name is required.")]
        [StringLength(100, ErrorMessage = "Branch name cannot exceed 100 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Branch address is required.")]
        [StringLength(200, ErrorMessage = "Branch address cannot exceed 200 characters.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Branch phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; }
    }
}
