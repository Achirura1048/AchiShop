using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achi.Models.ViewModels
{
    public class CompanyUpsertVM
    {
        public Company Company { get; set; }

        public string SelectedCountryCode { get; set; } 
        public string SelectedStateCode { get; set; }

        [ValidateNever]
        public string? SelectedCityName { get; set; }      

        [ValidateNever]

        public List<SelectListItem> CountryList { get; set; }
        [ValidateNever]

        public List<SelectListItem> StateList { get; set; }
        [ValidateNever]

        public List<SelectListItem> CityList { get; set; }
    }



}
