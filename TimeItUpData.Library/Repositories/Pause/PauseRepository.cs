using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeItUpData.Library.DataAccess;
using TimeItUpData.Library.Models;

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
