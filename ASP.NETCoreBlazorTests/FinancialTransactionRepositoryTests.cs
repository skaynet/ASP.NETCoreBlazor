using ASP.NETCoreBlazor.Data;
using ASP.NETCoreBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreBlazorTests
{
    public class FinancialTransactionRepositoryTests : IDisposable
    {
        private readonly DbContextOptions<FinanceContext> _dbContextOptions;

        public FinancialTransactionRepositoryTests()
        {
            // Создаем в памяти базу данных для тестов
            _dbContextOptions = new DbContextOptionsBuilder<FinanceContext>().UseInMemoryDatabase(databaseName: "FinanceTestDatabase2").Options;
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnAllEntities_WithRelatedTypes()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            {
                // Добавляем тестовые данные
                OperationType type1 = TestsData.GetTestOperationsType().First();
                OperationType type2 = TestsData.GetTestOperationsType().Last();

                FinancialTransaction finance1 = TestsData.GetTestFinancialTransactions().First();
                finance1.Type = type1;
                FinancialTransaction finance2 = TestsData.GetTestFinancialTransactions().Last();
                finance2.Type = type2;

                context.OperationsType.AddRange(type1, type2);
                context.FinancialTransactions.AddRange(finance1, finance2);
                await context.SaveChangesAsync();

                FinancialTransactionRepository repository = new FinancialTransactionRepository(context);

                // Act
                var result = await repository.GetAllAsync();

                // Assert
                Assert.Collection(result,
                    item => Assert.Equal(finance1.Id, item.Id),
                    item => Assert.Equal(finance2.Id, item.Id)
                );

                foreach (var item in result)
                {
                    Assert.NotNull(item.Type);
                }
            }
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnEntityWithMatchingId_WithRelatedType()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            {
                // Добавляем тестовые данные
                OperationType type = TestsData.GetTestOperationsType().First();
                FinancialTransaction finance = TestsData.GetTestFinancialTransactions().First();
                finance.Type = type;

                context.OperationsType.Add(type);
                context.FinancialTransactions.Add(finance);
                await context.SaveChangesAsync();

                FinancialTransactionRepository repository = new FinancialTransactionRepository(context);

                // Act
                var result = await repository.GetByIdAsync(finance.Id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(finance.Id, result.Id);
                Assert.NotNull(result.Type);
            }
        }

        [Fact]
        public void IsExists_Should_ReturnTrue_When_EntityExists()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            {
                // Добавляем тестовые данные
                FinancialTransaction entity = TestsData.GetTestFinancialTransactions().First();
                context.FinancialTransactions.Add(entity);
                context.SaveChanges();

                FinancialTransactionRepository repository = new FinancialTransactionRepository(context);

                // Act
                var result = repository.IsExists(entity.Id);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void IsExists_Should_ReturnFalse_When_EntityDoesNotExist()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            {
                FinancialTransactionRepository repository = new FinancialTransactionRepository(context);

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
                FinancialTransaction entity = TestsData.GetTestFinancialTransactions().Last();
                context.FinancialTransactions.Add(entity);
                await context.SaveChangesAsync();

                FinancialTransactionRepository repository = new FinancialTransactionRepository(context);

                // Modify the entity
                entity.Description = "New Value";

                // Act
                await repository.UpdateAsync(entity);

                // Assert
                var updatedEntity = await context.FinancialTransactions.FindAsync(entity.Id);
                Assert.Equal("New Value", updatedEntity?.Description);
            }
        }

        [Fact]
        public async Task CreateAsync_Should_AddEntityToDatabase()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            { 
                FinancialTransaction entity = TestsData.GetTestFinancialTransactions().Last();

                FinancialTransactionRepository repository = new FinancialTransactionRepository(context);

                // Act
                await repository.CreateAsync(entity);

                // Assert
                var addedEntity = await context.FinancialTransactions.FindAsync(entity.Id);
                Assert.NotNull(addedEntity);
            }
        }

        [Fact]
        public async Task DeleteAsync_Should_RemoveEntityFromDatabase()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            {
                // Добавляем тестовые данные
                FinancialTransaction entity = TestsData.GetTestFinancialTransactions().First();
                context.FinancialTransactions.Add(entity);
                await context.SaveChangesAsync();

                FinancialTransactionRepository repository = new FinancialTransactionRepository(context);

                // Act
                await repository.DeleteAsync(entity);

                // Assert
                var deletedEntity = await context.FinancialTransactions.FindAsync(entity.Id);
                Assert.Null(deletedEntity);
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
