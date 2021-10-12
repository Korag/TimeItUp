using System.Collections.Generic;
using System.Threading.Tasks;
using TimeItUpData.Library.Models;

namespace TimeItUpData.Library.Repositories
{
    public interface ISplitRepository
    {
        Task<ICollection<Split>> GetAllSplitsAsync();
        Task<ICollection<Split>> GetAllActiveSplitsAsync();
        Task<ICollection<Split>> GetAllPastSplitsAsync();
        Task<Split> GetSplitByIdAsync(int id);
        Task<ICollection<Split>> GetSplitsByIdsAsync(ICollection<int> idSet);
        void RemoveSplit(Split split);
        Task AddSplitAsync(Split split);
        bool CheckIfSplitExist(int id);
    }
}
