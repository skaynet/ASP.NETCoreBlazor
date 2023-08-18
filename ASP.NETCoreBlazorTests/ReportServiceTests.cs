using ASP.NETCoreBlazor.Data;
using ASP.NETCoreBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreBlazorTests
{
    public class ReportServiceTests : IDisposable
    {
        private readonly DbContextOptions<FinanceContext> _dbContextOptions;

        public ReportServiceTests()
        {
            // Создаем в памяти базу данных для тестов
            _dbContextOptions = new DbContextOptionsBuilder<FinanceContext>().UseInMemoryDatabase(databaseName: "FinanceTestDatabase3").Options;
        }

        [Fact]
        public async Task GetDailyReportAsync_Should_ReturnDailyReport()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            {
                // Добавляем тестовые данные
                DateTime date = new DateTime(2023, 6, 1);

                List<FinancialTransaction> transactions = TestsData.GetTestFinancialTransactions();
                context.OperationsType.AddRange(TestsData.GetTestOperationsType());
                context.FinancialTransactions.AddRange(transactions);
                await context.SaveChangesAsync();

                var reportService = new ReportService(context);

                // Act
                var dailyReport = await reportService.GetDailyReportAsync(date);

                // Assert
                Assert.Equal(date, dailyReport.Date);
                Assert.Equal(8000, dailyReport.TotalIncome);
                Assert.Equal(-500, dailyReport.TotalExpenses);
                Assert.Equal(2, dailyReport.Transactions.Count);
            }
        }

        [Fact]
        public async Task GetPeriodReportAsync_Should_ReturnPeriodReport()
        {
            // Arrange
            using (var context = new FinanceContext(_dbContextOptions))
            {
                // Добавляем тестовые данные
                var startDate = new DateTime(2023, 6, 1);
                var endDate = DateTime.Today;
                List<FinancialTransaction> transactions = TestsData.GetTestFinancialTransactions();
                context.OperationsType.AddRange(TestsData.GetTestOperationsType());
                context.FinancialTransactions.AddRange(transactions);
                await context.SaveChangesAsync();

                var reportService = new ReportService(context);

                // Act
                var periodReport = await reportService.GetPeriodReportAsync(startDate, endDate);

                // Assert
                Assert.Equal(startDate, periodReport.StartDate);
                Assert.Equal(endDate, periodReport.EndDate);
                Assert.Equal(9800, periodReport.TotalIncome);
                Assert.Equal(-5000, periodReport.TotalExpenses);
                Assert.Equal(7, periodReport.Transactions.Count);
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
