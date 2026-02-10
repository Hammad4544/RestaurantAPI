using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Enums
{
    public enum UserRole
    {
        Admin = 1,
        User = 2
    }

    public enum OrderStatus
    {
        Pending = 1,
        Paid,
        Preparing,
        OnTheWay,
        Delivered,
        Cancelled
    }

    public enum PaymentMethod
    {
        Cash = 1,
        VodafoneCash,
        InstaPay
    }

    public enum PaymentStatus
    {
        Pending = 1,
        Paid,
        Failed
    }

}
