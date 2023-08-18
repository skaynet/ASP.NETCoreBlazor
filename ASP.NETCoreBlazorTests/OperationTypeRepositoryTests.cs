using ASP.NETCoreBlazor.Data;
using ASP.NETCoreBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreBlazorTests
{
    public class OperationTypeRepositoryTests : IDisposable
    {
        private readonly DbContextOptions<FinanceContext> _dbContextOptions;

        public OperationTypeRepositoryTests()
        {
            // Создаем в памяти базу данных для тестов
            _dbContextOptions = new DbContextOptionsBuilder<FinanceContext>().UseInMemoryDatabase(databaseName: "FinanceTestDatabase1").Options;
        }

        [Fact]
        public async Task CreateAsync_ShouldAddEntityToDatabase()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            {
                // Добавляем тестовые данные
                OperationType entity = TestsData.GetTestOperationsType().First();
                OperationTypeRepository repository = new OperationTypeRepository(context);

                // Act
                await repository.CreateAsync(entity);

                // Assert
                Assert.Contains(entity, context.OperationsType);
                var addedEntity = await context.OperationsType.FindAsync(entity.Id);
                Assert.NotNull(addedEntity);
            }
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveEntityFromDatabase()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            {
                // Добавляем тестовые данные
                OperationType entity = TestsData.GetTestOperationsType().Last();
                context.OperationsType.Add(entity);
                context.SaveChanges();

                OperationTypeRepository repository = new OperationTypeRepository(context);

                // Act
                await repository.DeleteAsync(entity);

                // Assert
                Assert.DoesNotContain(entity, context.OperationsType);
                var deletedEntity = await context.OperationsType.FindAsync(entity.Id);
                Assert.Null(deletedEntity);
            }
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnAllEntities()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            {
                // Добавляем тестовые данные
                List<OperationType> entitys = TestsData.GetTestOperationsType();
                context.OperationsType.AddRange(entitys);
                await context.SaveChangesAsync();

                OperationTypeRepository repository = new OperationTypeRepository(context);

                // Act
                var result = await repository.GetAllAsync();

                // Assert
                Assert.Equal(result, entitys);
            }
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnEntityWithMatchingId()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            {
                // Добавляем тестовые данные
                OperationType entity = TestsData.GetTestOperationsType().Last();
                context.OperationsType.Add(entity);
                await context.SaveChangesAsync();

                OperationTypeRepository repository = new OperationTypeRepository(context);

                // Act
                var result = await repository.GetByIdAsync(entity.Id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(entity.Id, result.Id);
            }
        }
        
        [Fact]
        public async Task IsExists_Should_ReturnTrue_IfEntityExists()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            {
                // Добавляем тестовые данные
                OperationType entity = TestsData.GetTestOperationsType().First();
                context.OperationsType.Add(entity);
                await context.SaveChangesAsync();

                OperationTypeRepository repository = new OperationTypeRepository(context);

                // Act
                var result = repository.IsExists(entity.Id);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void IsExists_Should_ReturnFalse_IfEntityDoesNotExist()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            {
                OperationTypeRepository repository = new OperationTypeRepository(context);

                // Act
                var result = repository.IsExists(123);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public async Task UpdateAsync_Should_UpdateEntityInDatabase()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            {
                // Добавляем тестовые данные
                OperationType entity = TestsData.GetTestOperationsType().Last();
                context.OperationsType.Add(entity);
                await context.SaveChangesAsync();

                OperationTypeRepository repository = new OperationTypeRepository(context);

                entity.Name = "New Name";

                // Act
                await repository.UpdateAsync(entity);

                // Assert
                var updatedEntity = await context.OperationsType.FindAsync(entity.Id);
                Assert.NotNull(updatedEntity);
                Assert.Equal("New Name", updatedEntity.Name);
            }
        }

        public void Dispose()
        {
            using (var context = new FinanceContext(_dbContextOptions))
            {
                // Удаляем базу данных после выполнения каждого тестового метода
                context.Database.EnsureDeleted();
            }
        }
    }
}