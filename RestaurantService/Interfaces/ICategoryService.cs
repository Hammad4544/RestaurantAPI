using Models.DTOS.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantService.Interfaces
{
    public interface ICategoryService
    {
        //Task<IEnumerable<ResponsCategoryDTO>> GetAllCategoriesByBranchIdAsync(int branchId);
        Task<IEnumerable<ResponsCategoryDTO>> GetAllCategories();  
        Task<ResponsCategoryDTO> GetCategoryByIdAsync(int categoryId);
        Task<ResponsCategoryDTO> CreateCategoryAsync(AddCategoryDTO category);
        Task<ResponsCategoryDTO> UpdateCategoryAsync(int categoryId, UpdateCategoryDTO category);
        Task<bool> DeleteCategoryAsync(int categoryId);

    }
}
