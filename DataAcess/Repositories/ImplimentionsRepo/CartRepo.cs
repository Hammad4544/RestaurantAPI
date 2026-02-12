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
    public class CartRepo : GenericRepo<Cart> , ICartRepo
    {
        private readonly RestaurantDbContext _dbContext;

        public CartRepo(RestaurantDbContext dbContext) : base(dbContext)
        {

            _dbContext = dbContext;
        }

        public async Task<Cart?> GetActiveCartByUserIdWithDetails(string userId)
        {
            return await _dbContext.Carts.Include(b => b.Branch).Include(i => i.CartItems).ThenInclude(i => i.MenuItem).ThenInclude(ii => ii.Images).FirstOrDefaultAsync(u=>u.UserId==userId&& u.IsActive);
        }

        public async Task<Cart> GetActiveCartByUserIdWithItems(string userId)
        {
            return await _dbContext.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId&&c.IsActive);
        }
    }
}
