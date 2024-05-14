using BLL.Users;
using BLL.Validation;
using Core.Entities;
using DAL;
using Moq;


namespace UnitTests.Helpers.MockHelpers.ServiceMocks;

public class IUserServiceMock
{
    public static IUserService GetMock()
    {
        var passwordService = new Mock<IPasswordService>();
        
        var users = DbHelper.GetFakeUsers();
        var dbContextMock = DbContextMock.GetMock<User, ApplicationDbContext>
            (users, u => u.Users);

        return new UserService(dbContextMock, passwordService.Object);
    }
}