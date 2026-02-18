using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.ImplimentionsRepo
{
    public class OrderRepo : GenericRepo<Models.Entities.Order>, InterfacesRepo.IOrderRepo
    {
        private readonly RestaurantDbContext _context;

        public OrderRepo(RestaurantDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
