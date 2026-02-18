using DataAcess.Repositories.UnitOfWork;
using Models.DTOS.Cart;
using Models.DTOS.CartItem;
using Models.Entities;
using RestaurantService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantService.Implementation
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfwork;

        public CartService(IUnitOfWork unitOfWork ) {
        
            _unitOfwork = unitOfWork;
        }
        public async Task<CartResponseDTO?> AddToCartAsync(string userId, AddTOCartDto dto)
        {
            if (dto.Quantity <= 0)
                return null;

            var menuItem = await _unitOfwork.MenuItems
                .GetByIdAsync(dto.MenuItemId);

            if (menuItem == null || !menuItem.IsAvailable)
                return null;

            var cart = await _unitOfwork.Carts
                .GetActiveCartByUserIdWithItems(userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    BranchId = menuItem.BranchId,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                };

                await _unitOfwork.Carts.AddAsync(cart);
                await _unitOfwork.SaveAsync();
            }
            else if (cart.BranchId != menuItem.BranchId)
            {
                return null;
            }

            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.MenuItemId == dto.MenuItemId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
                existingItem.TotalPrice =
                    existingItem.Quantity * existingItem.UnitPrice;
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    MenuItemId = menuItem.Id,
                    Quantity = dto.Quantity,
                    UnitPrice = menuItem.Price,
                    TotalPrice = dto.Quantity * menuItem.Price
                };

                await _unitOfwork.CartItems.AddAsync(cartItem);
            }

            await _unitOfwork.SaveAsync();

            return await GetCartAsync(userId);
        }

        public async Task<ServiceResult<string>> CheckOut(string userId)
        {
            var cart = await _unitOfwork.Carts.GetActiveCartByUserIdWithItems(userId);

            // 1. Validations
            if (cart == null) return ServiceResult<string>.Failure("No active cart found.");
            if (!cart.CartItems.Any()) return ServiceResult<string>.Failure("Cart is empty.");

            // 2. Calculation 
            decimal totalAmount = cart.CartItems.Sum(item => item.TotalPrice);

            // 3. Mapping Order
            var order = new Order
            {
                UserId = userId,
                BranchId = cart.BranchId,
                TotalPrice = totalAmount,
                Status = Models.Enums.OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    MenuItemId = ci.MenuItemId,
                    Quantity = ci.Quantity,
                    Price = ci.UnitPrice
                }).ToList()
            };

            try
            {
                // 4. Database Operations
                await _unitOfwork.Orders.AddAsync(order);

                // مسح العناصر وإغلاق الكارت
                _unitOfwork.CartItems.DeleteRange(cart.CartItems);
                cart.IsActive = false;

                // تنفيذ الكل في Transaction واحدة بفضل الـ Unit of Work
                await _unitOfwork.SaveAsync();

                return ServiceResult<string>.Ok($"Order #{order.Id} placed successfully!");
            }
            catch (Exception ex)
            {
                // لو حصل أي خطأ في الداتا بيز
                return ServiceResult<string>.Failure("An error occurred while processing your order.");
            }
        }

        public async Task<CartResponseDTO?> ClearCartAsync(string userId)
        {
            var cart = await _unitOfwork.Carts.GetActiveCartByUserIdWithItems(userId);
            if (cart == null) return null;
            _unitOfwork.CartItems.DeleteRange(cart.CartItems);
            await _unitOfwork.SaveAsync();
            return await GetCartAsync(userId);
        }

        private const decimal VAT_RATE = 0.14m; // 14% ضريبة
        private const decimal FIXED_DELIVERY_FEE = 30.0m;

        public async Task<CartResponseDTO> GetCartAsync(string userId)
        {
            var cart = await _unitOfwork.Carts.GetActiveCartByUserIdWithDetails(userId);

            if (cart == null) return new CartResponseDTO { Items = new List<CartItemResponseDTO>() };

            var items = cart.CartItems.Select(item => new CartItemResponseDTO
            {
                CartItemId = item.Id,
                MenuItemId = item.MenuItemId,
                MenuItemName = item.MenuItem.Name,
                ImageUrl = item.MenuItem.Images.FirstOrDefault()?.ImageUrl,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                ItemTotal = item.Quantity * item.UnitPrice
            }).ToList();

            var subTotal = items.Sum(i => i.ItemTotal);

            // الحسابات بقت أنضف هنا
            var tax = subTotal * VAT_RATE;
            var totalAmount = subTotal + tax + FIXED_DELIVERY_FEE;

            return new CartResponseDTO
            {
                CartId = cart.Id,
                BranchId = cart.BranchId,
                BranchName = cart.Branch.Name,
                CreatedAt = cart.CreatedAt,
                Items = items,
                SubTotal = subTotal,
                Tax = tax,
                DeliveryFee = FIXED_DELIVERY_FEE,
                TotalAmount = totalAmount
            };
        }
        public async Task<CartResponseDTO?> RemoveCartItem(string userId, int cartItemId)
        {
            var cartItem = await _unitOfwork.CartItems.FindByUserId(cartItemId, userId);

            if (cartItem == null)
            {
                return null;
            }

            _unitOfwork.CartItems.Delete(cartItem);

   
            var result = await _unitOfwork.SaveAsync();


            return await GetCartAsync(userId);

        }
        public async Task<CartResponseDTO?> UpdateCartItemQuantityAsync(
    string userId,
    int cartItemId,
    int newQuantity)
        {
            if (newQuantity < 0)
                return null;

            var cartItem = await _unitOfwork.CartItems
                .FindByUserId(cartItemId, userId);

            if (cartItem == null)
                return null;

            if (newQuantity == 0)
            {
                _unitOfwork.CartItems.Delete(cartItem);
            }
            else
            {
                cartItem.Quantity = newQuantity;
                cartItem.TotalPrice =
                    cartItem.UnitPrice * newQuantity;
            }

            await _unitOfwork.SaveAsync();

            return await GetCartAsync(userId);
        }

    }
}
