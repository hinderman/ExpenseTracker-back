namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChange(CancellationToken pCancellationToken = default);
    }
}
