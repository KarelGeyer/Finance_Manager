using Common.Models.Category;
using SavingsService.Controllers;
using Moq;
using SavingsService.Service;
using Common.Exceptions;
using Common.Enums;
using Common.Models.Savings;

namespace SavingsServiceTest
{
    [TestClass]
    public class SavingsServiceTest
    {
        Mock<IDbService> _dbServiceMock;

        [TestInitialize]
        public void InitializeTest()
        {
            _dbServiceMock = new Mock<IDbService>();
        }

        [TestMethod]
        public void GetSavingsTest()
        {
            _dbServiceMock.Setup(x => x.Get(1)).ReturnsAsync(1000f);
            SavingsController controller = new(_dbServiceMock.Object);

            var response = controller.GetSavings(1);
            var result = response.Result;

            Assert.AreEqual(result.Data, 1000f);
            Assert.AreEqual(result.Status, Common.Enums.EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
        }

        [TestMethod]
        public void GetSavingsTestThrowsNotFoundException()
        {
            _dbServiceMock.Setup(x => x.Get(1)).Throws(new NotFoundException());
            SavingsController controller = new(_dbServiceMock.Object);

            var response = controller.GetSavings(1);
            var result = response.Result;

            Assert.AreEqual(result.Data, 0);
            Assert.AreEqual(result.Status, EHttpStatus.NOT_FOUND);
            Assert.AreEqual(result.ResponseMessage, "No record was found");
        }

        [TestMethod]
        public void GetSavingsTestThrowsException()
        {
            _dbServiceMock.Setup(x => x.Get(1)).Throws(new Exception());
            SavingsController controller = new(_dbServiceMock.Object);

            var response = controller.GetSavings(1);
            var result = response.Result;

            Assert.AreEqual(result.Data, 0);
            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
            Assert.AreEqual(
                result.ResponseMessage,
                "Exception of type 'System.Exception' was thrown."
            );
        }

        [TestMethod]
        public void AddSavingsTest()
        {
            _dbServiceMock.Setup(x => x.Create(1)).ReturnsAsync(true);
            SavingsController controller = new(_dbServiceMock.Object);

            var response = controller.AddSavings(1);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, true);
        }

        [TestMethod]
        public void AddSavingsTestThrowsFailedToCreateException()
        {
            _dbServiceMock.Setup(x => x.Create(1)).Throws(new FailedToCreateException<Savings>());
            SavingsController controller = new(_dbServiceMock.Object);

            var response = controller.AddSavings(1);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.BAD_REQUEST);
            Assert.AreEqual(result.ResponseMessage, "Failed to create new T");
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void AddSavingsTestThrowsException()
        {
            _dbServiceMock.Setup(x => x.Create(1)).Throws(new Exception());
            SavingsController controller = new(_dbServiceMock.Object);

            var response = controller.AddSavings(1);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
            Assert.AreEqual(
                result.ResponseMessage,
                "Exception of type 'System.Exception' was thrown."
            );
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void UpdateSavingsTest()
        {
            UpdateSavings request = new UpdateSavings { UserId = 1, Amount = 1000, };
            _dbServiceMock.Setup(x => x.Update(request)).ReturnsAsync(true);
            SavingsController controller = new(_dbServiceMock.Object);

            var response = controller.UpdateSavings(request);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, true);
        }

        [TestMethod]
        public void UpdateSavingsTestThrowsFailedToUpdateException()
        {
            UpdateSavings request = new UpdateSavings { UserId = 1, Amount = 1000, };
            _dbServiceMock
                .Setup(x => x.Update(request))
                .Throws(new FailedToUpdateException<Savings>());
            SavingsController controller = new(_dbServiceMock.Object);

            var response = controller.UpdateSavings(request);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.BAD_REQUEST);
            Assert.AreEqual(result.ResponseMessage, "Failed to update T");
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void UpdateSavingsTestThrowsUserDoesNotExistExceptionn()
        {
            UpdateSavings request = new UpdateSavings { UserId = 1, Amount = 1000, };
            _dbServiceMock.Setup(x => x.Update(request)).Throws(new UserDoesNotExistException());
            SavingsController controller = new(_dbServiceMock.Object);

            var response = controller.UpdateSavings(request);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.BAD_REQUEST);
            Assert.AreEqual(result.ResponseMessage, "User does not exist");
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void UpdateSavingsTestThrowsException()
        {
            UpdateSavings request = new UpdateSavings { UserId = 1, Amount = 1000, };
            _dbServiceMock.Setup(x => x.Update(request)).Throws(new Exception());
            SavingsController controller = new(_dbServiceMock.Object);

            var response = controller.UpdateSavings(request);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
            Assert.AreEqual(
                result.ResponseMessage,
                "Exception of type 'System.Exception' was thrown."
            );
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void DeleteSavingsTest()
        {
            int userId = 1;
            _dbServiceMock.Setup(x => x.Delete(userId)).ReturnsAsync(true);
            SavingsController controller = new(_dbServiceMock.Object);

            var response = controller.DeleteSavings(userId);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, true);
        }

        [TestMethod]
        public void DeleteSavingsTestThrowsException()
        {
            int userId = 1;
            _dbServiceMock.Setup(x => x.Delete(userId)).Throws(new Exception());
            SavingsController controller = new(_dbServiceMock.Object);

            var response = controller.DeleteSavings(userId);
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
