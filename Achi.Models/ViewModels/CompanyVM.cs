using Achi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achi.Models.ViewModels
{
    public class CompanyVM
    {
        public List<Company> Companies { get; set; }

        public string CompanyName { get; set; }
        public string AddressLine { get; set; }

        public string SelectedCountryCode { get; set; }
        public string SelectedStateId { get; set; }
        public string SelectedCityId { get; set; }

        public List<CountryDTO> Countries { get; set; }
        public List<StateDTO> States { get; set; }
        public List<CityDTO> Cities { get; set; }
    }



}
