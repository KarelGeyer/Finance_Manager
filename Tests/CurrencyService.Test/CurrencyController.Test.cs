using Common.Enums;
using Common.Exceptions;
using Common.Models.Category;
using Common.Models.Currency;
using CurrencyService.Controllers;
using CurrencyService.Db;
using Moq;

namespace CurrencyService.Test
{
    [TestClass]
    public class CurrencyControllerTest
    {
        Mock<IDbService> _mockDbService;

        List<Currency> _currencies;
        Currency _currency;

        [TestInitialize]
        public void Init()
        {
            _currency = new Currency
            {
                Id = 1,
                Value = "CZK"
            };
            _currencies = [_currency];
            _mockDbService = new Mock<IDbService>();
        }

        [TestMethod]
        public void GetAllTest()
        {
            _mockDbService.Setup(x => x.GetAll()).ReturnsAsync(_currencies);
            CurrencyController controller = new CurrencyController(_mockDbService.Object);

            var response = controller.GetAll();
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data.Count, _currencies.Count);
        }

        [TestMethod]
        public void GetAllTestWithNotFoundException()
        {
            _mockDbService.Setup(x => x.GetAll()).Throws(new NotFoundException());
            CurrencyController controller = new CurrencyController(_mockDbService.Object);

            var response = controller.GetAll();
            var result = response.Result;

            Assert.IsNull(result.Data);
            Assert.AreEqual(result.ResponseMessage, "No record was found");
            Assert.AreEqual(result.Status, EHttpStatus.NOT_FOUND);
        }

        [TestMethod]
        public void GetAllTestWithException()
        {
            _mockDbService.Setup(x => x.GetAll()).Throws(new Exception());
            CurrencyController controller = new CurrencyController(_mockDbService.Object);

            var response = controller.GetAll();
            var result = response.Result;

            Assert.IsNull(result.Data);
            Assert.AreEqual(result.ResponseMessage, "Exception of type 'System.Exception' was thrown.");
            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
        }

        [TestMethod]
        public void GetTest()
        {
            int id = 1;
            _mockDbService.Setup(x => x.Get(id)).ReturnsAsync(_currency);
            CurrencyController controller = new CurrencyController(_mockDbService.Object);

            var response = controller.Get(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, _currency);
        }

        [TestMethod]
        public void GetTestWithNotFoundException()
        {
            int id = 1;
            _mockDbService.Setup(x => x.Get(id)).Throws(new NotFoundException());
            CurrencyController controller = new CurrencyController(_mockDbService.Object);

            var response = controller.Get(id);
            var result = response.Result;

            Assert.IsNull(result.Data);
            Assert.AreEqual(result.ResponseMessage, "No record was found");
            Assert.AreEqual(result.Status, EHttpStatus.NOT_FOUND);
        }

        [TestMethod]
        public void GetTestWithException()
        {
            int id = 1;
            _mockDbService.Setup(x => x.Get(id)).Throws(new Exception());
            CurrencyController controller = new CurrencyController(_mockDbService.Object);

            var response = controller.Get(id);
            var result = response.Result;

            Assert.IsNull(result.Data);
            Assert.AreEqual(result.ResponseMessage, "Exception of type 'System.Exception' was thrown.");
            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
}
