using DataAcess.Repositories.InterfacesRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.ImplimentionsRepo
{
    public class GenericRepo<T>:IGenericRepo<T> where T : class
    {

        private readonly RestaurantDbContext _Dbcontext;
        private readonly DbSet<T> _DbSet;

        public GenericRepo(RestaurantDbContext dbContext)
        {

            _Dbcontext = dbContext;
            _DbSet = _Dbcontext.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _DbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _DbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await _DbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _DbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _DbSet.FindAsync(id);
        }

        public async Task<bool> SaveAsync()
        {

            return await _Dbcontext.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            _DbSet.Update(entity);
        }
    }
}
