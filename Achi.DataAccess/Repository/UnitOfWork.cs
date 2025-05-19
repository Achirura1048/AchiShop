using Achi.DataAccess.Repository.IRepository;
using Achi.DataAccess.Data;
using Achi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Achi.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        public ICompanyRepository Company { get; private set; }

        public ICountryRepository Country { get; private set; }
        public IStateRepository State { get; private set; }
        public ICityRepository City { get; private set; }


        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            Country = new CountryRepository(_db);
            State = new StateRepository(_db);
            City = new CityRepository(_db);
        }


        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
