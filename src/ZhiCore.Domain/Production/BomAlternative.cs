using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class BomAlternative : AuditableEntity
{
    public int ComponentId { get; private set; }
    public int AlternativeComponentId { get; private set; }
    public decimal ConversionRate { get; private set; }
    public int Priority { get; private set; }
    public DateTime EffectiveDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string Notes { get; private set; }
    public int BomComponentId { get; private set; }
    public BomComponent BomComponent { get; private set; }

    private BomAlternative() { }

    public static BomAlternative Create(
        int componentId,
        int alternativeComponentId,
        decimal conversionRate,
        int priority,
        DateTime effectiveDate,
        DateTime? expirationDate = null,
        string notes = null)
    {
        if (componentId <= 0)
            throw new ArgumentException("主料ID必须大于0", nameof(componentId));

        if (alternativeComponentId <= 0)
            throw new ArgumentException("替代料ID必须大于0", nameof(alternativeComponentId));

        if (conversionRate <= 0)
            throw new ArgumentException("替代比例必须大于0", nameof(conversionRate));

        if (priority <= 0)
            throw new ArgumentException("优先级必须大于0", nameof(priority));

        return new BomAlternative
        {
            ComponentId = componentId,
            AlternativeComponentId = alternativeComponentId,
            ConversionRate = conversionRate,
            Priority = priority,
            EffectiveDate = effectiveDate,
            ExpirationDate = expirationDate,
            Notes = notes
        };
    }

    public void UpdateConversionRate(decimal newRate)
    {
        if (newRate <= 0)
            throw new ArgumentException("替代比例必须大于0", nameof(newRate));

        ConversionRate = newRate;
    }

    public void UpdatePriority(int newPriority)
    {
        if (newPriority <= 0)
            throw new ArgumentException("优先级必须大于0", nameof(newPriority));

        Priority = newPriority;
    }

    public void UpdateEffectiveDates(DateTime effectiveDate, DateTime? expirationDate)
    {
        if (expirationDate.HasValue && effectiveDate >= expirationDate.Value)
            throw new ArgumentException("生效日期必须早于失效日期");

        EffectiveDate = effectiveDate;
        ExpirationDate = expirationDate;
    }

    public void UpdateNotes(string newNotes)
    {
        Notes = newNotes;
    }
}