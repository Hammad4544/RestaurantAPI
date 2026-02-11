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
    public class MenuItemRepo : GenericRepo<MenuItem>, IMenuItemRepo
    {
        private readonly RestaurantDbContext _context;

        public MenuItemRepo(RestaurantDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MenuItem>> GetAllWithImages(int Id)
        {
            return await _context.MenuItems.Include(m => m.Images).Where(m=>m.BranchId==Id).ToListAsync();
        }

        public async Task<MenuItem> GetByIdWithImages(int id)
        {
            return await _context.MenuItems.Include(m => m.Images).SingleOrDefaultAsync(m=>m.Id==id);
        }

        public async Task<bool> removeMenuItemAsync(int id)
        {
            if(id <= 0) return false;
            var r = await _context.MenuItems.SingleOrDefaultAsync(m=>m.Id==id);
            if (r==null)
            {
                return false;
            }
            r.IsAvailable = false;
            return true;
        }
    }
}
