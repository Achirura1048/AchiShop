using Achi.DataAccess.Data;
using Achi.DataAccess.Repository;
using Achi.DataAccess.Repository.IRepository;
using Achi.Models;
using Achi.Models.DTOs;
using Achi.Models.ViewModels;
using Achi.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _UoW;
        private readonly ICSCService _cscService;
        private readonly ICountryRepository _countryRepo;
        private readonly IStateRepository _stateRepo;
        private readonly ICityRepository _cityRepo;

        public CompanyController(IUnitOfWork UoW, ICSCService cscService, ICountryRepository countryRepo, IStateRepository stateRepo, ICityRepository cityRepo)
        {
            _UoW = UoW;
            _cscService = cscService;
            _countryRepo = countryRepo;
            _stateRepo = stateRepo;
            _cityRepo = cityRepo;
        }

        public IActionResult Index()
        {
            var companies = _UoW.Company.GetAll().ToList();
            var viewModel = new CompanyVM
            {
                Companies = companies
            };

            return View(viewModel);
        }

        // GET
        public IActionResult Upsert(int? id)
        {
            var vm = new CompanyUpsertVM();

            if (id == null)
            {
                vm.Company = new Company();
            }
            else
            {
                vm.Company = _UoW.Company.Get(i => i.ID == id, includeProperties: "Country,State");

                if (vm.Company == null)
                    return NotFound();

                // Country code (ISO2)
                if (vm.Company.CountryId > 0)
                {
                    var country = _UoW.Country.Get(c => c.ID == vm.Company.CountryId);
                    if (country != null)
                        vm.SelectedCountryCode = country.ISO2;
                }

                // State code
                if (vm.Company.StateId > 0)
                {
                    var state = _UoW.State.Get(s => s.ID == vm.Company.StateId);
                    if (state != null)
                        vm.SelectedStateCode = state.Code;
                }

                // City name from city ID
                if (vm.Company.CityId > 0)
                {
                    var city = _UoW.City.Get(c => c.CityId == vm.Company.CityId);
                    if (city != null)
                        vm.SelectedCityName = city.Name;
                }
            }

            // Load countries dropdown list (async call forced sync here)
            var countries = _cscService.GetCountriesAsync().Result;
            vm.CountryList = countries.ToList();

            // Load states if country selected
            if (!string.IsNullOrEmpty(vm.SelectedCountryCode))
            {
                var states = _cscService.GetStatesAsync(vm.SelectedCountryCode).Result;
                vm.StateList = states.ToList();
            }
            else
            {
                vm.StateList = new List<SelectListItem>();
            }

            // Load cities if country and state selected
            if (!string.IsNullOrEmpty(vm.SelectedCountryCode) && !string.IsNullOrEmpty(vm.SelectedStateCode))
            {
                var cities = _cscService.GetCitiesAsync(vm.SelectedCountryCode, vm.SelectedStateCode).Result;
                vm.CityList = cities.ToList();
            }
            else
            {
                vm.CityList = new List<SelectListItem>();
            }

            return View(vm);
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CompanyUpsertVM vm)
        {
            vm.CountryList = (await _cscService.GetCountriesAsync()).ToList();
            vm.StateList = string.IsNullOrEmpty(vm.SelectedCountryCode)
                ? new List<SelectListItem>()
                : (await _cscService.GetStatesAsync(vm.SelectedCountryCode)).ToList();
            vm.CityList = (string.IsNullOrEmpty(vm.SelectedCountryCode) || string.IsNullOrEmpty(vm.SelectedStateCode))
                ? new List<SelectListItem>()
                : (await _cscService.GetCitiesAsync(vm.SelectedCountryCode, vm.SelectedStateCode)).ToList();

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var selectedIso2 = vm.SelectedCountryCode;
            var countryName = vm.CountryList.FirstOrDefault(c => c.Value == selectedIso2)?.Text ?? selectedIso2;

            var country = _countryRepo.Get(c => c.ISO2 == selectedIso2);

            if (country == null)
            {
                country = new Country
                {
                    Name = countryName,
                    ISO2 = selectedIso2
                };
                _countryRepo.Add(country);
                await _UoW.SaveAsync();
            }
            else if (country.Name != countryName)
            {
                country.Name = countryName;
                _countryRepo.Update(country);
                await _UoW.SaveAsync();
            }

            var stateCode = vm.SelectedStateCode;
            var stateName = vm.StateList.FirstOrDefault(s => s.Value == stateCode)?.Text ?? stateCode;

            var state = _stateRepo.Get(s => s.Code == stateCode && s.CountryId == country.ID);

            if (state == null)
            {
                state = new State
                {
                    Name = stateName,
                    Code = stateCode,
                    CountryId = country.ID
                };
                _stateRepo.Add(state);
                await _UoW.SaveAsync();
            }

            var selectedCityName = vm.SelectedCityName?.Trim();

            var city = _cityRepo.Get(c =>
                c.Name.ToLower() == selectedCityName.ToLower() &&
                c.StateId == state.ID);

            if (city == null && !string.IsNullOrEmpty(selectedCityName))
            {
                city = new City
                {
                    Name = selectedCityName,
                    StateId = state.ID
                };
                _cityRepo.Add(city);
                await _UoW.SaveAsync();
            }

            
            vm.Company.CountryId = country.ID;
            vm.Company.StateId = state.ID;
            vm.Company.CityId = city.CityId;

            
            vm.Company.Name = vm.Company.Name?.Trim();
            vm.Company.Adress = vm.Company.Adress?.Trim();
            vm.Company.PhoneNumber = vm.Company.PhoneNumber?.Trim();

            if (vm.Company.ID == 0)
                _UoW.Company.Add(vm.Company);
            else
                _UoW.Company.Update(vm.Company);

            await _UoW.SaveAsync();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> GetStates(string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                return BadRequest("Country code is required");

            var states = await _cscService.GetStatesAsync(countryCode);

            var mappedStates = states.Select(s => new
            {
                value = s.Value,
                text = s.Text
            }).ToList();

            return Json(mappedStates);
        }

        [HttpGet]
        public async Task<IActionResult> GetCities(string countryCode, string stateCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode) || string.IsNullOrWhiteSpace(stateCode))
                return BadRequest("Country code and state code are required");

            var cities = await _cscService.GetCitiesAsync(countryCode, stateCode);

            if (!cities.Any())
                return Json(new { error = "No cities found" });

            return Json(cities);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Company? companyID = _UoW.Company.Get(i => i.ID == id);

            if (companyID == null)
            {
                return NotFound();
            }

            return View(companyID);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Company? obj = _UoW.Company.Get(i => i.ID == id);

            if (obj == null)
            {
                return NotFound();
            }

            _UoW.Company.Remove(obj);
            _UoW.Save();

            TempData["Success"] = obj.Name + " Şirket Kullanıcısı Başarıyla Silindi";
            return RedirectToAction("Index");
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companies = _UoW.Company.GetAll(includeProperties: "Country,State,City")
                .Select(c => new
                {
                    id = c.ID,
                    name = c.Name,
                    phoneNumber = c.PhoneNumber,
                    address = c.Adress,
                    country = c.Country.Name,
                    state = c.State.Name,
                    city = c.City.Name
                });

            return Json(new { data = companies });
        }

        #endregion
    }
}
