using Core.DTO.Users;
using Core.Entities;
using Core.Enums;

namespace UnitTests.Helpers;

internal class DbHelper
{
    public static List<User> GetFakeUsers()
    {
        return new List<User>()
        {
            new User
            {
                Email = "email1",
                Role = RoleEnum.User,
                Name = "user1"
            },
            new User
            {
                Email = "email2",
                Role = RoleEnum.User,
                Name = "user2"
            },
            new User
            {
                Email = "email3",
                Role = RoleEnum.User,
                Name = "user3"
            },
            new User
            {
                Email = "email4",
                Role = RoleEnum.User,
                Name = "user4"
            },
        };

    }
}