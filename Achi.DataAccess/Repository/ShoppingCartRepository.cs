using Achi.DataAccess.Data;
using Achi.DataAccess.Repository.IRepository;
using Achi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achi.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly AppDbContext _db;
        public ShoppingCartRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ShoppingCart obj)
        {
            _db.ShoppingCarts.Update(obj);
        }
        public IEnumerable<ShoppingCart> GetAll(Func<object, bool> value)
        {
            return _db.ShoppingCarts.ToList();
        }
    }

}
