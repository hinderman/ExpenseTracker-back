namespace Application.ExpenseRequest.Dtos
{
    public sealed record SummaryDto(Guid Id, string UserName, string CategoryName, string StatusName, string CurrencySymbol, decimal Amount, DateTime ExpenseDate);
}
