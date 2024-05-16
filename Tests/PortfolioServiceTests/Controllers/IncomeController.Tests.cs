using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Models.PortfolioModels.Properties;
using Common.Models.ProductModels.Income;
using Common.Models.ProductModels.Properties;
using Common.Response;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PortfolioService.Controllers;
using PortfolioService.Db;

namespace PortfolioServiceTests.Controllers
{
	public class IncomeControllerTests
	{
		private readonly ILogger<IncomeController> _logger;
		private readonly IDbService<Income> _dbService;

		private IncomeController _controller;

		private Income _income;
		private Income _income2;
		private List<Income> _incomes;

		private int _ownerId = 1;

		public IncomeControllerTests()
		{
			_dbService = Substitute.For<IDbService<Income>>();
			_logger = Substitute.For<ILogger<IncomeController>>();
			_controller = new IncomeController(_logger, _dbService);

			_income = new()
			{
				Id = 1,
				OwnerId = _ownerId,
				Name = "Test",
				Value = 100,
				CategoryId = 2
			};
			_income2 = new()
			{
				Id = 2,
				OwnerId = _ownerId,
				Name = "Test2",
				Value = 200,
				CategoryId = 2
			};
			_incomes = new() { _income, _income2 };
		}

		[Fact]
		public async Task GetAllIncomes_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.GetAllAsync(_ownerId).Returns(_incomes);

			// Act
			BaseResponse<List<Income>> result = await _controller.GetAllIncomes(_ownerId);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().NotBeNull();
			result.Data.Should().BeOfType<List<Income>>();
			result.Data.Should().HaveCount(2);
			result.Data[0].Should().BeEquivalentTo(_income);
		}

		[Fact]
		public async Task GetAllIncomes_ThrowsException()
		{
			// Arrange
			_dbService.GetAllAsync(_ownerId).Throws(x => new Exception());

			// Act
			BaseResponse<List<Income>> result = await _controller.GetAllIncomes(_ownerId);

			// Assert
			result.Should().NotBeNull();
			result.Data.Should().BeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
		}

		[Fact]
		public async Task GetIncome_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.GetAsync(1).Returns(_income);

			// Act
			BaseResponse<Income> result = await _controller.GetIncome(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().NotBeNull();
			result.Data.Should().BeOfType<Income>();
			result.Data.Should().BeEquivalentTo(_income);
		}

		[Fact]
		public async Task GetIncome_ThrowsException()
		{
			// Arrange
			_dbService.GetAsync(1).Throws(new Exception());

			// Act
			BaseResponse<Income> result = await _controller.GetIncome(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeNull();
		}

		[Fact]
		public async Task GetIncome_ThrowsNotFoundException()
		{
			// Arrange
			_dbService.GetAsync(1).Throws(new NotFoundException());

			// Act
			BaseResponse<Income> result = await _controller.GetIncome(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be("No record was found");
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeNull();
		}

		[Fact]
		public async Task CreateIncome_ReturnsCorrectValue()
		{
			// Arrange
			CreateIncome createIncome =
				new()
				{
					OwnerId = _ownerId,
					Name = "Test",
					Value = 100,
					CategoryId = 1
				};

			_dbService.CreateAsync(Arg.Any<Income>()).Returns(true);

			// Act
			BaseResponse<bool> result = await _controller.CreateIncome(createIncome);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().BeTrue();
		}

		[Fact]
		public async Task CreateIncome_ThrowsException()
		{
			// Arrange
			CreateIncome createIncome =
				new()
				{
					OwnerId = _ownerId,
					Name = "Test",
					Value = 100,
					CategoryId = 1
				};
			_dbService.CreateAsync(Arg.Any<Income>()).Throws(x => new Exception());

			// Act
			BaseResponse<bool> result = await _controller.CreateIncome(createIncome);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task CreateIncome_ThrowsFailedToCreateException()
		{
			// Arrange
			CreateIncome createIncome =
				new()
				{
					OwnerId = _ownerId,
					Name = "Test",
					Value = 100,
					CategoryId = 1
				};
			_dbService.CreateAsync(Arg.Any<Income>()).Throws(x => new FailedToCreateException<Income>(1));

			// Act
			BaseResponse<bool> result = await _controller.CreateIncome(createIncome);

			// Assert
			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToCreateMessage<Income>(1));
			result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task UpdateIncome_ReturnsCorrectValue()
		{
			// Arrange
			UpdateIncome updateIncome =
				new()
				{
					Id = 1,
					Name = "Test",
					Value = 100,
				};
			_dbService.GetAsync(Arg.Any<int>()).Returns(_income);
			_dbService.UpdateAsync(Arg.Any<Income>()).Returns(true);

			// Act
			BaseResponse<bool> result = await _controller.UpdateIncome(updateIncome);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().BeTrue();
		}

		[Fact]
		public async Task UpdateIncome_ThrowsException()
		{
			// Arrange
			UpdateIncome updateIncome =
				new()
				{
					Id = 1,
					Name = "Test",
					Value = 100,
				};
			_dbService.GetAsync(Arg.Any<int>()).Returns(_income);
			_dbService.UpdateAsync(Arg.Any<Income>()).Throws(new Exception());

			// Act
			BaseResponse<bool> result = await _controller.UpdateIncome(updateIncome);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task UpdateIncome_ThrowsNotFoundException()
		{
			// Arrange
			UpdateIncome updateIncome =
				new()
				{
					Id = 1,
					Name = "Test",
					Value = 100,
				};
			_dbService.GetAsync(Arg.Any<int>()).Returns(_income);
			_dbService.UpdateAsync(Arg.Any<Income>()).Throws(new NotFoundException());

			// Act
			BaseResponse<bool> result = await _controller.UpdateIncome(updateIncome);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task UpdateIncome_ThrowsFailedToUpdateException()
		{
			// Arrange
			UpdateIncome updateIncome =
				new()
				{
					Id = 1,
					Name = "Test",
					Value = 100,
				};
			_dbService.GetAsync(Arg.Any<int>()).Returns(_income);
			_dbService.UpdateAsync(Arg.Any<Income>()).Throws(new FailedToUpdateException<Income>(updateIncome.Id));

			// Act
			BaseResponse<bool> result = await _controller.UpdateIncome(updateIncome);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToUpdateMessage<Income>(updateIncome.Id));
			result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task DeleteIncome_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.DeleteAsync(1).Returns(true);

			// Act
			BaseResponse<bool> result = await _controller.DeleteIncome(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().BeTrue();
		}

		[Fact]
		public async Task DeleteIncome_ThrowsException()
		{
			// Arrange
			_dbService.DeleteAsync(1).Throws(new Exception());

			// Act
			BaseResponse<bool> result = await _controller.DeleteIncome(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task DeleteIncome_ThrowsNotFoundException()
		{
			// Arrange
			_dbService.DeleteAsync(1).Throws(new NotFoundException());

			// Act
			BaseResponse<bool> result = await _controller.DeleteIncome(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task DeleteIncome_ThrowsFailedToDeleteException()
		{
			// Arrange
			_dbService.DeleteAsync(1).Throws(new FailedToDeleteException<Income>(1));

			// Act
			BaseResponse<bool> result = await _controller.DeleteIncome(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToDeleteMessage<Income>(1));
			result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
			result.Data.Should().BeFalse();
		}
	}
}
