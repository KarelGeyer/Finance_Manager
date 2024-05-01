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
    public class CategoryControllerTest
    {
        Mock<IDbService> _mockDbService;

        Category _category;
        List<Category> _categories;

        [TestInitialize]
        public void InitializeTest()
        {
            _mockDbService = new Mock<IDbService>();

            _category = new Category
            {
                Id = 0,
                CategoryTypeId = 0,
                Value = "Category One"
            };

            _categories = [_category];
        }

        [TestMethod]
        public void GetAllTest()
        {
            _mockDbService.Setup(x => x.GetAllCategories()).ReturnsAsync(_categories);
            CategoryController controller = new CategoryController(_mockDbService.Object);

            var response = controller.GetAll();
            var result = response.Result;

            Assert.AreEqual(result.Status, Common.Enums.EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data.Count, _categories.Count);
        }

        [TestMethod]
        public void GetAllTestWithNotFoundException()
        {
            _mockDbService.Setup(x => x.GetAllCategories()).Throws(new NotFoundException());
            CategoryController controller = new CategoryController(_mockDbService.Object);

            var response = controller.GetAll();
            var result = response.Result;

            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.ResponseMessage, "No record was found");
            Assert.AreEqual(result.Status, EHttpStatus.NOT_FOUND);
        }

        [TestMethod]
        public void GetAllTestWithInternalErrorException()
        {
            _mockDbService.Setup(x => x.GetAllCategories()).Throws(new Exception());
            CategoryController controller = new CategoryController(_mockDbService.Object);

            var response = controller.GetAll();
            var result = response.Result;

            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.ResponseMessage, "Exception of type 'System.Exception' was thrown.");
            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
        }

        [TestMethod]
        public void GetByTypeTest()
        {
            int id = 1;
            _mockDbService.Setup(x => x.GetCategoriesByCategoryType(id)).ReturnsAsync(_categories);
            CategoryController controller = new CategoryController(_mockDbService.Object);

            var response = controller.GetByType(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, Common.Enums.EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data.Count, _categories.Count);
        }

        [TestMethod]
        public void GetByTypeTestWithNotFoundException()
        {
            int id = 1;
            _mockDbService.Setup(x => x.GetCategoriesByCategoryType(id)).Throws(new NotFoundException());
            CategoryController controller = new CategoryController(_mockDbService.Object);

            var response = controller.GetByType(id);
            var result = response.Result;

            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.ResponseMessage, "No record was found");
            Assert.AreEqual(result.Status, EHttpStatus.NOT_FOUND);
        }

        [TestMethod]
        public void GetByTypeTestWithInternalErrorException()
        {
            int id = 1;
            _mockDbService.Setup(x => x.GetCategoriesByCategoryType(id)).Throws(new Exception());
            CategoryController controller = new CategoryController(_mockDbService.Object);

            var response = controller.GetByType(id);
            var result = response.Result;

            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.ResponseMessage, "Exception of type 'System.Exception' was thrown.");
            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            int id = 1;
            _mockDbService.Setup(x => x.GetCategory(id)).ReturnsAsync(_category);
            CategoryController controller = new CategoryController(_mockDbService.Object);

            var response = controller.GetById(id);
            var result = response.Result;

            Assert.AreEqual(result.Status, Common.Enums.EHttpStatus.OK);
            Assert.AreEqual(result.ResponseMessage, string.Empty);
            Assert.AreEqual(result.Data, _category);
        }

        [TestMethod]
        public void GetByIdTestWithNotFoundException()
        {
            int id = 1;
            _mockDbService.Setup(x => x.GetCategory(id)).Throws(new NotFoundException());
            CategoryController controller = new CategoryController(_mockDbService.Object);

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
            _mockDbService.Setup(x => x.GetCategory(id)).Throws(new Exception());
            CategoryController controller = new CategoryController(_mockDbService.Object);

            var response = controller.GetById(id);
            var result = response.Result;

            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.ResponseMessage, "Exception of type 'System.Exception' was thrown.");
            Assert.AreEqual(result.Status, EHttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
}
