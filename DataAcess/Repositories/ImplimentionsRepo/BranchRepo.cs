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
    public class BranchRepo : GenericRepo<Branch> ,  IBranchRepo 
    {
        private readonly RestaurantDbContext _dbContext;

        public BranchRepo(RestaurantDbContext dbContext) : base(dbContext) {
        
            _dbContext = dbContext;
        }
      
        public async Task ClosedBranchAsync(int id)
        {
          var br =  await _dbContext.Branches.FirstOrDefaultAsync(b => b.Id == id);
            if (br != null) {
                br.IsOpen = false;
            }
        }

        


        
    }
}
