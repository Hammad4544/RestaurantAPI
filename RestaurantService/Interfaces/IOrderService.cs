using Models.DTOS.Order;
using Models.Entities;
using Models.Enums;
using RestaurantService.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantService.Interfaces
{
    public interface IOrderService
    {
        //Task PlaceOrderAsync(Order order);
         Task<OrderResponseDto> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderResponseDto>> GetOrdersByCustomerIdAsync(string customerId);
        Task<ServiceResult<string>> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);
            Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
    }
}
