using Entity.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contract
{
   public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoryByAsync();

    }
}
