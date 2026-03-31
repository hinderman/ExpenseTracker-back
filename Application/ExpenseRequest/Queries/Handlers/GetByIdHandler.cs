using Application.ExpenseRequest.Dtos;
using Application.Interfaces.Queries;
using ErrorOr;
using MediatR;

namespace Application.ExpenseRequest.Queries.Handlers
{
    internal class GetByIdHandler(IExpenseRequestQueries prmIExpenseRequestQueries) : IRequestHandler<GetById, ErrorOr<DetailDto>>
    {
        private readonly IExpenseRequestQueries _IExpenseRequestQueries = prmIExpenseRequestQueries ?? throw new ArgumentNullException(nameof(prmIExpenseRequestQueries));

        public async Task<ErrorOr<DetailDto>> Handle(GetById request, CancellationToken cancellationToken)
        {
            DetailDto? objDetailDto = await _IExpenseRequestQueries.GetById(request.Id, cancellationToken);

            if (objDetailDto is null)
            {
                return Error.NotFound(code: "NOT_FOUD", description: "No se encontró la solicitud de gastos especificada.");
            }

            return objDetailDto;
        }
    }
}
