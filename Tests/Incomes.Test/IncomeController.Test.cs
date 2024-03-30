using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Models.Category;
using Common.Models.Income;
using IncomeService.Controllers;
using IncomeService.Db;
using Moq;
using Newtonsoft.Json.Linq;

namespace Incomes.Test
{
    [TestClass]
    public class IncomeServiceTests
    {

        Mock<IDbService> _mockDbService;

        Income _income;
        List<Income> _incomes;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockDbService = new Mock<IDbService>();

            _income = new Income
                {
                    Id = 1,
                    OwnerId = 1,
                    Name = "Name",
                    Value = 25000,
                    CategoryId = 1,
                    CreatedAt = DateTime.Now,
                };

            _incomes = [_income];
        }

        [TestMethod]
        public void GetAllTest()
        {
            int id = 1;
            _mockDbService.Setup(x => x.GetAll(id)).ReturnsAsync(_incomes);
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.GetAll(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data.Count, _incomes.Count);
        }

        [TestMethod]
        public void GetAllTestWithInternalErrorException()
        {
        int id = 1;
        _mockDbService.Setup(x => x.GetAll(id)).Throws(new Exception());
        IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.GetAll(id);
            var result = response.Result;

            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.ResponseMessage, "Exception of type 'System.Exception' was thrown.");
            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
        }

        [TestMethod]
        public void GetTest()
        {
            int id = 1;
            int userId = 1;
            _mockDbService.Setup(x => x.Get(userId, id)).ReturnsAsync(_income);
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Get(userId, id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data.Id, _income.Id);
        }

        [TestMethod]
        public void GetTestThrowsNotFoundException()
        {
            int id = 1;
            int userId = 1;

            _mockDbService.Setup(x => x.Get(userId, id)).Throws(new NotFoundException());
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Get(userId, id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.NOT_FOUND);
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void GetTestThrowsException()
        {
            int id = 1;
            int userId = 1;

            _mockDbService.Setup(x => x.Get(userId, id)).Throws(new Exception());
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Get(userId, id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
            Assert.AreEqual(result.ResponseMessage, "Exception of type 'System.Exception' was thrown.");
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void CreateTest()
        {
            IncomeCreateRequest req = new()
            {
                OwnerId = 1,
                Name = "Name",
                Value = 10000,
                CategoryId = 1,
            };

            _mockDbService.Setup(x => x.Create(req)).ReturnsAsync(true);
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Create(req);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, true);
        }

        [TestMethod]
        public void CreateTestThrowsFailedToCreateException()
        {
            IncomeCreateRequest req = new()
            {
                OwnerId = 1,
                Name = "Name",
                Value = 10000,
                CategoryId = 1,
            };

            _mockDbService.Setup(x => x.Create(req)).Throws(new FailedToCreateException<Income>());
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Create(req);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.BAD_REQUEST);
            Assert.AreEqual(result.ResponseMessage, CustomResponseMessage.GetFailedToCreateMessage<Income>());
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void CreateTestThrowsException()
        {
            IncomeCreateRequest req = new()
            {
                OwnerId = 1,
                Name = "Name",
                Value = 10000,
                CategoryId = 1,
            };

            _mockDbService.Setup(x => x.Create(req)).Throws(new Exception());
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Create(req);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
            Assert.AreEqual(result.ResponseMessage, "Exception of type 'System.Exception' was thrown.");
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void UpdateNameTest()
        {
            IncomeUpdateNameRequest req = new()
            {
                Id = 1,
                Name = "Name",
            };

            _mockDbService.Setup(x => x.Update(req)).ReturnsAsync(true);
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Update(req);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, true);
        }


        [TestMethod]
        public void UpdateNameTestThrowsNotFoundException()
        {
            IncomeUpdateNameRequest req = new()
            {
                Id = 1,
                Name = "Name",
            };

            _mockDbService.Setup(x => x.Update(req)).Throws(new NotFoundException());
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Update(req);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.NOT_FOUND);
            Assert.AreEqual(result.ResponseMessage, "No record was found");
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void UpdateNameTestThrowsFailedToUpdateException()
        {
            IncomeUpdateNameRequest req = new()
            {
                Id = 1,
                Name = "Name",
            };

            _mockDbService.Setup(x => x.Update(req)).Throws(new FailedToUpdateException<Income>(req.Id));
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Update(req);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.BAD_REQUEST);
            Assert.AreEqual(result.ResponseMessage, CustomResponseMessage.GetFailedToUpdateMessage<Income>(req.Id));
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void UpdateValueTest()
        {
            IncomeUpdateValueRequest req = new()
            {
                Id = 1,
                Value = 1000,
            };

            _mockDbService.Setup(x => x.Update(req)).ReturnsAsync(true);
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Update(req);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, true);
        }

        [TestMethod]
        public void UpdateValueTestThrowsNotFoundException()
        {
            IncomeUpdateValueRequest req = new()
            {
                Id = 1,
                Value = 1000,
            };

            _mockDbService.Setup(x => x.Update(req)).Throws(new NotFoundException());
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Update(req);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.NOT_FOUND);
            Assert.AreEqual(result.ResponseMessage, "No record was found");
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void UpdateValueTestThrowsFailedToUpdateException()
        {
            IncomeUpdateValueRequest req = new()
            {
                Id = 1,
                Value = 1000,
            };

            _mockDbService.Setup(x => x.Update(req)).Throws(new FailedToUpdateException<Income>(req.Id));
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Update(req);
            var result = response.Result;
            var responseString = CustomResponseMessage.GetFailedToUpdateMessage<Income>(req.Id);

            Assert.AreEqual(result.Status, EHttpStatus.BAD_REQUEST);
            Assert.AreEqual(result.ResponseMessage, CustomResponseMessage.GetFailedToUpdateMessage<Income>(req.Id));
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void UpdateValueTestThrowsException()
        {
            IncomeUpdateValueRequest req = new()
            {
                Id = 1,
                Value = 1000,
            };

            _mockDbService.Setup(x => x.Update(req)).Throws(new Exception());
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Update(req);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
            Assert.AreEqual(result.ResponseMessage, "Exception of type 'System.Exception' was thrown.");
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int id = 1;

            _mockDbService.Setup(x => x.Delete(id)).ReturnsAsync(true);
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Delete(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, true);
        }

        [TestMethod]
        public void DeleteTestThrowsNotFoundException()
        {
            int id = 1;

            _mockDbService.Setup(x => x.Delete(id)).Throws(new NotFoundException());
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Delete(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.NOT_FOUND);
            Assert.AreEqual(result.ResponseMessage, "No record was found");
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void DeleteTestThrowsFailedToDeleteException()
        {
            int id = 1;

            _mockDbService.Setup(x => x.Delete(id)).Throws(new FailedToDeleteException<Income>(id));
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Delete(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.BAD_REQUEST);
            Assert.AreEqual(result.ResponseMessage, CustomResponseMessage.GetFailedToDeleteMessage<Income>(id));
            Assert.AreEqual(result.Data, false);
        }

        [TestMethod]
        public void DeleteTestThrowsException()
        {
            int id = 1;

            _mockDbService.Setup(x => x.Delete(id)).Throws(new Exception());
            IncomeController controller = new IncomeController(_mockDbService.Object);

            var response = controller.Delete(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
            Assert.AreEqual(result.ResponseMessage, "Exception of type 'System.Exception' was thrown.");
            Assert.AreEqual(result.Data, false);
        }
    }
}