using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
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
    public class SavingsControllerTests
    {
        private readonly ILogger<SavingsController> _logger;
        private readonly IDbService<Savings> _dbService;

        private SavingsController _controller;

        private Savings _Savings;

        private int _ownerId = 1;

        public SavingsControllerTests()
        {
            _dbService = Substitute.For<IDbService<Savings>>();
            _logger = Substitute.For<ILogger<SavingsController>>();
            _controller = new SavingsController(_dbService);

            _Savings = new()
            {
                Id = 1,
                OwnerId = _ownerId,
                Amount = 100
            };
        }


        [Fact]
        public async Task GetSavings_ReturnsCorrectValue()
        {
            // Arrange
            _dbService.GetAsync(1).Returns(_Savings);

            // Act
            BaseResponse<double> result = await _controller.GetSavings(1);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be(string.Empty);
            result.Status.Should().Be(EHttpStatus.OK);
            result.Data.Should().Be(100);
        }

        [Fact]
        public async Task GetSavings_ThrowsException()
        {
            // Arrange
            _dbService.GetAsync(1).Throws(new Exception());

            // Act
            BaseResponse<double> result = await _controller.GetSavings(1);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
            result.Data.Should().Be(0);
        }

        [Fact]
        public async Task GetSavings_ThrowsNotFoundException()
        {
            // Arrange
            _dbService.GetAsync(1).Throws(new NotFoundException());

            // Act
            BaseResponse<double> result = await _controller.GetSavings(1);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be("No record was found");
            result.Status.Should().Be(EHttpStatus.NOT_FOUND);
            result.Data.Should().Be(0);
        }

        [Fact]
        public async Task CreateSavings_ReturnsCorrectValue()
        {
            // Arrange
            CreateSavings createSavings = new()
            {
                OwnerId = _ownerId,
                Amount = 300,
            };

            _dbService.CreateAsync(Arg.Any<Savings>()).Returns(true);

            // Act
            BaseResponse<bool> result = await _controller.AddSavings(createSavings);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be(string.Empty);
            result.Status.Should().Be(EHttpStatus.OK);
            result.Data.Should().BeTrue();
        }

        [Fact]
        public async Task CreateSavings_ThrowsException()
        {
            // Arrange
            CreateSavings createSavings = new()
            {
                OwnerId = _ownerId,
                Amount = 300,
            };
            _dbService.CreateAsync(Arg.Any<Savings>()).Throws(x => new Exception());

            // Act
            BaseResponse<bool> result = await _controller.AddSavings(createSavings);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateSavings_ReturnsCorrectValue()
        {
            // Arrange
            UpdateSavings updateSavings = new()
            {
                Id = 1,
                Amount = 200,
            };
            _dbService.GetAsync(Arg.Any<int>()).Returns(_Savings);
            _dbService.UpdateAsync(Arg.Any<Savings>()).Returns(true);

            // Act
            BaseResponse<bool> result = await _controller.UpdateSavings(updateSavings);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.OK);
            result.Data.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateSavings_ThrowsException()
        {
            // Arrange
            UpdateSavings updateSavings = new()
            {
                Id = 1,
                Amount = 200,
            };
            _dbService.GetAsync(Arg.Any<int>()).Returns(_Savings);
            _dbService.UpdateAsync(Arg.Any<Savings>()).Throws(new Exception());

            // Act
            BaseResponse<bool> result = await _controller.UpdateSavings(updateSavings);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateSavings_ThrowsNotFoundException()
        {
            // Arrange
            UpdateSavings updateSavings = new()
            {
                Id = 1,
                Amount = 200,
            };
            _dbService.GetAsync(Arg.Any<int>()).Returns(_Savings);
            _dbService.UpdateAsync(Arg.Any<Savings>()).Throws(new NotFoundException());

            // Act
            BaseResponse<bool> result = await _controller.UpdateSavings(updateSavings);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.NOT_FOUND);
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateSavings_ThrowsFailedToUpdateException()
        {
            // Arrange
            UpdateSavings updateSavings = new()
            {
                Id = 1,
                Amount = 200,
            };
            _dbService.GetAsync(Arg.Any<int>()).Returns(_Savings);
            _dbService.UpdateAsync(Arg.Any<Savings>()).Throws(new FailedToUpdateException<Savings>());

            // Act
            BaseResponse<bool> result = await _controller.UpdateSavings(updateSavings);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToUpdateMessage<Savings>());
            result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteSavings_ReturnsCorrectValue()
        {
            // Arrange
            _dbService.DeleteAsync(1).Returns(true);

            // Act
            BaseResponse<bool> result = await _controller.DeleteSavings(1);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.OK);
            result.Data.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteSavings_ThrowsException()
        {
            // Arrange
            _dbService.DeleteAsync(1).Throws(new Exception());

            // Act
            BaseResponse<bool> result = await _controller.DeleteSavings(1);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteSavings_ThrowsNotFoundException()
        {
            // Arrange
            _dbService.DeleteAsync(1).Throws(new NotFoundException());

            // Act
            BaseResponse<bool> result = await _controller.DeleteSavings(1);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.NOT_FOUND);
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteSavings_ThrowsFailedToDeleteException()
        {
            // Arrange
            _dbService.DeleteAsync(1).Throws(new FailedToDeleteException<Savings>());

            // Act
            BaseResponse<bool> result = await _controller.DeleteSavings(1);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToDeleteMessage<Savings>());
            result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
            result.Data.Should().BeFalse();
        }
    }
}
