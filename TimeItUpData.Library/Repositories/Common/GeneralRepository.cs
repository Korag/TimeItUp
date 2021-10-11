using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeItUpData.Library.DataAccess;

namespace TimeItUpData.Library.Repositories
{
    public class GeneralRepository : IGeneralRepository
    {
        private readonly EFDbContext _context;

        public GeneralRepository(EFDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task ChangeEntryStateToModified(object entry)
        {
            _context.Entry(entry).State = EntityState.Modified;
        }
    }
}
