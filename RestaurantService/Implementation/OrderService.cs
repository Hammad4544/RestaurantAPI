using DataAcess.Repositories.UnitOfWork;
using Models.Enums;
using RestaurantService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantService.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork) {
        
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceResult<string>> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null) return ServiceResult<string>.Failure("Order not found");
            order.Status = newStatus;
            await _unitOfWork.SaveAsync();
            return ServiceResult<string>.Ok("Order status updated successfully");
        }
    }
}
