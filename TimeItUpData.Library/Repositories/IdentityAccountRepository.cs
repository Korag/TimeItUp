using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeItUpData.Library.DataAccess;
using TimeItUpData.Library.Models;

namespace TimeItUpData.Library.Repositories
{
    public class IdentityAccountRepository
    {
        private readonly EFDbContext _context;

        public IdentityAccountRepository(EFDbContext context, UserManager<BasicIdentityUser> userManager)
        {
            _context = context;
        }
    }
}
