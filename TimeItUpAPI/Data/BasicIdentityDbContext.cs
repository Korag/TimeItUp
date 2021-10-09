using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeItUpData.Library.Models;

namespace TimeItUpAPI.Data
{
    public class BasicIdentityDbContext : IdentityDbContext<BasicIdentityUser>
    {
        public BasicIdentityDbContext(DbContextOptions<BasicIdentityDbContext> options)
            : base(options)
        {
        }
    }
}