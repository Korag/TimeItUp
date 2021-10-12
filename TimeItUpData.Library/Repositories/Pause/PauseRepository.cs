using TimeItUpData.Library.DataAccess;

namespace TimeItUpData.Library.Repositories
{
    public class PauseRepository : IPauseRepository
    {
        private readonly EFDbContext _context;

        public PauseRepository(EFDbContext context)
        {
            _context = context;
        }

    }
}
