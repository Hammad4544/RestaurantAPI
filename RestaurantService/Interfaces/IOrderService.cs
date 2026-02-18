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
        //Task<Order> GetOrderByIdAsync(Guid orderId);
        //Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId);
        Task<ServiceResult<string>> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);
    }
}
