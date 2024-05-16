using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Models.Expenses;
using Common.Models.Savings;
using Common.Response;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PortfolioService.Controllers;
using PortfolioService.Db;

namespace PortfolioServiceTests.Controllers
{
	public class ExpensesControllerTests
	{
		private readonly ILogger<ExpensesController> _logger;
		private readonly IDbService<Expense> _dbService;

		private ExpensesController _controller;

		private Expense _expense;
		private Expense _expense2;
		private List<Expense> _expenses;

		private int _ownerId = 1;

		public ExpensesControllerTests()
		{
			_logger = Substitute.For<ILogger<ExpensesController>>();
			_dbService = Substitute.For<IDbService<Expense>>();

			_controller = new ExpensesController(_logger, _dbService);

			_expense = new()
			{
				Id = 1,
				OwnerId = _ownerId,
				Name = "Test",
				Value = 100,
				CategoryId = 1
			};
			_expense2 = new()
			{
				Id = 2,
				OwnerId = _ownerId,
				Name = "Test2",
				Value = 200,
				CategoryId = 2
			};
			_expenses = new() { _expense, _expense2 };
		}

		[Fact]
		public async Task GetAllExpenses_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.GetAllAsync(_ownerId).Returns(_expenses);

			// Act
			BaseResponse<List<Expense>> result = await _controller.GetAllExpenses(_ownerId);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().NotBeNull();
			result.Data.Should().BeOfType<List<Expense>>();
			result.Data.Should().HaveCount(2);
			result.Data[0].Should().BeEquivalentTo(_expense);
		}

		[Fact]
		public async Task GetAllExpenses_ThrowsException()
		{
			// Arrange
			_dbService.GetAllAsync(_ownerId).Throws(x => new Exception());

			// Act
			BaseResponse<List<Expense>> result = await _controller.GetAllExpenses(_ownerId);

			// Assert
			result.Should().NotBeNull();
			result.Data.Should().BeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
		}

		[Fact]
		public async Task GetExpense_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.GetAsync(1).Returns(_expense);

			// Act
			BaseResponse<Expense> result = await _controller.GetExpense(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().NotBeNull();
			result.Data.Should().BeOfType<Expense>();
			result.Data.Should().BeEquivalentTo(_expense);
		}

		[Fact]
		public async Task GetExpense_ThrowsException()
		{
			// Arrange
			_dbService.GetAsync(1).Throws(new Exception());

			// Act
			BaseResponse<Expense> result = await _controller.GetExpense(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeNull();
		}

		[Fact]
		public async Task GetExpense_ThrowsNotFoundException()
		{
			// Arrange
			_dbService.GetAsync(1).Throws(new NotFoundException());

			// Act
			BaseResponse<Expense> result = await _controller.GetExpense(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be("No record was found");
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeNull();
		}

		[Fact]
		public async Task CreateExpense_ReturnsCorrectValue()
		{
			// Arrange
			CreateExpense createExpense =
				new()
				{
					OwnerId = _ownerId,
					Name = "Test",
					Value = 100,
					CategoryId = 1
				};

			_dbService.CreateAsync(Arg.Any<Expense>()).Returns(true);

			// Act
			BaseResponse<bool> result = await _controller.CreateExpense(createExpense);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().BeTrue();
		}

		[Fact]
		public async Task CreateExpense_ThrowsException()
		{
			// Arrange
			CreateExpense createExpense =
				new()
				{
					OwnerId = _ownerId,
					Name = "Test",
					Value = 100,
					CategoryId = 1
				};
			_dbService.CreateAsync(Arg.Any<Expense>()).Throws(x => new Exception());

			// Act
			BaseResponse<bool> result = await _controller.CreateExpense(createExpense);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task CreateProperty_ThrowsFailedToCreateException()
		{
			// Arrange
			CreateExpense createExpense =
				new()
				{
					OwnerId = _ownerId,
					Name = "Test",
					Value = 100,
					CategoryId = 1
				};
			_dbService.CreateAsync(Arg.Any<Expense>()).Throws(x => new FailedToCreateException<Expense>());

			// Act
			BaseResponse<bool> result = await _controller.CreateExpense(createExpense);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToCreateMessage<Expense>());
			result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task UpdateExpense_ReturnsCorrectValue()
		{
			// Arrange
			UpdateExpense updateExpense =
				new()
				{
					Id = 1,
					Name = "Test",
					Value = 100,
				};
			_dbService.GetAsync(Arg.Any<int>()).Returns(_expense);
			_dbService.UpdateAsync(Arg.Any<Expense>()).Returns(true);

			// Act
			BaseResponse<bool> result = await _controller.UpdateExpense(updateExpense);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().BeTrue();
		}

		[Fact]
		public async Task UpdateExpense_ThrowsException()
		{
			// Arrange
			UpdateExpense updateExpense =
				new()
				{
					Id = 1,
					Name = "Test",
					Value = 100,
				};
			_dbService.GetAsync(Arg.Any<int>()).Returns(_expense);
			_dbService.UpdateAsync(Arg.Any<Expense>()).Throws(new Exception());

			// Act
			BaseResponse<bool> result = await _controller.UpdateExpense(updateExpense);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task UpdateExpense_ThrowsNotFoundException()
		{
			// Arrange
			UpdateExpense updateExpense =
				new()
				{
					Id = 1,
					Name = "Test",
					Value = 100,
				};
			_dbService.GetAsync(Arg.Any<int>()).Returns(_expense);
			_dbService.UpdateAsync(Arg.Any<Expense>()).Throws(new NotFoundException());

			// Act
			BaseResponse<bool> result = await _controller.UpdateExpense(updateExpense);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task UpdateExpense_ThrowsFailedToUpdateException()
		{
			// Arrange
			UpdateExpense updateExpense =
				new()
				{
					Id = 1,
					Name = "Test",
					Value = 100,
				};
			_dbService.GetAsync(Arg.Any<int>()).Returns(_expense);
			_dbService.UpdateAsync(Arg.Any<Expense>()).Throws(new FailedToUpdateException<Expense>());

			// Act
			BaseResponse<bool> result = await _controller.UpdateExpense(updateExpense);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToUpdateMessage<Expense>());
			result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task DeleteExpense_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.DeleteAsync(1).Returns(true);

			// Act
			BaseResponse<bool> result = await _controller.DeleteExpense(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().BeTrue();
		}

		[Fact]
		public async Task DeleteExpense_ThrowsException()
		{
			// Arrange
			_dbService.DeleteAsync(1).Throws(new Exception());

			// Act
			BaseResponse<bool> result = await _controller.DeleteExpense(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task DeleteExpense_ThrowsNotFoundException()
		{
			// Arrange
			_dbService.DeleteAsync(1).Throws(new NotFoundException());

			// Act
			BaseResponse<bool> result = await _controller.DeleteExpense(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task DeleteExpense_ThrowsFailedToDeleteException()
		{
			// Arrange
			_dbService.DeleteAsync(1).Throws(new FailedToDeleteException<Expense>());

			// Act
			BaseResponse<bool> result = await _controller.DeleteExpense(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToDeleteMessage<Expense>());
			result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
			result.Data.Should().BeFalse();
		}
	}
}
