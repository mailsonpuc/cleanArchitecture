
using Shoop.Domain.Entities;

namespace Shoop.Domain.Interfaces
{
    public interface IUserRepository
    {
        
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetByIdAsync(int? id);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User> RemoveAsync(User user);
        Task<User> GetByUsername(string username);
    }
}