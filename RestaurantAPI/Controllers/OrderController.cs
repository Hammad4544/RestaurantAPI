using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantService.Interfaces;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

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

    }
}
