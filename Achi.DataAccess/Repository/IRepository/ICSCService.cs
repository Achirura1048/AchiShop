using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achi.DataAccess.Repository.IRepository
{
    public interface ICSCService
    {
        Task<IEnumerable<SelectListItem>> GetCountriesAsync();
        Task<IEnumerable<SelectListItem>> GetStatesAsync(string countryCodeIso2);
        Task<IEnumerable<SelectListItem>> GetCitiesAsync(string countryCodeIso2, string stateCode);


    }
}
