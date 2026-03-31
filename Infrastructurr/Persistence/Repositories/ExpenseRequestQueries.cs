using Application.Common.Models;
using Application.ExpenseRequest.Dtos;
using Application.Interfaces.Queries;
using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class ExpenseRequestQueries(DatabaseContext prmDatabaseContext) : IExpenseRequestQueries
    {
        public async Task<Pagination<SummaryDto>> GetAll(Guid? prmStatusId = null, Guid? prmCategoryId = null, DateTime? prmStartDate = null, DateTime? prmEndDate = null, int prmPageNumber = 1, int prmPageSize = 10, CancellationToken prmCancellationToken = default)
        {
            IQueryable<ExpenseRequest> objQuery = prmDatabaseContext.ExpenseRequest
                .AsNoTracking()
                .AsQueryable()
                .AsSplitQuery();

            if (prmStatusId.HasValue)
            {
                objQuery = objQuery.Where(w => w.StatusId == prmStatusId.Value);
            }

            if (prmCategoryId.HasValue)
            {
                objQuery = objQuery.Where(w => w.CategoryId == prmCategoryId.Value);
            }

            if (prmStartDate.HasValue && prmEndDate.HasValue && prmEndDate > prmStartDate)
            {
                objQuery = objQuery.Where(w => w.ExpenseDate >= prmStartDate.Value && w.ExpenseDate <= prmEndDate.Value);
            }

            int intTotalCount = await objQuery.CountAsync(prmCancellationToken);

            IReadOnlyList<SummaryDto> lstSummaryDto = await objQuery.OrderBy(e => e.ExpenseDate)
                .Select(s => new SummaryDto(s.Id, s.RequestedBy.Name, s.Category.Name, s.Status.Name, s.Currency.Symbol, s.Amount.Value, s.ExpenseDate))
                .Skip((prmPageNumber - 1) * prmPageSize)
                .Take(prmPageSize)
                .ToListAsync(prmCancellationToken);

            return new Pagination<SummaryDto>(lstSummaryDto, intTotalCount, prmPageNumber, prmPageSize);
        }

        public async Task<DetailDto?> GetById(Guid prmId, CancellationToken prmCancellationToken = default)
        {
            return await prmDatabaseContext.ExpenseRequest.AsNoTracking()
                .AsNoTracking()
                .Select(s => new DetailDto(s.Id, s.RequestedById, s.CategoryId, s.StatusId, s.CategoryId, s.Amount.Value, s.ExpenseDate, s.CreatedAt, s.UpdatedAt, s.DeletedAt))
                .FirstOrDefaultAsync(prmCancellationToken);
        }
    }
}
