using Common.Enums;
using Common.Exceptions;
using Common.Models.Category;
using Common.Response;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using StaticDataService.Controllers;
using StaticDataService.Db;

namespace StaticDataServiceTests.Controllers
{
    public class CategoryControllerTests
    {
        private readonly IDbService<Category> _dbService;
        private readonly ILogger<CategoryController> _logger;

        private CategoryController _controller;

        private Category _category;
        private Category _category2;
        private List<Category> _categories;

        public CategoryControllerTests()
        {
            _dbService = Substitute.For<IDbService<Category>>();
            _logger = Substitute.For<ILogger<CategoryController>>();
            _controller = new CategoryController(_dbService);

            _category = new()
            {
                Id = 1,
                Value = "Category 1",
                CategoryTypeId = 1,
            };
            _category2 = new()
            {
                Id = 2,
                Value = "Category 2",
                CategoryTypeId = 2,
            };
            _categories = new() { _category, _category2 };
        }

        [Fact]
        public async Task GetAllCategories_ReturnsCorrectValue()
        {
            // Arrange
            _dbService.GetAllAsync().Returns(_categories);

            // Act
            BaseResponse<List<Category>> result = await _controller.GetAllCategories();

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be(string.Empty);
            result.Status.Should().Be(EHttpStatus.OK);
            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<List<Category>>();
            result.Data.Should().HaveCount(2);
            result.Data[0].Should().BeEquivalentTo(_category);
        }

        [Fact]
        public async Task GetAllCategories_ThrowsException()
        {
            // Arrange
            _dbService.GetAllAsync().Throws(x => new Exception());

            // Act
            BaseResponse<List<Category>> result = await _controller.GetAllCategories();

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().BeNull();
            result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
        }

        [Fact]
        public async Task GetAllCategories_ThrowsNotFoundException()
        {
            // Arrange
            _dbService.GetAllAsync().Throws(x => new NotFoundException());

            // Act
            BaseResponse<List<Category>> result = await _controller.GetAllCategories();

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be("No record was found");
            result.Status.Should().Be(EHttpStatus.NOT_FOUND);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetCategoriesByType_ReturnsCorrectValue()
        {
            // Arrange
            _dbService.GetAllAsync().Returns(_categories);

            // Act
            BaseResponse<List<Category>> result = await _controller.GetCategoriesByType(1);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be(string.Empty);
            result.Status.Should().Be(EHttpStatus.OK);
            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<List<Category>>();
            result.Data.Should().HaveCount(1);
            result.Data[0].Should().BeEquivalentTo(_category);
        }

        [Fact]
        public async Task GetCategoriesByType_ThrowsException()
        {
            // Arrange
            _dbService.GetAllAsync().Throws(x => new Exception());

            // Act
            BaseResponse<List<Category>> result = await _controller.GetCategoriesByType(1);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().BeNull();
            result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
        }

        [Fact]
        public async Task GetCategoriesByType_ThrowsNotFoundException()
        {
            // Arrange
            _dbService.GetAllAsync().Throws(x => new NotFoundException());

            // Act
            BaseResponse<List<Category>> result = await _controller.GetCategoriesByType(1);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be("No record was found");
            result.Status.Should().Be(EHttpStatus.NOT_FOUND);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetCategory_ReturnsCorrectValue()
        {
            // Arrange
            _dbService.GetAsync(1).Returns(_category);

            // Act
            BaseResponse<Category> result = await _controller.GetCategoryById(1);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be(string.Empty);
            result.Status.Should().Be(EHttpStatus.OK);
            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<Category>();
            result.Data.Should().BeEquivalentTo(_category);
        }

        [Fact]
        public async Task GetCategory_ThrowsException()
        {
            // Arrange
            _dbService.GetAsync(1).Throws(new Exception());

            // Act
            BaseResponse<Category> result = await _controller.GetCategoryById(1);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetCategory_ThrowsNotFoundException()
        {
            // Arrange
            _dbService.GetAsync(1).Throws(new NotFoundException());

            // Act
            BaseResponse<Category> result = await _controller.GetCategoryById(1);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be("No record was found");
            result.Status.Should().Be(EHttpStatus.NOT_FOUND);
            result.Data.Should().BeNull();
        }
    }
}
