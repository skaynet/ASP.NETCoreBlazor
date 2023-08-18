using ASP.NETCoreBlazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreBlazor.Data
{
    public class OperationTypeRepository : IRepository<OperationType>
    {
        private readonly FinanceContext _context;

        public OperationTypeRepository(FinanceContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(OperationType entity)
        {
            _context.OperationsType.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(OperationType entity)
        {
            _context.OperationsType.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OperationType>> GetAllAsync()
        {
            return await _context.OperationsType.ToListAsync();
        }

        public async Task<OperationType?> GetByIdAsync(int id)
        {
            return await _context.OperationsType.FindAsync(id);
        }

        public bool IsExists(int id)
        {
            return (_context.OperationsType?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task UpdateAsync(OperationType entity)
        {
            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
