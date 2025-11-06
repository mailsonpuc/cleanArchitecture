

using Shoop.Application.DTOs;

namespace Shoop.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsers();
        Task<UserDTO> GetById(int? id);
        Task Add(UserDTO userDTO);
        Task Update(UserDTO userDTO);
        Task Remove(int? id);

        Task<UserDTO> GetByUsername(string username); //autentica

    }
}