using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeItUpData.Library.DataAccess;
using TimeItUpData.Library.Models;

namespace TimeItUpData.Library.Repositories
{
    public class TimerRepository : ITimerRepository
    {
        private readonly EFDbContext _context;

        public TimerRepository(EFDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Timer>> GetAllTimersAsync()
        {
            return await _context.Timers.ToListAsync();
        }

        public async Task<ICollection<Timer>> GetAllActiveTimersAsync()
        {
            return await _context.Timers.Where(z => !z.Finished && !z.Paused).ToListAsync();
        }

        public async Task<ICollection<Timer>> GetAllFinishedTimersAsync()
        {
            return await _context.Timers.Where(z => z.Finished).ToListAsync();
        }

        public async Task<ICollection<Timer>> GetAllPausedTimersAsync()
        {
            return await _context.Timers.Where(z => z.Paused).ToListAsync();
        }

        public async Task<Timer> GetTimerByIdAsync(int id)
        {
            return await _context.Timers.Where(z => z.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Timer>> GetTimersByIdsAsync(ICollection<int> idSet)
        {
            return await _context.Timers.Where(z => idSet.Contains(z.Id)).ToListAsync();
        }

        public void RemoveTimer(Timer timer)
        {
            _context.Timers.Remove(timer);
        }

        public async Task AddTimerAsync(Timer timer)
        {
            await _context.Timers.AddAsync(timer);
        }

        public bool CheckIfTimerExist(int id)
        {
            return _context.Timers.Any(z => z.Id == id);
        }
    }
}
