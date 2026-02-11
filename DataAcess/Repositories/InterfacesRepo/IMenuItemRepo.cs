using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.InterfacesRepo
{
    public interface IMenuItemRepo : IGenericRepo<MenuItem>
    {
        Task <IEnumerable<MenuItem>> GetAllWithImages(int BranchId);
        Task <bool> removeMenuItemAsync(int id);
        Task<MenuItem> GetByIdWithImages(int id);
    }
}
