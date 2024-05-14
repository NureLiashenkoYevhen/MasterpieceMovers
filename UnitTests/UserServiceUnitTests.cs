using UnitTests.Helpers.MockHelpers.ServiceMocks;


namespace UnitTests;

public class UserServiceUnitTests
{
    [Fact]
    public async Task GetAllAsync_WhenAddedUsers_CheckCount()
    {
        //Arrange
        var userService = IUserServiceMock.GetMock();
        
        //Act
        var count = await userService.GetAllAsync();
        
        //Assert
        Assert.Equal(4, count.Count);
    }
}