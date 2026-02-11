using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantService.Interfaces;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemContorller : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemContorller(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }
        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetAllMenuItemsByBranchId(int branchId)
        {
            var menuItems = await _menuItemService.GetAllMenuItemsByBranchIdAsync(branchId);
            return Ok(menuItems);
        }
        [HttpGet("{menuItemId}")]
        public async Task<IActionResult> GetMenuItemById(int menuItemId)
        {
            var menuItem = await _menuItemService.GetMenuItemByIdAsync(menuItemId);
            return Ok(menuItem);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMenuItem([FromForm] Models.DTOS.MenuItem.AddMenuItemDTO dto)
        {
            var createdMenuItem = await _menuItemService.CreateMenuItemAsync(dto);
            return CreatedAtAction(nameof(GetMenuItemById), new { menuItemId = createdMenuItem.Id }, createdMenuItem);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItem(int id, [FromForm] Models.DTOS.MenuItem.AddMenuItemDTO dto)
        {
            var result = await _menuItemService.UpdateMenuItem(id, dto);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{menuItemId}")]
        public async Task<IActionResult> DeleteMenuItem(int menuItemId)
        {
            var result = await _menuItemService.DeleteMenuItemAsync(menuItemId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
