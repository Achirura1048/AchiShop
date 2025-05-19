using Achi.DataAccess.Repository.IRepository;
using Achi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Achi.Models.DTOs;
using Achi.DataAccess.Migrations;


namespace Achi.DataAccess.Repository
{
    public class CSCService : ICSCService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public CSCService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["CountryStateCity:ApiKey"];
            _httpClient.DefaultRequestHeaders.Add("X-CSCAPI-KEY", _apiKey);
            _httpClient.BaseAddress = new Uri("https://api.countrystatecity.in/v1/");
        }

        public async Task<IEnumerable<SelectListItem>> GetCountriesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("countries");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var countries = JsonSerializer.Deserialize<List<CountryDTO>>(json);

                return countries?
                    .OrderBy(c => c.name)
                    .Select(c => new SelectListItem
                    {
                        Value = c.iso2,
                        Text = c.name
                    }) ?? Enumerable.Empty<SelectListItem>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to fetch countries", ex);
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetStatesAsync(string countryCodeIso2)
        {
            if (string.IsNullOrWhiteSpace(countryCodeIso2))
                throw new ArgumentException("Country code is required", nameof(countryCodeIso2));

            try
            {
                var response = await _httpClient.GetAsync($"countries/{countryCodeIso2}/states");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var states = JsonSerializer.Deserialize<List<StateDTO>>(json);

                return states?
                    .OrderBy(s => s.name)
                    .Select(s => new SelectListItem
                    {
                        Value = s.iso2 ?? s.id.ToString(),
                        Text = s.name
                    }) ?? Enumerable.Empty<SelectListItem>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to fetch states for country {countryCodeIso2}", ex);
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetCitiesAsync(string countryCodeIso2, string stateCode)
        {

            try
            {
                var response = await _httpClient.GetAsync($"countries/{countryCodeIso2}/states/{stateCode}/cities");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var cities = JsonSerializer.Deserialize<List<CityDTO>>(json);

                return cities?
                    .OrderBy(c => c.name)
                    .Select(c => new SelectListItem
                    {
                        Value = c.name,   // Use city name as value
                        Text = c.name    // Use city name as display text
                    }) ?? Enumerable.Empty<SelectListItem>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to fetch cities for country {countryCodeIso2} and state {stateCode}", ex);
            }
        }







    }
}
