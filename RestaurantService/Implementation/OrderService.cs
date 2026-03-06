using DataAcess.Repositories.UnitOfWork;
using Models.DTOS.Order;
using Models.DTOS.OrderItem;
using Models.Entities;
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

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.Orders.GetAllOrders();
            if (orders == null || !orders.Any()) return Enumerable.Empty<OrderResponseDto>();
            return orders.Select(orders => new OrderResponseDto
            {
                user=orders.User.FullName ?? "Unknown User",
                BranchId = orders.BranchId,
                BranchName = orders.Branch.Name ?? "Unknown Branch",
                TotalPrice = orders.TotalPrice,
                OrderId = orders.Id,
                CreatedAt = orders.CreatedAt,
                Status = orders.Status.ToString(),
                Items = orders.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    MenuItemId = oi.MenuItemId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.Price,
                    MenuItemName = oi.MenuItem.Name ?? "Unknown Item",
                    ItemTotal = oi.Quantity * oi.Price,
                    ImageUrl = oi.MenuItem.Images.FirstOrDefault()?.ImageUrl
                }).ToList(),
                PaymentMethod = orders.Payment.Method.ToString() ?? "Cach"

            });
            

        }

        public async Task<OrderResponseDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetOrderWithDetailsAsync(orderId);
            if (order == null) return null!;
            var result =  new OrderResponseDto {
                OrderId=order.Id,
                BranchId = order.BranchId,
                Items = order.OrderItems.Select(oi => new OrderItemResponseDto {
                    MenuItemId = oi.MenuItemId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.Price,
                    MenuItemName = oi.MenuItem.Name,
                    ItemTotal = oi.Quantity * oi.Price,
                    ImageUrl =oi.MenuItem.Images.FirstOrDefault()?.ImageUrl 
                }).ToList(),
                BranchName = order.Branch.Name,
                CreatedAt = order.CreatedAt,
                PaymentMethod = order.Payment?.Method.ToString() ?? "Not Pait Yet",
                Status = order.Status.ToString(),
                TotalPrice = order.TotalPrice,
            };
            return  result;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrdersByCustomerIdAsync(string customerId)
        {
            var orders = await _unitOfWork.Orders.GetOrdersByUserIdAsync(customerId);
            if (orders == null || !orders.Any()) return Enumerable.Empty<OrderResponseDto>();
            return orders.Select(orders => new OrderResponseDto
            {
                BranchId = orders.BranchId,
                BranchName = orders.Branch.Name,
                TotalPrice = orders.TotalPrice,
                OrderId = orders.Id,
                CreatedAt = orders.CreatedAt,
                Status = orders.Status.ToString(),
                Items = orders.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    MenuItemId = oi.MenuItemId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.Price,
                    MenuItemName = oi.MenuItem.Name,
                    ItemTotal = oi.Quantity * oi.Price,
                    ImageUrl = oi.MenuItem.Images.FirstOrDefault()?.ImageUrl
                }).ToList(),
                PaymentMethod = orders.Payment.Method.ToString() ?? "Cach"

            });
        }

        public async Task<ServiceResult<string>> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null) return ServiceResult<string>.Failure("Order not found");
            order.Status = newStatus;
            if (newStatus == OrderStatus.Delivered)
            {
                 var payment = await _unitOfWork.payments.GetByOrderIdAsync(orderId);
                if(payment!=null && payment.Status != PaymentStatus.Paid)
                {
                    payment.Status = PaymentStatus.Paid;
                    payment.PaidAt = DateTime.UtcNow;

                }
            }
            await _unitOfWork.SaveAsync();
            return ServiceResult<string>.Ok("Order status updated successfully");
        }
    }
}
