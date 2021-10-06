using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TimeItUpAPI.Data
{
    public class BasicIdentityDbContext : IdentityDbContext
    {
        public BasicIdentityDbContext(DbContextOptions<BasicIdentityDbContext> options)
            : base(options)
        {
        }
    }
}