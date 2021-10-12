using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeItUpData.Library.DataAccess;
using TimeItUpData.Library.Models;

namespace TimeItUpData.Library.Repositories
{
    public class AlarmRepository : IAlarmRepository
    {
        private readonly EFDbContext _context;

        public AlarmRepository(EFDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Alarm>> GetAllAlarmsAsync()
        {
            return await _context.Alarms.ToListAsync();
        }

        public async Task<Alarm> GetAlarmByIdAsync(int id)
        {
            return await _context.Alarms.Where(z => z.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Alarm>> GetAlarmsByIdsAsync(ICollection<int> idSet)
        {
            return await _context.Alarms.Where(z => idSet.Contains(z.Id)).ToListAsync();
        }

        public void RemoveAlarm(Alarm alarm)
        {
            _context.Alarms.Remove(alarm);
        }

        public async Task AddAlarmAsync(Alarm alarm)
        {
            await _context.Alarms.AddAsync(alarm);
        }

        public bool CheckIfAlarmExist(int id)
        {
            return _context.Alarms.Any(z => z.Id == id);
        }
    }
}
