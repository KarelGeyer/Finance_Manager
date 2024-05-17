using Common.Enums;
using Common.Exceptions;
using Common.Models.Currency;
using Common.Response;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using StaticDataService.Controllers;
using StaticDataService.Interfaces;

namespace StaticDataServiceTests.Controllers
{
	public class CurrencyControllerTests
	{
		private readonly IDbService<Currency> _dbService;
		private readonly ILogger<CurrencyController> _logger;

		private CurrencyController _controller;

		private Currency _Currency;
		private Currency _Currency2;
		private List<Currency> _categories;

		public CurrencyControllerTests()
		{
			_dbService = Substitute.For<IDbService<Currency>>();
			_logger = Substitute.For<ILogger<CurrencyController>>();
			_controller = new CurrencyController(_dbService);

			_Currency = new() { Id = 1, Value = "CZK", };
			_Currency2 = new() { Id = 2, Value = "USD", };
			_categories = new() { _Currency, _Currency2 };
		}

		[Fact]
		public async Task GetAllCurrencies_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.GetAllAsync().Returns(_categories);

			// Act
			BaseResponse<List<Currency>> result = await _controller.GetAllCurrencies();

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().NotBeNull();
			result.Data.Should().BeOfType<List<Currency>>();
			result.Data.Should().HaveCount(2);
			result.Data[0].Should().BeEquivalentTo(_Currency);
		}

		[Fact]
		public async Task GetAllCurrencies_ThrowsException()
		{
			// Arrange
			_dbService.GetAllAsync().Throws(x => new Exception());

			// Act
			BaseResponse<List<Currency>> result = await _controller.GetAllCurrencies();

			// Assert
			result.Should().NotBeNull();
			result.Data.Should().BeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
		}

		[Fact]
		public async Task GetAllCurrencies_ThrowsNotFoundException()
		{
			// Arrange
			_dbService.GetAllAsync().Throws(x => new NotFoundException());

			// Act
			BaseResponse<List<Currency>> result = await _controller.GetAllCurrencies();

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be("No record was found");
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeNull();
		}

		[Fact]
		public async Task GetCurrency_ReturnsCorrectValue()
		{
			// Arrange
			_dbService.GetAsync(1).Returns(_Currency);

			// Act
			BaseResponse<Currency> result = await _controller.GetCurrency(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be(string.Empty);
			result.Status.Should().Be(EHttpStatus.OK);
			result.Data.Should().NotBeNull();
			result.Data.Should().BeOfType<Currency>();
			result.Data.Should().BeEquivalentTo(_Currency);
		}

		[Fact]
		public async Task GetCurrency_ThrowsException()
		{
			// Arrange
			_dbService.GetAsync(1).Throws(new Exception());

			// Act
			BaseResponse<Currency> result = await _controller.GetCurrency(1);

			// Assert
			result.Should().NotBeNull();
			result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
			result.Data.Should().BeNull();
		}

		[Fact]
		public async Task GetCurrency_ThrowsNotFoundException()
		{
			// Arrange
			_dbService.GetAsync(1).Throws(new NotFoundException());

			// Act
			BaseResponse<Currency> result = await _controller.GetCurrency(1);

			// Assert
			result.Should().NotBeNull();
			result.ResponseMessage.Should().Be("No record was found");
			result.Status.Should().Be(EHttpStatus.NOT_FOUND);
			result.Data.Should().BeNull();
		}
	}
}
