using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.ImplimentionsRepo
{
    public class OrderRepo : GenericRepo<Models.Entities.Order>, InterfacesRepo.IOrderRepo
    {
        private readonly RestaurantDbContext _context;

        public OrderRepo(RestaurantDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders.
                Include(b=>b.Branch)
                .Include(it=>it.OrderItems)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

        }

        public async Task<Order?> GetOrderWithDetailsAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Branch)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                    .ThenInclude(im=>im.Images)
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.Id == orderId);

        }
    }
}
