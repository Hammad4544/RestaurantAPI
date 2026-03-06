using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.InterfacesRepo
{
    public interface IOrderRepo : IGenericRepo<Models.Entities.Order>
    {
        Task<Order?> GetOrderWithDetailsAsync(int orderId);
         Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);

        Task<IEnumerable<Order>> GetAllOrders();
    }
}
