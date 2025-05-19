using Achi.DataAccess.Data;
using Achi.DataAccess.Repository.IRepository;
using Achi.Models;
using Microsoft.EntityFrameworkCore;

namespace Achi.DataAccess.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly AppDbContext _context;

        public CountryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Fix parameter name and search by ISO2 now
        public async Task<Country?> GetByISO2Async(string iso2)
        {
            return await _context.Countries
                .FirstOrDefaultAsync(c => c.ISO2 == iso2);
        }

        public async Task<Country> GetByCodeAsync(string code)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.ISO2 == code);
        }

        public async Task<Country> GetOrAddAsync(string iso2, string name)
        {
            var country = await _context.Countries
                .FirstOrDefaultAsync(c => c.ISO2 == iso2 || c.Name == name);

            if (country == null)
            {
                country = new Country { ISO2 = iso2, Name = name };
                _context.Countries.Add(country);
                await _context.SaveChangesAsync();
            }

            return country;
        }

        // Add Update method
        public void Update(Country entity)
        {
            _context.Countries.Update(entity);
        }
    }

}
