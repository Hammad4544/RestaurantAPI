using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.ImplimentionsRepo
{
    public class PaymentRepo : GenericRepo<Models.Entities.Payment>, InterfacesRepo.IPaymentRepo
    {
        private readonly RestaurantDbContext _context;

        public PaymentRepo(RestaurantDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Payment> GetByOrderIdAsync(int orderId)
        {
             
             return  await _context.Payments.FirstOrDefaultAsync(o => o.Order.Id == orderId);


        }
    }
}
