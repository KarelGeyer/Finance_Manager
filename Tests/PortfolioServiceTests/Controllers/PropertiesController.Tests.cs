using Common.Enums;
using Common.Exceptions;
using Common.Helpers;
using Common.Models.PortfolioModels.Properties;
using Common.Models.ProductModels.Loans;
using Common.Models.ProductModels.Properties;
using Common.Response;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PortfolioService.Controllers;
using PortfolioService.Db;

namespace PortfolioServiceTests.Controllers
{
    public class PropertiesControllerTests
    {
        private readonly IDbService<Property> _dbService;

        private PropertiesController _controller;

        private Property _Property;
        private Property _Property2;
        private List<Property> _Propertys;

        private int _ownerId = 1;

        public PropertiesControllerTests()
        {
            _dbService = Substitute.For<IDbService<Property>>();

            _controller = new PropertiesController(_dbService);

            _Property = new()
            {
                Id = 1,
                OwnerId = _ownerId,
                Name = "Test",
                Value = 100,
                CategoryId = 1
            };
            _Property2 = new()
            {
                Id = 2,
                OwnerId = _ownerId,
                Name = "Test2",
                Value = 200,
                CategoryId = 2
            };
            _Propertys = new() { _Property, _Property2 };
        }

        [Fact]
        public async Task GetAllProperties_ReturnsCorrectValue()
        {
            // Arrange
            _dbService.GetAllAsync(_ownerId, 1, 2005).Returns(_Propertys);

            // Act
            BaseResponse<List<Property>> result = await _controller.GetAllProperties(_ownerId, 1, 2005);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be(string.Empty);
            result.Status.Should().Be(EHttpStatus.OK);
            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<List<Property>>();
            result.Data.Should().HaveCount(2);
            result.Data[0].Should().BeEquivalentTo(_Property);
        }

        [Fact]
        public async Task GetAllProperties_ThrowsException()
        {
            // Arrange
            _dbService.GetAllAsync(_ownerId, 1, 2005).Throws(x => new Exception());

            // Act
            BaseResponse<List<Property>> result = await _controller.GetAllProperties(_ownerId, 1, 2005);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().BeNull();
            result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
        }
        
        [Fact]
        public async Task GetPropertiesByCategory_ReturnsCorrectValue()
        {
            DateTime date = DateTime.Now;
            int month = date.Month;
            int year = date.Year;
            // Arrange
            _dbService.GetAllAsync(_ownerId, month, year).Returns(_Propertys);

            // Act
            BaseResponse<List<Property>> result = await _controller.GetPropertiesByCategory(_ownerId, 1);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be(string.Empty);
            result.Status.Should().Be(EHttpStatus.OK);
            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<List<Property>>();
            result.Data.Should().HaveCount(1);
            result.Data[0].Should().BeEquivalentTo(_Property);
        }

        [Fact]
        public async Task GetPropertiesByCategory_ThrowsException()
        {
            // Arrange
            _dbService.GetAllAsync(_ownerId, 1, 2005).Throws(x => new Exception());

            // Act
            BaseResponse<List<Property>> result = await _controller.GetPropertiesByCategory(_ownerId, 1);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().BeNull();
            result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
        }

        [Fact]
        public async Task GetProperty_ReturnsCorrectValue()
        {
            // Arrange
            _dbService.GetAsync(1).Returns(_Property);

            // Act
            BaseResponse<Property> result = await _controller.GetProperty(1);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be(string.Empty);
            result.Status.Should().Be(EHttpStatus.OK);
            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<Property>();
            result.Data.Should().BeEquivalentTo(_Property);
        }

