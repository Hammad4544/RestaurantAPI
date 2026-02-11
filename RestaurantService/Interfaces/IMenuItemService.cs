using Microsoft.EntityFrameworkCore.Query.Internal;
using Models.DTOS.MenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantService.Interfaces
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItemResponsDTO>> GetAllMenuItemsByBranchIdAsync(int branchId);
        
        Task<MenuItemResponsDTO> GetMenuItemByIdAsync(int menuItemId);
        Task<MenuItemResponsDTO> CreateMenuItemAsync(AddMenuItemDTO Dto);
        Task<bool> UpdateMenuItem(int id, AddMenuItemDTO dto);
        Task<bool> DeleteMenuItemAsync(int menuItemId);
    }
}
