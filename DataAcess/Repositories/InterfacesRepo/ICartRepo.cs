using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.InterfacesRepo
{
    public interface ICartRepo : IGenericRepo<Cart>
    {
       Task<Cart> GetActiveCartByUserIdWithItems(string userId);
        Task<Cart?> GetActiveCartByUserIdWithDetails(string userId);
    }
}
