using Achi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achi.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        ICountryRepository Country { get; }
        IStateRepository State { get; }
        ICityRepository City { get; }

        Task SaveAsync();

        void Save();
    }
}
