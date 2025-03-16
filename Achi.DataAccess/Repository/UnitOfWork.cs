using Achi.DataAccess.Repository.IRepository;
using E_Ticaret.Data;
using E_Ticaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achi.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
        }


        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
