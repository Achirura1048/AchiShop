using Achi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achi.DataAccess.Repository.IRepository
{
    public interface ICityRepository : IRepository<City> {

        Task<City> GetOrAddAsync(string name, int stateId);
        Task<City> GetByNameAsync(string name, int stateId);


    }



}
