using TimeItUpData.Library.DataAccess;

namespace TimeItUpData.Library.Repositories
{
    public class TimerRepository : ITimerRepository
    {
        private readonly EFDbContext _context;

        public TimerRepository(EFDbContext context)
        {
            _context = context;
        }
    }
}
