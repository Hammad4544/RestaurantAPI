using DataAcess.Repositories.InterfacesRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IBranchRepo Branches { get; }
        ICategoryRepo Categories { get; }
        IMenuItemRepo MenuItems { get; }
        IMenuItemImageRepo MenuItemImages { get; }
        ICartRepo Carts { get; }
        ICartItemRepo CartItems { get; }
        IOrderRepo Orders { get; }
        //IOrderItemRepo OrderItemRepo { get; }
        Task<bool> SaveAsync();
    }
}
