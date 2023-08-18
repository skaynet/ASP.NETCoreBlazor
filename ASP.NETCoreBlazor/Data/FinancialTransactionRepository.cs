using ASP.NETCoreBlazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreBlazor.Data
{
    public class FinancialTransactionRepository : IRepository<FinancialTransaction>
    {
        private readonly FinanceContext _context;

        public FinancialTransactionRepository(FinanceContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(FinancialTransaction entity)
        {
            _context.FinancialTransactions.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(FinancialTransaction entity)
        {
            _context.FinancialTransactions.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FinancialTransaction>> GetAllAsync()
        {
            return await _context.FinancialTransactions.Include(o => o.Type).ToListAsync();
        }

        public async Task<FinancialTransaction?> GetByIdAsync(int id)
        {
            return await _context.FinancialTransactions.Include(o => o.Type).FirstOrDefaultAsync(f => f.Id == id);
        }

        public bool IsExists(int id)
        {
            return (_context.FinancialTransactions?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task UpdateAsync(FinancialTransaction entity)
        {
            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
