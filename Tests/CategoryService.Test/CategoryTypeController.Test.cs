using CategoryService.Service;
using Common.Enums;
using Common.Exceptions;
using Common.Models.Category;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using UsersService.Controllers;

namespace CategoryService.Test
{
    [TestClass]
    public class CategoryTypeControllerTest
    {
        Mock<IDbService> _mockDbService;

        CategoryType _categoryType;
        List<CategoryType> _categoryTypes;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockDbService = new Mock<IDbService>();

            _categoryType = new CategoryType
            {
                Id = 0,
                Value = "Category Type One"
            };

            _categoryTypes = [_categoryType];
        }

        [TestMethod]
        public void GetAllTest()
        {
            _mockDbService.Setup(x => x.GetAllCategoryTypes()).ReturnsAsync(_categoryTypes);
            CategoryTypeController controller = new CategoryTypeController(_mockDbService.Object);

            var response = controller.GetAll();
            var result = response.Result;

            Assert.AreEqual(result.Status, Common.Enums.EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data.Count, _categoryTypes.Count);
        }

        [TestMethod]
        public void GetAllTestWithNotFoundException()
        {
            _mockDbService.Setup(x => x.GetAllCategoryTypes()).Throws(new NotFoundException());
            CategoryTypeController controller = new CategoryTypeController(_mockDbService.Object);

            var response = controller.GetAll();
            var result = response.Result;

            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.ResponseMessage, "No record was found");
            Assert.AreEqual(result.Status, EHttpStatus.NOT_FOUND);
        }

        [TestMethod]
        public void GetAllTestWithInternalErrorException()
        {
            _mockDbService.Setup(x => x.GetAllCategoryTypes()).Throws(new Exception());
            CategoryTypeController controller = new CategoryTypeController(_mockDbService.Object);

            var response = controller.GetAll();
            var result = response.Result;

            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.ResponseMessage, "Exception of type 'System.Exception' was thrown.");
            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            int id = 1;
            _mockDbService.Setup(x => x.GetCategoryType(id)).ReturnsAsync(_categoryType);
            CategoryTypeController controller = new CategoryTypeController(_mockDbService.Object);

            var response = controller.GetById(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, Common.Enums.EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, _categoryType);
        }

        [TestMethod]
        public void GetByIdTestWithNotFoundException()
        {
            int id = 1;
            _mockDbService.Setup(x => x.GetCategoryType(id)).Throws(new NotFoundException());
            CategoryTypeController controller = new CategoryTypeController(_mockDbService.Object);

            var response = controller.GetById(id);
            var result = response.Result;

            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.ResponseMessage, "No record was found");
            Assert.AreEqual(result.Status, EHttpStatus.NOT_FOUND);
        }

        [TestMethod]
        public void GetByIdTestWithInternalErrorException()
        {
            int id = 1;
            _mockDbService.Setup(x => x.GetCategoryType(id)).Throws(new Exception());
            CategoryTypeController controller = new CategoryTypeController(_mockDbService.Object);

            var response = controller.GetById(id);
            var result = response.Result;

            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.ResponseMessage, "Exception of type 'System.Exception' was thrown.");
            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
}
