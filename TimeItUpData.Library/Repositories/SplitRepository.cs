using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
