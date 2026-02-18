using Models.DTOS.Cart;
using RestaurantService.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantService.Interfaces
{
    public interface ICartService
    {
         Task<CartResponseDTO?>  AddToCartAsync(string userId, AddTOCartDto dto);
        Task<CartResponseDTO> GetCartAsync(string userId);
        Task<CartResponseDTO?> RemoveCartItem(string userId, int cartItemId);
        Task<CartResponseDTO?> ClearCartAsync(string userId);
        Task<CartResponseDTO?> UpdateCartItemQuantityAsync(string userId,int cartItemId,int newQuantity);
        Task<ServiceResult<string>> CheckOut(string userId);
    }
}
