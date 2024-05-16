using Common.Exceptions;
using Common.Models.Category;
using Common.Models.ProductModels.Properties;
using FluentAssertions;
using StaticDataService.Db;

namespace StaticDataServiceTests.Db
{
	public class DbServiceTests
	{
		private readonly DataContext _context;

		private readonly DbService<Category> _dbService;
		private readonly Category _category;

		public DbServiceTests()
		{
			_context = GetContext.GetDbContext();
			_dbService = new DbService<Category>(_context);

			_category = new() { Id = 1, Value = "Category", };
		}

		[Fact]
		public async Task GetAsync_RetrievesEntity()
		{
			// Arrange
			_context.Categories.Add(_category);
			await _context.SaveChangesAsync();

			// Act
			Category result = await _dbService.GetAsync(1);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeEquivalentTo(_category);
			_context.Categories.Should().Contain(result);
			_context.Categories.Count().Should().Be(1);

			_context.Remove(_category);
			await _context.SaveChangesAsync();
		}

		[Fact]
		public async Task GetAsync_ThrowsAnException()
		{
			// Act
			try
			{
				Category result = await _dbService.GetAsync(1);
			}
			catch (NotFoundException ex)
			{
				// Assert
				ex.Message.Should().Be("No record was found");
				_context.Categories.Count().Should().Be(0);
			}
		}

		[Fact]
		public async Task GetAllAsync_RetrievesEntities()
		{
			// Arrange
			_context.Categories.Add(_category);
			await _context.SaveChangesAsync();

			// Act
			List<Category> result = await _dbService.GetAllAsync();

			// Assert
			result.Should().NotBeNull();
			result[0].Should().BeEquivalentTo(_category);
			_context.Categories.Should().Contain(result);
			_context.Categories.Count().Should().Be(1);

			_context.Remove(_category);
			await _context.SaveChangesAsync();
		}

		[Fact]
		public async Task GetAllAsync_ThrowsAnException()
		{
			// Act
			try
			{
				await _dbService.GetAllAsync();
			}
			catch (NotFoundException ex)
			{
				// Assert
				ex.Message.Should().Be("No record was found");
				_context.Categories.Count().Should().Be(0);
			}
		}
	}
}
