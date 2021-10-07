using System.Threading.Tasks;

namespace TimeItUpData.Library.Repositories
{
    public interface IGeneralRepository
    {
        Task ChangeEntryStateToModified(object entry);
        Task SaveChangesAsync();
    }
}