using Microsoft.AspNetCore.Identity;
using TimeItUpData.Library.DataAccess;
using TimeItUpData.Library.Models;

namespace TimeItUpData.Library.Repositories
{
    public class IdentityAccountRepository : IIdentityAccountRepository
    {
        private readonly EFDbContext _context;

        public IdentityAccountRepository(EFDbContext context, UserManager<BasicIdentityUser> userManager)
        {
            _context = context;
        }
    }
}
