using Application.Common.Models;
using Application.ExpenseRequest.Dtos;
using ErrorOr;
using MediatR;

namespace Application.ExpenseRequest.Queries
{
    public sealed record GetAll(Guid? StatusId = null, Guid? CategoryId = null, DateTime? StartDate = null, DateTime? EndDate = null, int PageNumber = 1, int PageSize = 10) : IRequest<ErrorOr<Pagination<SummaryDto>>>;
}
