using Domain.Interfaces;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Entity = Domain.Aggregates;

namespace Application.ExpenseRequest.Commands.Handlers
{
    internal class UpdateHandler(IExpenseRequestRepository prmIExpenseRequestRepository, IUnitOfWork prmIUnitOfWork) : IRequestHandler<Update, ErrorOr<Unit>>
    {
        private readonly IExpenseRequestRepository _IExpenseRequestRepository = prmIExpenseRequestRepository ?? throw new ArgumentNullException(nameof(prmIExpenseRequestRepository));
        private readonly IUnitOfWork _IUnitOfWork = prmIUnitOfWork ?? throw new ArgumentNullException(nameof(prmIUnitOfWork));

        public async Task<ErrorOr<Unit>> Handle(Update request, CancellationToken cancellationToken)
        {
            Entity.ExpenseRequest? objExpenseRequest = await _IExpenseRequestRepository.GetById(request.Id, cancellationToken);

            if (objExpenseRequest is null)
            {
                return Error.NotFound(code: "NOT_FOUD", description: "No se encontró la solicitud de gastos que desea actualizar.");
            }

            objExpenseRequest.Update(request.CategoryId, request.StatusId, request.CurrencyId, Amount.Create(request.Amount), request.ExpenseDate, request.Description);

            await _IUnitOfWork.SaveChange(cancellationToken);

            return Unit.Value;
        }
    }
}
