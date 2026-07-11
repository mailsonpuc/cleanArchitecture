
using Shoop.Application.DTOs;
using Shoop.Application.Interfaces;
using Shoop.Application.Mappings;
using Shoop.Domain.Interfaces;

namespace Shoop.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Add(UserDTO userDTO)
        {
            var userEntity = userDTO.ToUserEntity();

            await _userRepository.CreateAsync(userEntity);
            userDTO.Id = userEntity.Id;
        }

        public async Task<UserDTO> GetById(int? id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            return userEntity is null ? null! : userEntity.ToUserDTO();
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            try
            {
                var userEntity = await _userRepository.GetUsersAsync();
                return userEntity.Select(user => user.ToUserDTO());
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task Remove(int? id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            if (userEntity == null) return;
            await _userRepository.RemoveAsync(userEntity);
        }

        public async Task Update(UserDTO userDTO)
        {
            var userEntity = userDTO.ToUserEntity();
            await _userRepository.UpdateAsync(userEntity);
        }

        public async Task<UserDTO> GetByUsername(string username)
        {
            var userEntity = await _userRepository.GetByUsername(username);
            return userEntity is null ? null! : userEntity.ToUserDTO();
        }
    }
}