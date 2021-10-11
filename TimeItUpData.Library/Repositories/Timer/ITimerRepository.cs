using System.Collections.Generic;
using System.Threading.Tasks;
using TimeItUpData.Library.Models;

namespace TimeItUpData.Library.Repositories
{
    public interface ITimerRepository
    {
        public Task<Timer> GetTimerByIdAsync(int id);
        public Task<ICollection<Timer>> GetAllTimersAsync();
        Task<ICollection<Timer>> GetAllActiveTimersAsync();
        Task<ICollection<Timer>> GetAllFinishedTimersAsync();
        Task<ICollection<Timer>> GetAllPausedTimersAsync();
        public void RemoveTimer(Timer timer);
        public Task AddTimerAsync(Timer timer);
        public bool CheckIfTimerExist(int id);
    }
}
