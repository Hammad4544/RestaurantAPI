using DataAcess.Repositories.InterfacesRepo;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.ImplimentionsRepo
{
    public class CartItemRepo:GenericRepo<CartItem>,ICartItemRepo
    {
        private readonly RestaurantDbContext _dbContext;

        public CartItemRepo(RestaurantDbContext dbContext) : base(dbContext)
        {

            _dbContext = dbContext;
        }
    }
}
