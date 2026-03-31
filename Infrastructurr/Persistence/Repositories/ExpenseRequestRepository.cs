using Domain.Aggregates;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class ExpenseRequestRepository(DatabaseContext prmDatabaseContext) : IExpenseRequestRepository
    {
        public async Task Add(ExpenseRequest prmExpenseRequest, CancellationToken prmCancellationToken = default) => await prmDatabaseContext.ExpenseRequest.AddAsync(prmExpenseRequest, prmCancellationToken);

        public async Task<ExpenseRequest?> GetById(Guid prmId, CancellationToken prmCancellationToken = default) => await prmDatabaseContext.ExpenseRequest.AsTracking().Where(w => w.Id == prmId).FirstOrDefaultAsync(prmCancellationToken);
    }
}