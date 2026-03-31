using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    internal sealed class DatabaseContext(DbContextOptions<DatabaseContext> prmOptions) : DbContext(prmOptions)
    {
        public DbSet<ExpenseRequest> ExpenseRequest => Set<ExpenseRequest>();

        protected override void OnModelCreating(ModelBuilder prmModelBuilder)
        {
            prmModelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            base.OnModelCreating(prmModelBuilder);
        }
    }
}
