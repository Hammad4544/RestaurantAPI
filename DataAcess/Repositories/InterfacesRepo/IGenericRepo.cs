using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.InterfacesRepo
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveAsync();
    }
}
