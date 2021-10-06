using TimeItUpData.Library.DataAccess;

namespace TimeItUpData.Library.Repositories
{
    public class AlarmRepository : IAlarmRepository
    {
        private readonly EFDbContext _context;

        public AlarmRepository(EFDbContext context)
        {
            _context = context;
        }
    }
}
