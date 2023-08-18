using ASP.NETCoreBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreBlazor.Data
{
    public class ReportService : IReportService
    {
        private readonly FinanceContext _context;

        public ReportService(FinanceContext context)
        {
            _context = context;
        }

        public async Task<DailyReport> GetDailyReportAsync(DateTime date)
        {
            List<FinancialTransaction> financialTransactions = await _context.FinancialTransactions.Include(f => f.Type).Where(f => f.Date == date).ToListAsync();
            DailyReport dailyReport = new DailyReport();
            dailyReport.Date = date;

            foreach (FinancialTransaction transaction in financialTransactions)
            {
                if (transaction.Amount > 0)
                {
                    dailyReport.TotalIncome += transaction.Amount;
                }
                else
                {
                    dailyReport.TotalExpenses += transaction.Amount;
                }
                dailyReport.Transactions.Add(transaction);
            }
            return dailyReport;
        }

        public async Task<PeriodReport> GetPeriodReportAsync(DateTime startDate, DateTime endDate)
        {
            List<FinancialTransaction> financialTransactions = await _context.FinancialTransactions.Include(f => f.Type).Where(f => f.Date >= startDate && f.Date <= endDate).ToListAsync();

            financialTransactions.Sort((x, y) => DateTime.Compare(y.Date, x.Date));

            PeriodReport periodReport = new PeriodReport();
            periodReport.StartDate = startDate;
            periodReport.EndDate = endDate;

            foreach (FinancialTransaction transaction in financialTransactions)
            {
                if (transaction.Amount > 0)
                {
                    periodReport.TotalIncome += transaction.Amount;
                }
                else
                {
                    periodReport.TotalExpenses += transaction.Amount;
                }
                periodReport.Transactions.Add(transaction);
            }
            return periodReport;
        }
    }
}
