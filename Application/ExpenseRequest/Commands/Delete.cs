using ErrorOr;
using MediatR;

namespace Application.ExpenseRequest.Commands
{
    public sealed record Delete(Guid Id) : IRequest<ErrorOr<Unit>>;
}
