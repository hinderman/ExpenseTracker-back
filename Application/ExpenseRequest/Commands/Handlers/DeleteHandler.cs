using Domain.Interfaces;
using ErrorOr;
using MediatR;
using Entity = Domain.Aggregates;

namespace Application.ExpenseRequest.Commands.Handlers
{
    internal class DeleteHandler(IExpenseRequestRepository prmIExpenseRequestRepository, IUnitOfWork prmIUnitOfWork) : IRequestHandler<Delete, ErrorOr<Unit>>
    {
        private readonly IExpenseRequestRepository _IExpenseRequestRepository = prmIExpenseRequestRepository ?? throw new ArgumentNullException(nameof(prmIExpenseRequestRepository));
        private readonly IUnitOfWork _IUnitOfWork = prmIUnitOfWork ?? throw new ArgumentNullException(nameof(prmIUnitOfWork));

        public async Task<ErrorOr<Unit>> Handle(Delete request, CancellationToken cancellationToken)
        {
            Entity.ExpenseRequest? objExpenseRequest = await _IExpenseRequestRepository.GetById(request.Id, cancellationToken);

            if (objExpenseRequest is null)
            {
                return Error.NotFound(code: "NOT_FOUD", description: "No se encontró la solicitud de gastos que desea eliminar.");
            }

            objExpenseRequest.Delete();

            await _IUnitOfWork.SaveChange(cancellationToken);

            return Unit.Value;
        }
    }
}
