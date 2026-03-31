using Domain.Interfaces;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Entity = Domain.Aggregates;

namespace Application.ExpenseRequest.Commands.Handlers
{
    internal class CreateHandler(IExpenseRequestRepository prmIExpenseRequestRepository, IUnitOfWork prmIUnitOfWork) : IRequestHandler<Create, ErrorOr<Unit>>
    {
        private readonly IExpenseRequestRepository _IExpenseRequestRepository = prmIExpenseRequestRepository ?? throw new ArgumentNullException(nameof(prmIExpenseRequestRepository));
        private readonly IUnitOfWork _IUnitOfWork = prmIUnitOfWork ?? throw new ArgumentNullException(nameof(prmIUnitOfWork));

        public async Task<ErrorOr<Unit>> Handle(Create request, CancellationToken cancellationToken)
        {
            Entity.ExpenseRequest objExpenseRequest = Entity.ExpenseRequest.Create(request.RequestedById, request.CategoryId, request.StatusId, request.CurrencyId,
                Amount.Create(request.Amount), request.ExpenseDate, request.Description);

            await _IExpenseRequestRepository.Add(objExpenseRequest, cancellationToken);

            await _IUnitOfWork.SaveChange(cancellationToken);

            return Unit.Value;
        }
    }
}
