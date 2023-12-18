using Core.DTO.Users;
using Core.Entities;

namespace BLL.Users
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();

        Task<User>? GetUserByIdAsync(int id);

        Task CreateUserAsync(CreateUserModel user);

        Task UpdateUserAsync(int id, UpdateUserModel user);

        Task DeleteUserAsync(int id);

        Task<User> LoginAsync(string username, string password);

        Task<bool>? SignUpAsync(SignUpUserModel signUpDto);
    }
}
