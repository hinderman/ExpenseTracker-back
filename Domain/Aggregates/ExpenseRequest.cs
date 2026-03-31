using Domain.Common;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Aggregates
{
    public sealed class ExpenseRequest : AuditableEntity
    {
        public Guid RequestedById { get; private set; }

        public Guid CategoryId { get; private set; }

        public Guid StatusId { get; private set; }

        public Guid CurrencyId { get; private set; }

        public Amount Amount { get; private set; }

        public string? Description { get; private set; }

        public DateTime ExpenseDate { get; private set; }

        public User RequestedBy { get; private set; } = null!;

        public Category Category { get; private set; } = null!;

        public Status Status { get; private set; } = null!;

        public Currency Currency { get; private set; } = null!;

        public static ExpenseRequest Create(Guid prmRequestedById, Guid prmCategoryId, Guid prmStatusId, Guid prmCurrencyId, Amount prmAmount, DateTime prmExpenseDate, string? prmDescription = null)
        {
            return new ExpenseRequest(prmRequestedById, prmCategoryId, prmStatusId, prmCurrencyId, prmAmount, prmExpenseDate, prmDescription);
        }

        public void Update(Guid prmCategoryId, Guid prmStatusId, Guid prmCurrencyId, Amount prmAmount, DateTime prmExpenseDate, string? prmDescription = null)
        {
            CategoryId = prmCategoryId;
            StatusId = prmStatusId;
            CurrencyId = prmCurrencyId;
            Amount = prmAmount;
            Description = prmDescription;
            ExpenseDate = prmExpenseDate;
            SetUpdated();
        }

        public void Delete()
        {
            SetDeleted();
        }

        private ExpenseRequest(Guid prmRequestedById, Guid prmCategoryId, Guid prmStatusId, Guid prmCurrencyId, Amount prmAmount, DateTime prmExpenseDate, string? prmDescription = null)
        {
            RequestedById = prmRequestedById;
            CategoryId = prmCategoryId;
            StatusId = prmStatusId;
            CurrencyId = prmCurrencyId;
            Amount = prmAmount;
            Description = prmDescription;
            ExpenseDate = prmExpenseDate;
        }

        private ExpenseRequest() { }
    }
}