using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Models.ProductModels.Income;
using Common.Models.ProductModels.Loans;
using Common.Response;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PortfolioService.Controllers;
using PortfolioService.Db;

namespace PortfolioServiceTests.Controllers
{
	public class LoansControllerTests
	{
		private readonly IDbService<Loan> _dbService;

		private LoansController _controller;

		private Loan _loan;
		private Loan _loan2;
		private List<Loan> _loans;

		private int _ownerId = 1;

		public LoansControllerTests()
		{
			_dbService = Substitute.For<IDbService<Loan>>();

			_controller = new LoansController(_dbService);

			_loan = new()
			{
				Id = 1,
				OwnerId = _ownerId,
				Name = "Test",
				Value = 100,
				To = 2
			};
			_loan2 = new()
			{
				Id = 2,
				OwnerId = _ownerId,
				Name = "Test2",
				Value = 200,
				To = 2
			};
			_loans = new() { _loan, _loan2 };
		}

		[Fact]
		public async Task GetAllLoans_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.GetAllAsync(_ownerId, 1, 2005).Returns(_loans);

			// Act
			BaseResponse<List<Loan>> result = await _controller.GetAllLoans(_ownerId, 1, 2005);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().NotBeNull();
			result.Data.Should().BeOfType<List<Loan>>();
			result.Data.Should().HaveCount(2);
			result.Data[0].Should().BeEquivalentTo(_loan);
		}

		[Fact]
		public async Task GetAllLoans_ThrowsException()
		{
			// Arrange
			_dbService.GetAllAsync(_ownerId, 1, 2005).Throws(x => new Exception());

			// Act
			BaseResponse<List<Loan>> result = await _controller.GetAllLoans(_ownerId, 1, 2005);

			// Assert
			result.Should().NotBeNull();
			result.Data.Should().BeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
		}

		[Fact]
		public async Task GetAllLoansByCreditor_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.GetAllAsync(_ownerId).Returns(_loans);

