using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repositories.InterfacesRepo
{
    public interface ICategoryRepo : IGenericRepo<Category>
    {

            Task<IEnumerable<Category>> GetAllCategoriesByBranchIdAsync(int branchId);
    }
}
