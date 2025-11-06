

using Microsoft.EntityFrameworkCore;
using Shoop.Domain.Entities;
using Shoop.Domain.Interfaces;
using Shoop.Infrastructure.Context;

namespace Shoop.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _userContext;

        public UserRepository(ApplicationDbContext userContext)
        {
            _userContext = userContext;
        }



        public async Task<User> CreateAsync(User user)
        {
            _userContext.Add(user);
            await _userContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByIdAsync(int? id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _userContext.Users.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                var users = await _userContext.Users.AsNoTracking().ToListAsync();
                return users;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<User> RemoveAsync(User user)
        {
            _userContext.Remove(user);
            await _userContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _userContext.Update(user);
            await _userContext.SaveChangesAsync();
            return user;
        }
    }
}