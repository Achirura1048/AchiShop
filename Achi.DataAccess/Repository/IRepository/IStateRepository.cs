using Achi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achi.DataAccess.Repository.IRepository
{
    // IStateRepository.cs
    public interface IStateRepository : IRepository<State>
    {
        Task<State> GetOrAddAsync(string stateName, int countryId);

        Task<State> GetByCodeAsync(string code, int countryId);  // keep only if you have a Code property on State
    }


}
