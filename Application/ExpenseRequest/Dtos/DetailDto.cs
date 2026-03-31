namespace Application.ExpenseRequest.Dtos
{
    public sealed record DetailDto(Guid Id, Guid UserId, Guid CategoryId, Guid StatusId, Guid CurrencyId, decimal Amount, DateTime ExpenseDate, DateTime CreatedAt, DateTime? UpdatedAt = null, DateTime? DeletedAt = null);
}