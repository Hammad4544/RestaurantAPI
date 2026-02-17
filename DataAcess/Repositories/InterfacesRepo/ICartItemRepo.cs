using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.InterfacesRepo
{
    public interface ICartItemRepo : IGenericRepo<CartItem>
    {
        void DeleteRange(ICollection<CartItem> cartItems);
        Task<CartItem> FindByUserId(int Itemid,string userId);
    }
}
