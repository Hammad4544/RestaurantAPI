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


        public async Task<CartResponseDTO?> ClearCartAsync(string userId)
        {
            var cart = await _unitOfwork.Carts.GetActiveCartByUserIdWithItems(userId);
            if (cart == null) return null;
            _unitOfwork.CartItems.DeleteRange(cart.CartItems);
            await _unitOfwork.SaveAsync();
            return await GetCartAsync(userId);
        }

        public async Task<CartResponseDTO> GetCartAsync(string userId)
        {
            // 1️⃣ جلب الكارت
            var cart = await _unitOfwork.Carts
                .GetActiveCartByUserIdWithDetails(userId);

            // 2️⃣ لو مفيش كارت نشوف نرجع DTO فاضي
            if (cart == null)
            {
                return new CartResponseDTO
                {
                    Items = new List<CartItemResponseDTO>(),
                    SubTotal = 0,
                    Tax = 0,
                    DeliveryFee = 0,
                    TotalAmount = 0
                };
            }

            // 3️⃣ تحويل كل عنصر في CartItem ل DTO
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

            // 4️⃣ الحسابات
            var subTotal = items.Sum(i => i.ItemTotal);
            var tax = subTotal * 0.14m;          // مثال 14% ضريبة
            var deliveryFee = 30m;               // مثال رسوم توصيل ثابتة
            var totalAmount = subTotal + tax + deliveryFee;

            // 5️⃣ إعداد DTO النهائي
            var cartDto = new CartResponseDTO
            {
                CartId = cart.Id,
                BranchId = cart.BranchId,
                BranchName = cart.Branch.Name,
                CreatedAt = cart.CreatedAt,
                Items = items,
                SubTotal = subTotal,
                Tax = tax,
                DeliveryFee = deliveryFee,
                TotalAmount = totalAmount
            };

            return cartDto;
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
