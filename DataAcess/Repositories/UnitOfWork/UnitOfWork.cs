using DataAcess.Repositories.ImplimentionsRepo;
using DataAcess.Repositories.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantDbContext _dbcontext;
        public IBranchRepo Branches { get; private set; }
        public ICategoryRepo Categories { get; private set; }



        public UnitOfWork(RestaurantDbContext dbContext)
        {
            _dbcontext = dbContext;
            Branches = new BranchRepo(_dbcontext);
            Categories = new CategoryRepo(_dbcontext);
        }

        

        public void Dispose()
        {
            _dbcontext.Dispose();
        }

        public async Task<bool> SaveAsync()
        {
           return await _dbcontext.SaveChangesAsync() > 0;
        }
    }
}
