using Application.Common.Models;
using Application.ExpenseRequest.Dtos;

namespace Application.Interfaces.Queries
{
    public interface IExpenseRequestQueries
    {
        Task<Pagination<SummaryDto>> GetAll(Guid? prmStatusId = null, Guid? prmCategoryId = null, DateTime? prmStartDate = null, DateTime? prmEndDate = null, int prmPageNumber = 1, int prmPageSize = 10, CancellationToken prmCancellationToken = default);
        Task<DetailDto?> GetById(Guid prmId, CancellationToken prmCancellationToken = default);
    }
}