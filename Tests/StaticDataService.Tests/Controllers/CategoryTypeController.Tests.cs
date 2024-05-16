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
	public class CategoryTypeControllerTests
	{
		private readonly IDbService<CategoryType> _dbService;
		private readonly ILogger<CategoryTypeController> _logger;

		private CategoryTypeController _controller;

		private CategoryType _categoryType;
		private CategoryType _categoryType2;
		private List<CategoryType> _categoryTypes;

		public CategoryTypeControllerTests()
		{
			_dbService = Substitute.For<IDbService<CategoryType>>();
			_logger = Substitute.For<ILogger<CategoryTypeController>>();
			_controller = new CategoryTypeController(_dbService);

			_categoryType = new() { Id = 1, Value = "CategoryType 1", };
			_categoryType2 = new() { Id = 2, Value = "CategoryType 2", };
			_categoryTypes = new() { _categoryType, _categoryType2 };
		}

		[Fact]
		public async Task GetAllCategories_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.GetAllAsync().Returns(_categoryTypes);

			// Act
			BaseResponse<List<CategoryType>> result = await _controller.GetAllCategoryTypes();

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().NotBeNull();
			result.Data.Should().BeOfType<List<CategoryType>>();
			result.Data.Should().HaveCount(2);
			result.Data[0].Should().BeEquivalentTo(_categoryType);
		}

		[Fact]
		public async Task GetAllCategories_ThrowsException()
		{
			// Arrange
			_dbService.GetAllAsync().Throws(x => new Exception());

			// Act
			BaseResponse<List<CategoryType>> result = await _controller.GetAllCategoryTypes();

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
			BaseResponse<List<CategoryType>> result = await _controller.GetAllCategoryTypes();

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be("No record was found");
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeNull();
		}

		[Fact]
		public async Task GetCategoryType_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.GetAsync(1).Returns(_categoryType);

			// Act
			BaseResponse<CategoryType> result = await _controller.GetCategoryTypeById(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().NotBeNull();
			result.Data.Should().BeOfType<CategoryType>();
			result.Data.Should().BeEquivalentTo(_categoryType);
		}

		[Fact]
		public async Task GetCategoryType_ThrowsException()
		{
			// Arrange
			_dbService.GetAsync(1).Throws(new Exception());

			// Act
			BaseResponse<CategoryType> result = await _controller.GetCategoryTypeById(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeNull();
		}

		[Fact]
		public async Task GetCategoryType_ThrowsNotFoundException()
		{
			// Arrange
			_dbService.GetAsync(1).Throws(new NotFoundException());

			// Act
			BaseResponse<CategoryType> result = await _controller.GetCategoryTypeById(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be("No record was found");
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeNull();
		}
	}
}
