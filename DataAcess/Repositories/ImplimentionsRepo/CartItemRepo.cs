using DataAcess.Repositories.InterfacesRepo;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.ImplimentionsRepo
{
    public class CartItemRepo:GenericRepo<CartItem>,ICartItemRepo
    {
        private readonly RestaurantDbContext _dbContext;

        public CartItemRepo(RestaurantDbContext dbContext) : base(dbContext)
        {

            _dbContext = dbContext;
        }

        public void DeleteRange(ICollection<CartItem> cartItems)
        {
            _dbContext.CartItems.RemoveRange(cartItems);
        }

        public async Task<CartItem> FindByUserId(int Itemid, string userId)
        {
          return await _dbContext.CartItems.FirstOrDefaultAsync(i => i.Id == Itemid && i.Cart.UserId == userId && i.Cart.IsActive);
        }
    }
}
