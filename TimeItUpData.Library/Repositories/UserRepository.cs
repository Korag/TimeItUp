using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeItUpData.Library.DataAccess;
using TimeItUpData.Library.Models;

namespace TimeItUpData.Library.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EFDbContext _context;

        public UserRepository(EFDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.Where(z=> z.Id == id).FirstOrDefaultAsync();
        }

        public bool CheckIfUserExist(string id)
        {
            return _context.Users.Any(z => z.Id == id);
        }

        public void RemoveUser(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.AddAsync(user);
        }
    }
}
