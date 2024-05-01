using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Models.Income;
using Common.Models.User;
using Moq;
using UserService.Controllers;
using UserService.Db;
using UserService.Service;

namespace UserService.Test
{
    [TestClass]
    public class UserControllerTest
    {
        Mock<IDbService> _dbService;

        User _user;
        List<User> _users;

        [TestInitialize]
        public void Initialize()
        {
            _dbService = new Mock<IDbService>();

            _user = new User
            {
                Id = 1,
                UserGroupId = 1,
                Name = "John",
                Surname = "Doe",
                Username = "johndoe",
                Email = "john.doe@gmail.com",
                Password = "password",
                CurrencyId = 1,
                IsVerified = true,
                CreatedAt = DateTime.Now,
            };

            _users = [_user];
        }

        [TestMethod]
        public void TestGetAllUsers()
        {
            _dbService.Setup(x => x.GetAll()).ReturnsAsync(_users);
            UserController controller = new UserController(_dbService.Object);

            var response = controller.GetAll();
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, _users);
        }

        [TestMethod]
        public void TestGetAllNotFoundEx()
        {
            _dbService.Setup(x => x.GetAll()).ThrowsAsync(new NotFoundException());
            UserController controller = new UserController(_dbService.Object);

            var response = controller.GetAll();
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.NOT_FOUND);
            Assert.AreEqual(result.ResponseMessage, "No record was found");
            Assert.AreEqual(result.Data, null);
        }

        [TestMethod]
        public void TestGetAllInternalServerErrorEx()
        {
            _dbService.Setup(x => x.GetAll()).ThrowsAsync(new Exception());
            UserController controller = new UserController(_dbService.Object);

            var response = controller.GetAll();
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
            Assert.AreEqual(
                result.ResponseMessage,
                "Exception of type 'System.Exception' was thrown."
            );
            Assert.AreEqual(result.Data, null);
        }

        [TestMethod]
        public void TestGetUserById()
        {
            int id = 1;

            _dbService.Setup(x => x.GetById(id)).ReturnsAsync(_user);
            UserController controller = new UserController(_dbService.Object);

            var response = controller.GetById(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, _user);
        }

        [TestMethod]
        public void TestGetUserByIdNullEx()
        {
            int id = 0;

            UserController controller = new UserController(_dbService.Object);

            var response = controller.GetById(id);

            Assert.AreEqual(response.Exception.InnerException.Message, "Value cannot be null.");
        }

        [TestMethod]
        public void TestGetUserByIdNotFoundEx()
        {
            int id = 1;

            _dbService.Setup(x => x.GetById(id)).Throws(new NotFoundException());
            UserController controller = new UserController(_dbService.Object);

            var response = controller.GetById(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.NOT_FOUND);
            Assert.AreEqual(result.ResponseMessage, "No record was found");
            Assert.AreEqual(result.Data, null);
        }

        [TestMethod]
        public void TestGetUserByIdInternalServerErrorEx()
        {
            int id = 1;

            _dbService.Setup(x => x.GetById(id)).Throws(new Exception());
            UserController controller = new UserController(_dbService.Object);

            var response = controller.GetById(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
            Assert.AreEqual(
                result.ResponseMessage,
                "Exception of type 'System.Exception' was thrown."
            );
            Assert.AreEqual(result.Data, null);
        }

        [TestMethod]
        public void TestCreateUser()
        {
            CreateUser createUser = new CreateUser
            {
                Name = "John",
                Surname = "Doe",
                Username = "johndoe",
                Email = "john.doe@gmail.com",
                Password = "password",
                CurrencyId = 1,
            };

            _dbService.Setup(x => x.Create(createUser)).ReturnsAsync(true);
            UserController controller = new UserController(_dbService.Object);

            var response = controller.Create(createUser);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, true);
        }

        [TestMethod]
        public void TestCreateUserNullEx()
        {
            CreateUser createUser = null;

            UserController controller = new UserController(_dbService.Object);

            var response = controller.Create(createUser);

            Assert.AreEqual(
                response.Exception.InnerException.Message,
                "Value cannot be null. (Parameter 'req')"
            );
        }

        [TestMethod]
        public void TestCreateUserFailedToCreateEx()
        {
            CreateUser createUser = new CreateUser
            {
                Name = "John",
                Surname = "Doe",
                Username = "johndoe",
                Email = "john.doe@gmail.com",
                Password = "password",
                CurrencyId = 1,
            };

            _dbService.Setup(x => x.Create(createUser)).Throws(new FailedToCreateException<User>());
            UserController controller = new UserController(_dbService.Object);

            var response = controller.Create(createUser);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.BAD_REQUEST);
            Assert.AreEqual(
                result.ResponseMessage,
                CustomResponseMessage.GetFailedToCreateMessage<User>()
            );
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void TestCreateUserInternalServerErrorEx()
        {
            CreateUser createUser = new CreateUser
            {
                Name = "John",
                Surname = "Doe",
                Username = "johndoe",
                Email = "john.doe@gmail.com",
                Password = "password",
                CurrencyId = 1,
            };

            _dbService.Setup(x => x.Create(createUser)).Throws(new Exception());
            UserController controller = new UserController(_dbService.Object);

            var response = controller.Create(createUser);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
            Assert.AreEqual(
                result.ResponseMessage,
                "Exception of type 'System.Exception' was thrown."
            );
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            UpdateUser updateUser = new UpdateUser
            {
                Id = 1,
                Username = "johnisdoe",
                Email = "johnis.doe@gmail.com",
            };

            _dbService.Setup(x => x.Update(updateUser)).ReturnsAsync(true);
            UserController controller = new UserController(_dbService.Object);

            var response = controller.Update(updateUser);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, true);
        }

        [TestMethod]
        public void TestUpdateUserNullEx()
        {
            UpdateUser updateUser = null;

            UserController controller = new UserController(_dbService.Object);

            var response = controller.Update(updateUser);

            Assert.AreEqual(
                response.Exception.InnerException.Message,
                "Value cannot be null. (Parameter 'req')"
            );
        }

        [TestMethod]
        public void TestUpdateUserFailedToUpdateEx()
        {
            UpdateUser updateUser = new UpdateUser
            {
                Id = 1,
                Username = "johnisdoe",
                Email = "johnis.doe@gmail.com",
            };

            _dbService.Setup(x => x.Update(updateUser)).Throws(new FailedToUpdateException<User>());
            UserController controller = new UserController(_dbService.Object);

            var response = controller.Update(updateUser);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.BAD_REQUEST);
            Assert.AreEqual(
                result.ResponseMessage,
                CustomResponseMessage.GetFailedToUpdateMessage<User>()
            );
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void TestUpdateUserInternalServerErrorEx()
        {
            UpdateUser updateUser = new UpdateUser
            {
                Id = 1,
                Username = "johnisdoe",
                Email = "johnis.doe@gmail.com",
            };

            _dbService.Setup(x => x.Update(updateUser)).Throws(new Exception());
            UserController controller = new UserController(_dbService.Object);

            var response = controller.Update(updateUser);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
            Assert.AreEqual(
                result.ResponseMessage,
                "Exception of type 'System.Exception' was thrown."
            );
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void TestUpdateUserPassword()
        {
            UpdateUserPassword updateUserPassword = new UpdateUserPassword
            {
                Id = 1,
                Password = "newpassword",
            };

            _dbService.Setup(x => x.UpdatePassword(updateUserPassword)).ReturnsAsync(true);
            UserController controller = new UserController(_dbService.Object);

            var response = controller.UpdatePassword(updateUserPassword);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, true);
        }

        [TestMethod]
        public void TestUpdateUserPasswordNullEx()
        {
            UpdateUserPassword updateUserPassword = null;

            UserController controller = new UserController(_dbService.Object);

            var response = controller.UpdatePassword(updateUserPassword);

            Assert.AreEqual(
                response.Exception.InnerException.Message,
                "Value cannot be null. (Parameter 'req')"
            );
        }

        [TestMethod]
        public void TestUpdateUserPasswordFailedToUpdateEx()
        {
            UpdateUserPassword updateUserPassword = new UpdateUserPassword
            {
                Id = 1,
                Password = "newpassword",
            };

            _dbService
                .Setup(x => x.UpdatePassword(updateUserPassword))
                .Throws(new FailedToUpdateException<User>());
            UserController controller = new UserController(_dbService.Object);

            var response = controller.UpdatePassword(updateUserPassword);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.BAD_REQUEST);
            Assert.AreEqual(
                result.ResponseMessage,
                CustomResponseMessage.GetFailedToUpdateMessage<User>()
            );
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void TestUpdateUserPasswordInternalServerErrorEx()
        {
            UpdateUserPassword updateUserPassword = new UpdateUserPassword
            {
                Id = 1,
                Password = "newpassword",
            };

            _dbService.Setup(x => x.UpdatePassword(updateUserPassword)).Throws(new Exception());
            UserController controller = new UserController(_dbService.Object);

            var response = controller.UpdatePassword(updateUserPassword);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
            Assert.AreEqual(
                result.ResponseMessage,
                "Exception of type 'System.Exception' was thrown."
            );
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            int id = 1;

            _dbService.Setup(x => x.Delete(id)).ReturnsAsync(true);
            UserController controller = new UserController(_dbService.Object);

            var response = controller.Delete(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, true);
        }

        [TestMethod]
        public void TestDeleteUserNullEx()
        {
            int id = 0;

            UserController controller = new UserController(_dbService.Object);

            var response = controller.Delete(id);

            Assert.AreEqual(response.Exception.InnerException.Message, "Value cannot be null.");
        }

        [TestMethod]
        public void TestDeleteUserFailedToDeleteEx()
        {
            int id = 1;

            _dbService.Setup(x => x.Delete(id)).Throws(new FailedToDeleteException<User>());
            UserController controller = new UserController(_dbService.Object);

            var response = controller.Delete(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.BAD_REQUEST);
            Assert.AreEqual(
                result.ResponseMessage,
                CustomResponseMessage.GetFailedToDeleteMessage<User>()
            );
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void TestDeleteUserInternalServerErrorEx()
        {
            int id = 1;

            _dbService.Setup(x => x.Delete(id)).Throws(new Exception());
            UserController controller = new UserController(_dbService.Object);

            var response = controller.Delete(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
            Assert.AreEqual(
                result.ResponseMessage,
                "Exception of type 'System.Exception' was thrown."
            );
            Assert.AreEqual(result.Data, false);
        }
    }
}
