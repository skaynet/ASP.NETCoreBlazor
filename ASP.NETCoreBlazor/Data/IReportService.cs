using ASP.NETCoreBlazor.Models;

namespace ASP.NETCoreBlazor.Data
{
    public interface IReportService
    {
        Task<DailyReport> GetDailyReportAsync(DateTime date);
        Task<PeriodReport> GetPeriodReportAsync(DateTime startDate, DateTime endDate);
    }
}