using Models.DTOS.Barnch;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantService.Interfaces
{
    public interface IBranchService
    {
        Task<IEnumerable<ResponsAllBranchesDTO>> GetAllBranchesAsync();
        Task<ResponsAllBranchesDTO> GetBranchByIdAsync(int id);
        Task<AddBranchDTO> CreateBranchAsync(AddBranchDTO branch);
        Task<UpdateBranchDTO> UpdateBranchAsync(int id,UpdateBranchDTO branch);
        Task<bool> DeleteBranchAsync(int id);
        Task<bool> OpenBranchAsync(int id);
    }
}
