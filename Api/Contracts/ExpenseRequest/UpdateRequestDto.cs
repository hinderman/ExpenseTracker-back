namespace Api.Contracts.ExpenseRequest
{
    public sealed record UpdateRequestDto(Guid CategoryId, Guid StatusId, Guid CurrencyId, decimal Amount, DateTime ExpenseDate, string? Description = null);
}
