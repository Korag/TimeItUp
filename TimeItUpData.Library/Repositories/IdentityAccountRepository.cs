using Microsoft.AspNetCore.Identity;
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
