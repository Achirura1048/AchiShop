using Achi.DataAccess.Data;
using Achi.DataAccess.Repository.IRepository;
using Achi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achi.DataAccess.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly AppDbContext _context;

        public CityRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<City> GetByNameAsync(string name, int stateId)
        {
            return await _context.Cities.FirstOrDefaultAsync(c => c.Name == name && c.StateId == stateId);
        }

        public async Task<City> GetOrAddAsync(string name, int stateId)
        {
            var city = await _context.Cities
                .FirstOrDefaultAsync(c => c.Name == name && c.StateId == stateId);

            if (city == null)
            {
                city = new City { Name = name, StateId = stateId };
                _context.Cities.Add(city);
                await _context.SaveChangesAsync();
            }

            return city;
        }
    }
}
