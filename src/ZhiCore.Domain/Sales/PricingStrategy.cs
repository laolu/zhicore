using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales
{
    public class PricingStrategy : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime EffectiveDate { get; private set; }
        public DateTime? ExpiryDate { get; private set; }
        public bool IsActive { get; private set; }
        public decimal BasePrice { get; private set; }
        public decimal MinimumPrice { get; private set; }
        public List<DiscountRule> DiscountRules { get; private set; }

        private PricingStrategy() { }

        public PricingStrategy(
            string name,
            string description,
            DateTime effectiveDate,
            DateTime? expiryDate,
            decimal basePrice,
            decimal minimumPrice)
        {
            Name = name;
            Description = description;
            EffectiveDate = effectiveDate;
            ExpiryDate = expiryDate;
            BasePrice = basePrice;
            MinimumPrice = minimumPrice;
            IsActive = true;
            DiscountRules = new List<DiscountRule>();
        }

        public void AddDiscountRule(DiscountRule rule)
        {
            if (rule == null)
                throw new ArgumentNullException(nameof(rule));

            DiscountRules.Add(rule);
        }

        public void Deactivate()
        {
            IsActive = false;
            ExpiryDate = DateTime.Now;
        }

        public void UpdateBasePrice(decimal newBasePrice)
        {
            if (newBasePrice < MinimumPrice)
                throw new InvalidOperationException("Base price cannot be lower than minimum price.");

            BasePrice = newBasePrice;
        }
    }
}