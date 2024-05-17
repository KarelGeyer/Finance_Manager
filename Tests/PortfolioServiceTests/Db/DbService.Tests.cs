using Common.Exceptions;
using Common.Models.ProductModels.Properties;
using FluentAssertions;
using PortfolioService.Db;
using PortfolioService.Services;

namespace PortfolioServiceTests.Db
{
	public class DbServiceTests
	{
		private readonly DataContext _context;

		private readonly DbService<Property> _dbService;
		private readonly Property _property;

		public DbServiceTests()
		{
			_context = GetContext.GetDbContext();
			_dbService = new DbService<Property>(_context);

			_property = new()
			{
				Id = 1,
				Name = "Test Property",
				Value = 100000,
				OwnerId = 1
			};
		}

		[Fact]
		public async Task CreateAsync_CreatesEntity()
		{
			// Act
			await _dbService.CreateAsync(_property);

			// Assert
			_context.Properties.Count().Should().Be(1);
			_context.Properties.First().Should().BeEquivalentTo(_property);
		}

		[Fact]
		public async Task DeleteAsync_DeletesEntity()
		{
			// Arrange
			_context.Properties.Add(_property);
			await _context.SaveChangesAsync();

			// Act
			bool result = await _dbService.DeleteAsync(1);

			// Assert
			result.Should().BeTrue();
			_context.Properties.Count().Should().Be(0);

			try
			{
				_context.Remove(_property);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		[Fact]
		public async Task DeleteAsync_FailsToDeleteEntity()
		{
			// Act
			try
			{
				bool result = await _dbService.DeleteAsync(1);
			}
			catch (NotFoundException ex)
			{
				// Assert
				_context.Properties.Count().Should().Be(0);
				ex.Message.Should().Be("No record was found");
			}
		}

		[Fact]
		public async Task GetAsync_RetrievesEntity()
		{
			// Arrange
			_context.Properties.Add(_property);
			await _context.SaveChangesAsync();

			// Act
			Property result = await _dbService.GetAsync(1);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeEquivalentTo(_property);
			_context.Properties.Should().Contain(result);
			_context.Properties.Count().Should().Be(1);

			_context.Remove(_property);
			await _context.SaveChangesAsync();
		}

		[Fact]
		public async Task GetAsync_ThrowsAnException()
		{
			// Act
			try
			{
				Property result = await _dbService.GetAsync(1);
			}
			catch (NotFoundException ex)
			{
				// Assert
				ex.Message.Should().Be("No record was found");
				_context.Properties.Count().Should().Be(0);
			}
		}

		[Fact]
		public async Task GetAllAsync_RetrievesEntities()
		{
			// Arrange
			_context.Properties.Add(_property);
			await _context.SaveChangesAsync();

			// Act
			List<Property> result = await _dbService.GetAllAsync(1);

			// Assert
			result.Should().NotBeNull();
			result[0].Should().BeEquivalentTo(_property);
			_context.Properties.Should().Contain(result);
			_context.Properties.Count().Should().Be(1);

			_context.Remove(_property);
			await _context.SaveChangesAsync();
		}

		[Fact]
		public async Task GetAllAsync_ThrowsAnException()
		{
			// Act
			List<Property> result = await _dbService.GetAllAsync(1);

			// Assert
			result.Should().BeEmpty();
			_context.Properties.Count().Should().Be(0);
		}

		[Fact]
		public async Task UpdateAsync_UpdatesEntity()
		{
			Property property =
				new()
				{
					Id = 1,
					Name = "Updated Property",
					Value = 200000,
					OwnerId = 1
				};

			// Act
			bool result = await _dbService.UpdateAsync(property);

			// Assert
			result.Should().BeTrue();
			_context.Properties.Count().Should().Be(1);
			_context.Properties.First().Id.Should().Be(1);
			_context.Properties.First().Name.Should().Be("Updated Property");

			try
			{
				_context.Remove(_property);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		[Fact]
		public async Task UpdateAsync_ThrowsAnException()
		{
			Property property =
				new()
				{
					Id = 1,
					Name = "Updated Property",
					Value = 200000,
					OwnerId = 1
				};

			// Act
			try
			{
				await _dbService.UpdateAsync(property);
			}
			catch (NotFoundException ex)
			{
				// Assert
				ex.Message.Should().Be(string.Format("No record by id {0} was found", property.Id));
				_context.Properties.Count().Should().Be(0);
			}
		}
	}
}
