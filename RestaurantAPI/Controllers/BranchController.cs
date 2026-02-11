using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS.Barnch;
using RestaurantService.Interfaces;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllBranches()
        {
            var branches = await _branchService.GetAllBranchesAsync();
            return Ok(branches);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateBranch([FromBody] AddBranchDTO branchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdBranch = await _branchService.CreateBranchAsync(branchDto);
            if (createdBranch == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating branch.");
            }
            return Ok(createdBranch);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBranchById(int id)
        {
            var branch = await _branchService.GetBranchByIdAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            return Ok(branch);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBranch(int id, [FromBody] UpdateBranchDTO branchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedBranch = await _branchService.UpdateBranchAsync(id, branchDto);
            if (updatedBranch == null)
            {
                return NotFound();
            }
            return Ok(updatedBranch);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult> DeleteBranch(int id)
        {
            var result = await _branchService.DeleteBranchAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();

        }
    }
}
