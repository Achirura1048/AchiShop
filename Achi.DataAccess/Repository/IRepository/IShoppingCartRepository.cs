using Achi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achi.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        IEnumerable<ShoppingCart> GetAll(Func<object, bool> value);
        void Update(ShoppingCart obj);
    }

}
