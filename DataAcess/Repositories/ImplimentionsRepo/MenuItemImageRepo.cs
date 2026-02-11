using DataAcess.Repositories.InterfacesRepo;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.ImplimentionsRepo
{
    public class MenuItemImageRepo : GenericRepo<MenuItemImage>, IMenuItemImageRepo
    {
        private readonly RestaurantDbContext _dbcontext;

        public MenuItemImageRepo(RestaurantDbContext context) : base(context)
        {
            _dbcontext = context;
        }
    }
}
