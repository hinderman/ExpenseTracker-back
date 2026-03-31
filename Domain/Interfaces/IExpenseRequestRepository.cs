using Domain.Aggregates;

namespace Domain.Interfaces
{
    public interface IExpenseRequestRepository
    {
        Task<ExpenseRequest?> GetById(Guid prmId, CancellationToken prmCancellationToken = default);
        Task Add(ExpenseRequest prmExpenseRequest, CancellationToken prmCancellationToken = default);
    }
}