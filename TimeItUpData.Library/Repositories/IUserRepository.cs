using System.Collections.Generic;
using System.Threading.Tasks;
using TimeItUpData.Library.Models;

namespace TimeItUpData.Library.Repositories
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAllUsersAsync();
        Task<User> GetUserById(string id);
        bool CheckIfUserExist(string id);
        void RemoveUser(User user);
        Task AddUserAsync(User user);
    }
}
