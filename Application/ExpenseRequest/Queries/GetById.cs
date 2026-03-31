using Application.ExpenseRequest.Dtos;
using ErrorOr;
using MediatR;

namespace Application.ExpenseRequest.Queries
{
    public sealed record GetById(Guid Id) : IRequest<ErrorOr<DetailDto>>;
}
