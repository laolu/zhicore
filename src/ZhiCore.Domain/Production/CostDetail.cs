using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class CostDetail : AuditableEntity
{
    public CostType CostType { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public string Notes { get; private set; }

    private CostDetail() { }

    public static CostDetail Create(
        CostType costType,
        string description,
        decimal amount,
        string notes = null)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("成本描述不能为空", nameof(description));

        if (amount < 0)
            throw new ArgumentException("成本金额不能为负数", nameof(amount));

        return new CostDetail
        {
            CostType = costType,
            Description = description,
            Amount = amount,
            Notes = notes
        };
    }

    public void UpdateAmount(decimal newAmount)
    {
        if (newAmount < 0)
            throw new ArgumentException("成本金额不能为负数", nameof(newAmount));

        Amount = newAmount;
    }

    public void UpdateDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription))
            throw new ArgumentException("成本描述不能为空", nameof(newDescription));

        Description = newDescription;
    }

    public void UpdateNotes(string newNotes)
    {
        Notes = newNotes;
    }
}