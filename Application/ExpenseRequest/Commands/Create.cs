using ErrorOr;
using MediatR;

namespace Application.ExpenseRequest.Commands
{
    public sealed record Create(Guid RequestedById, Guid CategoryId, Guid StatusId, Guid CurrencyId, decimal Amount, DateTime ExpenseDate, string? Description = null) : IRequest<ErrorOr<Unit>>;
}
