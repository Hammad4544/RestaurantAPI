using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantService.Interfaces;
using System.Security.Claims;

namespace RestaurantAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private string? GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] Models.Enums.OrderStatus newStatus)
        {
            var result = await _orderService.UpdateOrderStatusAsync(orderId, newStatus);
            if (result.Success)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }
        [HttpGet("OrderId/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound();
            return Ok(order);
        }
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetOrdersByCustomerId()
        {
            var customerId = GetUserId();
            if (customerId == null) return Unauthorized();
            var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
            return Ok(orders);
        }
        [HttpGet("admin/all-orders")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);


        }
        [HttpPatch("CancelOrder/{orderId}")]
        
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var userid = GetUserId();
            var result = await _orderService.CancelOrderAsync(orderId,userid);
            if (result.Success)
                return Ok(result.Message);
            return BadRequest(result.Message);
        }
    }
}
