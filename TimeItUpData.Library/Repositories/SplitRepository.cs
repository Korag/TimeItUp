using TimeItUpData.Library.DataAccess;

namespace TimeItUpData.Library.Repositories
{
    public class SplitRepository : ISplitRepository
    {
        private readonly EFDbContext _context;

        public SplitRepository(EFDbContext context)
        {
            _context = context;
        }
    }
}