        [Fact]
        public async Task GetProperty_ThrowsException()
        {
            // Arrange
            _dbService.GetAsync(1).Throws(new Exception());

            // Act
            BaseResponse<Property> result = await _controller.GetProperty(1);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task GetProperty_ThrowsNotFoundException()
        {
            // Arrange
            _dbService.GetAsync(1).Throws(new NotFoundException());

            // Act
            BaseResponse<Property> result = await _controller.GetProperty(1);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be("No record was found");
            result.Status.Should().Be(EHttpStatus.NOT_FOUND);
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task CreateProperty_ReturnsCorrectValue()
        {
            // Arrange
            CreateProperty createProperty = new()
            {
                OwnerId = _ownerId,
                Name = "Test",
                Value = 100,
                CategoryId = 1
            };

            _dbService.CreateAsync(Arg.Any<Property>()).Returns(true);

            // Act
            BaseResponse<bool> result = await _controller.CreateProperty(createProperty);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be(string.Empty);
            result.Status.Should().Be(EHttpStatus.OK);
            result.Data.Should().BeTrue();
        }

        [Fact]
        public async Task CreateProperty_ThrowsException()
        {
            // Arrange
            CreateProperty createProperty = new()
            {
                OwnerId = _ownerId,
                Name = "Test",
                Value = 100,
                CategoryId = 1
            };
            _dbService.CreateAsync(Arg.Any<Property>()).Throws(x => new Exception());

            // Act
            BaseResponse<bool> result = await _controller.CreateProperty(createProperty);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task CreateProperty_ThrowsFailedToCreateException()
        {
            // Arrange
            CreateProperty createProperty = new()
            {
                OwnerId = _ownerId,
                Name = "Test",
                Value = 100,
                CategoryId = 1
            };
            _dbService.CreateAsync(Arg.Any<Property>()).Throws(x => new FailedToCreateException<Property>());

            // Act
            BaseResponse<bool> result = await _controller.CreateProperty(createProperty);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToCreateMessage<Property>());
            result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateProperty_ReturnsCorrectValue()
        {
            // Arrange
            UpdateProperty updateProperty = new()
            {
                Id = 1,
                Name = "Test",
                Value = 100,
            };
            _dbService.GetAsync(Arg.Any<int>()).Returns(_Property);
            _dbService.UpdateAsync(Arg.Any<Property>()).Returns(true);

            // Act
            BaseResponse<bool> result = await _controller.UpdateProperty(updateProperty);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.OK);
            result.Data.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateProperty_ThrowsException()
        {
            // Arrange
            UpdateProperty updateProperty = new()
            {
                Id = 1,
                Name = "Test",
                Value = 100,
            };
            _dbService.GetAsync(Arg.Any<int>()).Returns(_Property);
            _dbService.UpdateAsync(Arg.Any<Property>()).Throws(new Exception());

            // Act
            BaseResponse<bool> result = await _controller.UpdateProperty(updateProperty);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateProperty_ThrowsNotFoundException()
        {
            // Arrange
            UpdateProperty updateProperty = new()
            {
                Id = 1,
                Name = "Test",
                Value = 100,
            };
            _dbService.GetAsync(Arg.Any<int>()).Returns(_Property);
            _dbService.UpdateAsync(Arg.Any<Property>()).Throws(new NotFoundException());

            // Act
            BaseResponse<bool> result = await _controller.UpdateProperty(updateProperty);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.NOT_FOUND);
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateProperty_ThrowsFailedToUpdateException()
        {
            // Arrange
            UpdateProperty updateProperty = new()
            {
                Id = 1,
                Name = "Test",
                Value = 100,
            }; 
            _dbService.GetAsync(Arg.Any<int>()).Returns(_Property);
            _dbService.UpdateAsync(Arg.Any<Property>()).Throws(new FailedToUpdateException<Property>());

            // Act
            BaseResponse<bool> result = await _controller.UpdateProperty(updateProperty);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToUpdateMessage<Property>());
            result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteProperty_ReturnsCorrectValue()
        {
            // Arrange
            _dbService.DeleteAsync(1).Returns(true);

            // Act
            BaseResponse<bool> result = await _controller.DeleteProperty(1);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.OK);
            result.Data.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteProperty_ThrowsException()
        {
            // Arrange
            _dbService.DeleteAsync(1).Throws(new Exception());

            // Act
            BaseResponse<bool> result = await _controller.DeleteProperty(1);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.INTERNAL_SERVER_ERROR);
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteProperty_ThrowsNotFoundException()
        {
            // Arrange
            _dbService.DeleteAsync(1).Throws(new NotFoundException());

            // Act
            BaseResponse<bool> result = await _controller.DeleteProperty(1);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(EHttpStatus.NOT_FOUND);
            result.Data.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteProperty_ThrowsFailedToDeleteException()
        {
            // Arrange
            _dbService.DeleteAsync(1).Throws(new FailedToDeleteException<Property>());

            // Act
            BaseResponse<bool> result = await _controller.DeleteProperty(1);

            // Assert
            result.Should().NotBeNull();
            result.ResponseMessage.Should().Be(CustomResponseMessage.GetFailedToDeleteMessage<Property>());
            result.Status.Should().Be(EHttpStatus.BAD_REQUEST);
            result.Data.Should().BeFalse();
        }
    }
}
