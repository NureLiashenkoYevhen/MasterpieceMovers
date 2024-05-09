using BLL.Validation;
using Core.DTO.Users;
using Core.Entities;
using Core.Enums;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace BLL.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly IPasswordService _passwordService;

        public UserService(ApplicationDbContext dbContext, IPasswordService passwordService)
        {
            _appDbContext = dbContext;
            _passwordService = passwordService;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        public async Task<User>? GetUserByIdAsync(int id)
        {
            return await _appDbContext.Users.FindAsync(id);
        }

        public async Task CreateUserAsync(CreateUserModel user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var foundedUser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (foundedUser is not null)
            {
                throw new ArgumentException();
            }

            var (hashedPassword, salt) = _passwordService.HashPassword(user.Password);

            _appDbContext.Users.Add(new User
            {
                Name = user.UserName,
                Role = user.Role,
                Email = user.Email,
                PasswordHashed = hashedPassword,
                PasswordSalt = salt,
            });

            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(int id, UpdateUserModel updateUserModel)
        {
            if (updateUserModel is null)
            {
                throw new ArgumentNullException(nameof(updateUserModel));
            }

            var dbUser = await _appDbContext.FindAsync<User>(id);

            if (dbUser is null)
            {
                throw new ArgumentNullException(nameof(updateUserModel));
            }

            dbUser.Name = updateUserModel.Name;
            dbUser.Email = updateUserModel.Email;
            dbUser.Role = updateUserModel.Role;

            _appDbContext.Users.Update(dbUser);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _appDbContext.Users.FindAsync(id);
            if (user != null)
            {
                _appDbContext.Users.Remove(user);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> SignUpAsync(SignUpUserModel signUpModel)
        {
            var existingUser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == signUpModel.Email);

            if (existingUser != null)
            {
                return false;
            }

            var (hash, salt) = _passwordService.HashPassword(signUpModel.Password);

            var newUser = new User
            {
                Name = signUpModel.Name,
                Role = RoleEnum.User,
                Email = signUpModel.Email,
                PasswordHashed = hash,
                PasswordSalt = salt,
            };

            await _appDbContext.Users.AddAsync(newUser);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<User>? LoginAsync(string email, string password)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user is not null && _passwordService.IsValid(password, user.PasswordHashed, user.PasswordSalt))
            {
                return user;
            }

            return null;
        }
    }
}
