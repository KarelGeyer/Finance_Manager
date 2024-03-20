using Common.User;
using UsersService.Controllers;

namespace UserService.Test.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public async Task GetUsersTestItRetrievesUsers()
        {
            //Arrange
            UserController controller = new UserController();

            //Act
            List<User> result = await controller.GetUsers();
            //Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }
    }
}
