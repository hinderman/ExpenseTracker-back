using Application.Common.Models;
using Application.ExpenseRequest.Dtos;
using Application.Interfaces.Queries;
using ErrorOr;
using MediatR;

namespace Application.ExpenseRequest.Queries.Handlers
{
    internal class GetAllHandler(IExpenseRequestQueries prmIExpenseRequestQueries) : IRequestHandler<GetAll, ErrorOr<Pagination<SummaryDto>>>
    {
        private readonly IExpenseRequestQueries _IExpenseRequestQueries = prmIExpenseRequestQueries ?? throw new ArgumentNullException(nameof(prmIExpenseRequestQueries));

        public async Task<ErrorOr<Pagination<SummaryDto>>> Handle(GetAll request, CancellationToken cancellationToken)
        {
            Pagination<SummaryDto> objExpenseRequest = await _IExpenseRequestQueries.GetAll(request.StatusId, request.CategoryId, request.StartDate, request.EndDate, request.PageNumber, request.PageSize, cancellationToken);

            if (objExpenseRequest.Items is null || objExpenseRequest.Items.Count == 0)
            {
                return Error.NotFound(code: "NOT_FOUND", description: "No se encontraron solicitudes de gastos.");
            }

            return objExpenseRequest;
        }
    }
}
