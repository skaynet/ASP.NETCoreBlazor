using ASP.NETCoreBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreBlazor.Data
{
    public partial class FinanceContext : DbContext
    {
        public FinanceContext() { }
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options) { }

        public DbSet<OperationType> OperationsType { get; set; } = null!;
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinancialTransaction>().HasOne(f => f.Type).WithMany(t => t.FinancialTransactions).HasForeignKey(f => f.TypeId);

            modelBuilder.Entity<FinancialTransaction>()
            .HasOne(o => o.Type)
            .WithMany()
            .HasForeignKey(o => o.TypeId)
            .OnDelete(DeleteBehavior.Cascade);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
