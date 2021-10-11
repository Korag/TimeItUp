using System.Collections.Generic;
using System.Threading.Tasks;
using TimeItUpData.Library.Models;

namespace TimeItUpData.Library.Repositories
{
    public interface IAlarmRepository
    {
        Task<ICollection<Alarm>> GetAllAlarmsAsync();
        Task<Alarm> GetAlarmByIdAsync(int id);
        void RemoveAlarm(Alarm alarm);
        Task AddAlarmAsync(Alarm alarm);
        bool CheckIfAlarmExist(int id);
    }
}
