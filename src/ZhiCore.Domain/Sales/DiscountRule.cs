using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales
{
    public class DiscountRule : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DiscountType Type { get; private set; }
        public decimal DiscountValue { get; private set; }
        public DateTime EffectiveDate { get; private set; }
        public DateTime? ExpiryDate { get; private set; }
        public bool IsActive { get; private set; }
        public decimal? MinimumQuantity { get; private set; }
        public decimal? MinimumAmount { get; private set; }
        public string CustomerGrade { get; private set; }

        private DiscountRule() { }

        public DiscountRule(
            string name,
            string description,
            DiscountType type,
            decimal discountValue,
            DateTime effectiveDate,
            DateTime? expiryDate = null,
            decimal? minimumQuantity = null,
            decimal? minimumAmount = null,
            string customerGrade = null)
        {
            if (discountValue <= 0)
                throw new ArgumentException("Discount value must be greater than zero.", nameof(discountValue));

            if (type == DiscountType.Percentage && discountValue > 100)
                throw new ArgumentException("Percentage discount cannot exceed 100%.", nameof(discountValue));

            Name = name;
            Description = description;
            Type = type;
            DiscountValue = discountValue;
            EffectiveDate = effectiveDate;
            ExpiryDate = expiryDate;
            MinimumQuantity = minimumQuantity;
            MinimumAmount = minimumAmount;
            CustomerGrade = customerGrade;
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
            ExpiryDate = DateTime.Now;
        }

        public decimal CalculateDiscount(decimal originalPrice, decimal quantity = 1)
        {
            if (!IsActive || (ExpiryDate.HasValue && ExpiryDate.Value < DateTime.Now))
                return 0;

            if (MinimumQuantity.HasValue && quantity < MinimumQuantity.Value)
                return 0;

            if (MinimumAmount.HasValue && (originalPrice * quantity) < MinimumAmount.Value)
                return 0;

            return Type == DiscountType.Percentage
                ? originalPrice * (DiscountValue / 100)
                : DiscountValue;
        }
    }

    public enum DiscountType
    {
        Percentage,
        FixedAmount
    }
}