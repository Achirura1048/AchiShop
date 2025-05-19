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
    public class StateRepository : Repository<State>, IStateRepository
    {
        private readonly AppDbContext _context;

        public StateRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // StateRepository.cs
        public async Task<State> GetByCodeAsync(string code, int countryId)
        {
            return await _context.States.FirstOrDefaultAsync(s => s.Code == code && s.CountryId == countryId);
        }



        public async Task<State> GetOrAddAsync(string name, int countryId)
        {
            // Check if the state already exists
            var state = await _context.States
                .FirstOrDefaultAsync(s => s.Name == name && s.CountryId == countryId);

            if (state == null)
            {
                // If it doesn't exist, create a new state
                state = new State { Name = name, CountryId = countryId };
                _context.States.Add(state);
                await _context.SaveChangesAsync();
            }

            return state;
        }
    }
}
