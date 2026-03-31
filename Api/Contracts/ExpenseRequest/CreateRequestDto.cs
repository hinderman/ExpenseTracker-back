namespace Api.Contracts.ExpenseRequest
{
    public sealed record CreateRequestDto(Guid RequestedById, Guid CategoryId, Guid StatusId, Guid CurrencyId, decimal Amount, DateTime ExpenseDate, string Description);
}
