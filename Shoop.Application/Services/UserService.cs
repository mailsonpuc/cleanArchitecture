

using AutoMapper;
using Shoop.Application.DTOs;
using Shoop.Application.Interfaces;
using Shoop.Domain.Entities;
using Shoop.Domain.Interfaces;

namespace Shoop.Application.Services
{
    public class UserService : IUserService
    {

        private IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }



        public async Task Add(UserDTO userDTO)
        {
            var userEntity = _mapper.Map<User>(userDTO);
            await _userRepository.CreateAsync(userEntity);

        }

        public async Task<UserDTO> GetById(int? id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            try
            {
                var userEntity = await _userRepository.GetUsersAsync();
                var userDto = _mapper.Map<IEnumerable<UserDTO>>(userEntity);
                return userDto;
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
            var userEntity = _mapper.Map<User>(userDTO);
            await _userRepository.UpdateAsync(userEntity);
        }
    }
}