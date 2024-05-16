using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Models.Expenses;
using Common.Models.PortfolioModels.Budget;
using Common.Response;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PortfolioService.Controllers;
using PortfolioService.Db;

namespace PortfolioServiceTests.Controllers
{
	public class BudgetControllerTests
	{
		private readonly IDbService<Budget> _dbService;
		private readonly ILogger<BugdetController> _logger;

		private BugdetController _controller;

		private Budget _budget;
		private Budget _budget2;
		private List<Budget> _budgets;

		private int _ownerId = 1;

		public BudgetControllerTests()
		{
			_dbService = Substitute.For<IDbService<Budget>>();
			_logger = Substitute.For<ILogger<BugdetController>>();
			_controller = new BugdetController(_logger, _dbService);

			_budget = new()
			{
				Id = 1,
				OwnerId = _ownerId,
				Value = 100,
				CategoryId = 2
			};
			_budget2 = new()
			{
				Id = 2,
				OwnerId = _ownerId,
				Value = 200,
				CategoryId = 2
			};
			_budgets = new() { _budget, _budget2 };
		}

		[Fact]
		public async Task GetAllBudgets_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.GetAllAsync(_ownerId).Returns(_budgets);

			// Act
			BaseResponse<List<Budget>> result = await _controller.GetBudgets(_ownerId);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().NotBeNull();
			result.Data.Should().BeOfType<List<Budget>>();
			result.Data.Should().HaveCount(2);
			result.Data[0].Should().BeEquivalentTo(_budget);
		}

		[Fact]
		public async Task GetAllBudgets_ThrowsException()
		{
			// Arrange
			_dbService.GetAllAsync(_ownerId).Throws(x => new Exception());

			// Act
			BaseResponse<List<Budget>> result = await _controller.GetBudgets(_ownerId);

			// Assert
			result.Should().NotBeNull();
			result.Data.Should().BeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
		}

		[Fact]
		public async Task GetBudget_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.GetAsync(1).Returns(_budget);

			// Act
			BaseResponse<Budget> result = await _controller.GetBudget(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().NotBeNull();
			result.Data.Should().BeOfType<Budget>();
			result.Data.Should().BeEquivalentTo(_budget);
		}

		[Fact]
		public async Task GetBudget_ThrowsException()
		{
			// Arrange
			_dbService.GetAsync(1).Throws(new Exception());

			// Act
			BaseResponse<Budget> result = await _controller.GetBudget(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeNull();
		}

		[Fact]
		public async Task GetBudget_ThrowsNotFoundException()
		{
			// Arrange
			_dbService.GetAsync(1).Throws(new NotFoundException());

			// Act
			BaseResponse<Budget> result = await _controller.GetBudget(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be("No record was found");
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeNull();
		}

		[Fact]
		public async Task CreateBudget_ReturnsCorrectValue()
		{
			// Arrange
			CreateBudget createBudget =
				new()
				{
					Parent = 1,
					Value = 100,
					CategoryId = 1
				};

			_dbService.CreateAsync(Arg.Any<Budget>()).Returns(true);

			// Act
			BaseResponse<bool> result = await _controller.CreateBudget(createBudget);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().BeTrue();
		}

		[Fact]
		public async Task CreateBudget_ThrowsException()
		{
			// Arrange
			CreateBudget createBudget =
				new()
				{
					Parent = 1,
					Value = 100,
					CategoryId = 1
				};

			_dbService.CreateAsync(Arg.Any<Budget>()).Throws(x => new Exception());

			// Act
			BaseResponse<bool> result = await _controller.CreateBudget(createBudget);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task CreateProperty_ThrowsFailedToCreateException()
		{
			// Arrange
			CreateBudget createBudget =
				new()
				{
					Parent = 1,
					Value = 100,
					CategoryId = 1
				};

			_dbService.CreateAsync(Arg.Any<Budget>()).Throws(x => new FailedToCreateException<Budget>());

			// Act
			BaseResponse<bool> result = await _controller.CreateBudget(createBudget);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToCreateMessage<Budget>());
			result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task UpdateBudget_ReturnsCorrectValue()
		{
			// Arrange
			UpdateBudget updateBudget = new() { Id = 1, Value = 100, };
			_dbService.GetAsync(Arg.Any<int>()).Returns(_budget);
			_dbService.UpdateAsync(Arg.Any<Budget>()).Returns(true);

			// Act
			BaseResponse<bool> result = await _controller.UpdateBudget(updateBudget);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().BeTrue();
		}

		[Fact]
		public async Task UpdateBudget_ThrowsException()
		{
			// Arrange
			UpdateBudget updateBudget = new() { Id = 1, Value = 100, };
			_dbService.GetAsync(Arg.Any<int>()).Returns(_budget);
			_dbService.UpdateAsync(Arg.Any<Budget>()).Throws(new Exception());

			// Act
			BaseResponse<bool> result = await _controller.UpdateBudget(updateBudget);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task UpdateBudget_ThrowsNotFoundException()
		{
			// Arrange
			UpdateBudget updateBudget = new() { Id = 1, Value = 100, };
			_dbService.GetAsync(Arg.Any<int>()).Returns(_budget);
			_dbService.UpdateAsync(Arg.Any<Budget>()).Throws(new NotFoundException());

			// Act
			BaseResponse<bool> result = await _controller.UpdateBudget(updateBudget);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task UpdateBudget_ThrowsFailedToUpdateException()
		{
			// Arrange
			UpdateBudget updateBudget = new() { Id = 1, Value = 100, };
			_dbService.GetAsync(Arg.Any<int>()).Returns(_budget);
			_dbService.UpdateAsync(Arg.Any<Budget>()).Throws(new FailedToUpdateException<Budget>());

			// Act
			BaseResponse<bool> result = await _controller.UpdateBudget(updateBudget);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToUpdateMessage<Budget>());
			result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task DeleteBudget_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.DeleteAsync(1).Returns(true);

			// Act
			BaseResponse<bool> result = await _controller.DeleteBudget(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().BeTrue();
		}

		[Fact]
		public async Task DeleteBudget_ThrowsException()
		{
			// Arrange
			_dbService.DeleteAsync(1).Throws(new Exception());

			// Act
			BaseResponse<bool> result = await _controller.DeleteBudget(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task DeleteBudget_ThrowsNotFoundException()
		{
			// Arrange
			_dbService.DeleteAsync(1).Throws(new NotFoundException());

			// Act
			BaseResponse<bool> result = await _controller.DeleteBudget(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task DeleteBudget_ThrowsFailedToDeleteException()
		{
			// Arrange
			_dbService.DeleteAsync(1).Throws(new FailedToDeleteException<Budget>());

			// Act
			BaseResponse<bool> result = await _controller.DeleteBudget(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToDeleteMessage<Budget>());
			result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
			result.Data.Should().BeFalse();
		}
	}
}
