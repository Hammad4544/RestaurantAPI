using Models.DTOS.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantService.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddToCartAsync(string userId, AddTOCartDto dto);
        Task<CartResponseDTO> GetCartAsync(string userId);
    }
}
