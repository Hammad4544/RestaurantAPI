using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS.Cart;
using RestaurantService.Implementation;
using RestaurantService.Interfaces;
using System.Security.Claims;

namespace RestaurantAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        private string? GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserId();

            var result = await _cartService.GetCartAsync(userId);

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddTOCartDto dto)
        {
            var userId = GetUserId();

            var result = await _cartService.AddToCartAsync(userId, dto);

            if (result == null)
                return BadRequest("Unable to add item to cart.");

            return Ok(result);
        }

        [HttpPut("update/{cartItemId}")]
        public async Task<IActionResult> UpdateQuantity(
            int cartItemId,
            [FromQuery] int quantity)
        {
            var userId = GetUserId();

            var result = await _cartService
                .UpdateCartItemQuantityAsync(userId, cartItemId, quantity);

            if (result == null)
                return BadRequest("Unable to update item.");

            return Ok(result);
        }

        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            var userId = GetUserId();

            var result = await _cartService
                .RemoveCartItem(userId, cartItemId);

            if (result == null)
                return NotFound("Item not found.");

            return Ok(result);
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = GetUserId();

            var result = await _cartService.ClearCartAsync(userId);

            if (result == null)
                return BadRequest("Cart not found.");

            return Ok(result);
        }
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var userId = GetUserId();

            var result = await _cartService.CheckOut(userId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
 
    }
}

