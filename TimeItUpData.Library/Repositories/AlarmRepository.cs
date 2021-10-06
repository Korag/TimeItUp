using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
