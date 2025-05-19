using Achi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achi.DataAccess.Repository.IRepository
{
    public interface ICountryRepository : IRepository<Country> {

        Task<Country> GetOrAddAsync(string iso2, string name);
        Task<Country> GetByCodeAsync(string code);

        void Update(Country entity);  // Add this



    }



}
