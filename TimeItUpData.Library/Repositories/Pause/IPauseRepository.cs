using System.Collections.Generic;
using System.Threading.Tasks;
using TimeItUpData.Library.Models;

namespace TimeItUpData.Library.Repositories
{
    public interface IPauseRepository
    {
        Task<ICollection<Pause>> GetAllPausesAsync();
        Task<ICollection<Pause>> GetAllActivePausesAsync();
        Task<ICollection<Pause>> GetAllPastPausesAsync();
        Task<Pause> GetPauseByIdAsync(int id);
        Task<ICollection<Pause>> GetPausesByIdsAsync(ICollection<int> idSet);
        void RemovePause(Pause pause);
        Task AddPauseAsync(Pause pause);
        bool CheckIfPauseExist(int id);
    }
}