			// Act
			BaseResponse<List<Loan>> result = await _controller.GetAllLoansByCreditor(_ownerId, _loan.To);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().NotBeNull();
			result.Data.Should().BeOfType<List<Loan>>();
			result.Data.Should().HaveCount(2);
			result.Data[0].Should().BeEquivalentTo(_loan);
		}

		[Fact]
		public async Task GetAllLoansByCreditor_ThrowsException()
		{
			// Arrange
			_dbService.GetAllAsync(_ownerId).Throws(x => new Exception());

			// Act
			BaseResponse<List<Loan>> result = await _controller.GetAllLoansByCreditor(_ownerId, _loan.To);

			// Assert
			result.Should().NotBeNull();
			result.Data.Should().BeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
		}

		[Fact]
		public async Task GetLoan_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.GetAsync(1).Returns(_loan);

			// Act
			BaseResponse<Loan> result = await _controller.GetLoan(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().NotBeNull();
			result.Data.Should().BeOfType<Loan>();
			result.Data.Should().BeEquivalentTo(_loan);
		}

		[Fact]
		public async Task GetLoan_ThrowsException()
		{
			// Arrange
			_dbService.GetAsync(1).Throws(new Exception());

			// Act
			BaseResponse<Loan> result = await _controller.GetLoan(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeNull();
		}

		[Fact]
		public async Task GetLoan_ThrowsNotFoundException()
		{
			// Arrange
			_dbService.GetAsync(1).Throws(new NotFoundException());

			// Act
			BaseResponse<Loan> result = await _controller.GetLoan(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be("No record was found");
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeNull();
		}

		[Fact]
		public async Task CreateLoan_ReturnsCorrectValue()
		{
			// Arrange
			CreateLoan createLoan =
				new()
				{
					OwnerId = _ownerId,
					Name = "Test",
					Value = 100,
					OwnToId = 1
				};

			_dbService.CreateAsync(Arg.Any<Loan>()).Returns(true);

			// Act
			BaseResponse<bool> result = await _controller.CreateLoan(createLoan);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().BeTrue();
		}

		[Fact]
		public async Task CreateLoan_ThrowsException()
		{
			// Arrange
			CreateLoan createLoan =
				new()
				{
					OwnerId = _ownerId,
					Name = "Test",
					Value = 100,
					OwnToId = 1
				};
			_dbService.CreateAsync(Arg.Any<Loan>()).Throws(x => new Exception());

			// Act
			BaseResponse<bool> result = await _controller.CreateLoan(createLoan);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task CreateLoan_ThrowsFailedToCreateException()
		{
			// Arrange
			CreateLoan createLoan =
				new()
				{
					OwnerId = _ownerId,
					Name = "Test",
					Value = 100,
					OwnToId = 1
				};
			_dbService.CreateAsync(Arg.Any<Loan>()).Throws(x => new FailedToCreateException<Loan>());

			// Act
			BaseResponse<bool> result = await _controller.CreateLoan(createLoan);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToCreateMessage<Loan>());
			result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task UpdateLoan_ReturnsCorrectValue()
		{
			// Arrange
			UpdateLoan updateLoan =
				new()
				{
					Id = 1,
					Name = "Test",
					Value = 100,
				};
			_dbService.GetAsync(Arg.Any<int>()).Returns(_loan);
			_dbService.UpdateAsync(Arg.Any<Loan>()).Returns(true);

			// Act
			BaseResponse<bool> result = await _controller.UpdateLoan(updateLoan);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().BeTrue();
		}

		[Fact]
		public async Task UpdateLoan_ThrowsException()
		{
			// Arrange
			UpdateLoan updateLoan =
				new()
				{
					Id = 1,
					Name = "Test",
					Value = 100,
				};
			_dbService.GetAsync(Arg.Any<int>()).Returns(_loan);
			_dbService.UpdateAsync(Arg.Any<Loan>()).Throws(new Exception());

			// Act
			BaseResponse<bool> result = await _controller.UpdateLoan(updateLoan);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task UpdateLoan_ThrowsNotFoundException()
		{
			// Arrange
			UpdateLoan updateLoan =
				new()
				{
					Id = 1,
					Name = "Test",
					Value = 100,
				};
			_dbService.GetAsync(Arg.Any<int>()).Returns(_loan);
			_dbService.UpdateAsync(Arg.Any<Loan>()).Throws(new NotFoundException());

			// Act
			BaseResponse<bool> result = await _controller.UpdateLoan(updateLoan);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task UpdateLoan_ThrowsFailedToUpdateException()
		{
			// Arrange
			UpdateLoan updateLoan =
				new()
				{
					Id = 1,
					Name = "Test",
					Value = 100,
				};
			_dbService.GetAsync(Arg.Any<int>()).Returns(_loan);
			_dbService.UpdateAsync(Arg.Any<Loan>()).Throws(new FailedToUpdateException<Loan>());

			// Act
			BaseResponse<bool> result = await _controller.UpdateLoan(updateLoan);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToUpdateMessage<Loan>());
			result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task DeleteLoan_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.DeleteAsync(1).Returns(true);

			// Act
			BaseResponse<bool> result = await _controller.DeleteLoan(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().BeTrue();
		}

		[Fact]
		public async Task DeleteLoan_ThrowsException()
		{
			// Arrange
			_dbService.DeleteAsync(1).Throws(new Exception());

			// Act
			BaseResponse<bool> result = await _controller.DeleteLoan(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task DeleteLoan_ThrowsNotFoundException()
		{
			// Arrange
			_dbService.DeleteAsync(1).Throws(new NotFoundException());

			// Act
			BaseResponse<bool> result = await _controller.DeleteLoan(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeFalse();
		}

		[Fact]
		public async Task DeleteLoan_ThrowsFailedToDeleteException()
		{
			// Arrange
			_dbService.DeleteAsync(1).Throws(new FailedToDeleteException<Loan>());

			// Act
			BaseResponse<bool> result = await _controller.DeleteLoan(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToDeleteMessage<Loan>());
			result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
			result.Data.Should().BeFalse();
		}
	}
}
