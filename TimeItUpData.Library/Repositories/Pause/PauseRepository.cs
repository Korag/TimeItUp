using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeItUpData.Library.DataAccess;
using TimeItUpData.Library.Models;

namespace TimeItUpData.Library.Repositories
{
    public class PauseRepository : IPauseRepository
    {
        private readonly EFDbContext _context;

        public PauseRepository(EFDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Pause>> GetAllPausesAsync()
        {
            return await _context.Pauses.ToListAsync();
        }

        public async Task<ICollection<Pause>> GetAllActivePausesAsync()
        {
            return await _context.Pauses.Where(z => z.StartAt != DateTime.MinValue && z.EndAt == DateTime.MinValue).ToListAsync();
        }

        public async Task<ICollection<Pause>> GetAllPastPausesAsync()
        {
            return await _context.Pauses.Where(z => z.StartAt != DateTime.MinValue && z.EndAt != DateTime.MinValue).ToListAsync();
        }

        public async Task<Pause> GetPauseByIdAsync(int id)
        {
            return await _context.Pauses.Where(z => z.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Pause>> GetPausesByIdsAsync(ICollection<int> idSet)
        {
            return await _context.Pauses.Where(z => idSet.Contains(z.Id)).ToListAsync();
        }

        public void RemovePause(Pause pause)
        {
            _context.Pauses.Remove(pause);
        }

        public async Task AddPauseAsync(Pause pause)
        {
            await _context.Pauses.AddAsync(pause);
        }

        public bool CheckIfPauseExist(int id)
        {
            return _context.Pauses.Any(z => z.Id == id);
        }
    }
}
