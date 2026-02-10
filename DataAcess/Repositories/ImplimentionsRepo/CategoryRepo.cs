using DataAcess.Repositories.InterfacesRepo;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.Repositories.ImplimentionsRepo
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        private readonly RestaurantDbContext _dbContext;
        public CategoryRepo(RestaurantDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesByBranchIdAsync(int branchId)
        {
            return await _dbContext.Categories.Where(c => c.BranchId == branchId).ToListAsync();
        }
    }
}
