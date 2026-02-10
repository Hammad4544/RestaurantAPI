using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS.Category;
using RestaurantService.Interfaces;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {

            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id) {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] AddCategoryDTO categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);
            if (createdCategory == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating category.");
            }
            return Ok(createdCategory);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDTO categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedCategory = await _categoryService.UpdateCategoryAsync(id, categoryDto);
            if (updatedCategory == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating category.");
            }
            return Ok(updatedCategory);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var s = await _categoryService.DeleteCategoryAsync(id);
            if (!s)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting category.");
            }
            return NoContent();
        }
    }
}
