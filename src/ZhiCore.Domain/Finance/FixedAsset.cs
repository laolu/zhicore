using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Finance;

public class FixedAsset : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Category { get; private set; }
    public decimal OriginalValue { get; private set; }
    public decimal ResidualValue { get; private set; }
    public int UsefulLife { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    public DateTime? DisposalDate { get; private set; }
    public decimal AccumulatedDepreciation { get; private set; }
    public string Location { get; private set; }
    public string Description { get; private set; }
    public AssetStatus Status { get; private set; }
    private readonly List<DepreciationRecord> _depreciationRecords = new();
    public IReadOnlyCollection<DepreciationRecord> DepreciationRecords => _depreciationRecords.AsReadOnly();

    private FixedAsset() { }

    public static FixedAsset Create(
        string code,
        string name,
        string category,
        decimal originalValue,
        decimal residualValue,
        int usefulLife,
        DateTime purchaseDate,
        string location = null,
        string description = null)
    {
        if (originalValue <= 0)
            throw new InvalidOperationException("原值必须大于零");

        if (residualValue < 0 || residualValue >= originalValue)
            throw new InvalidOperationException("残值必须大于等于零且小于原值");

        if (usefulLife <= 0)
            throw new InvalidOperationException("使用年限必须大于零");

        return new FixedAsset
        {
            Code = code,
            Name = name,
            Category = category,
            OriginalValue = originalValue,
            ResidualValue = residualValue,
            UsefulLife = usefulLife,
            PurchaseDate = purchaseDate,
            Location = location,
            Description = description,
            Status = AssetStatus.Active,
            AccumulatedDepreciation = 0
        };
    }

    public void RecordDepreciation(
        DateTime depreciationDate,
        decimal amount,
        string description = null)
    {
        if (Status != AssetStatus.Active)
            throw new InvalidOperationException("只有在用的资产可以计提折旧");

        if (amount <= 0)
            throw new InvalidOperationException("折旧金额必须大于零");

        if (AccumulatedDepreciation + amount > OriginalValue - ResidualValue)
            throw new InvalidOperationException("累计折旧不能超过可折旧金额");

        var record = DepreciationRecord.Create(
            depreciationDate,
            amount,
            description);

        _depreciationRecords.Add(record);
        AccumulatedDepreciation += amount;
    }

    public void Dispose(DateTime disposalDate, string reason)
    {
        if (Status != AssetStatus.Active)
            throw new InvalidOperationException("只有在用的资产可以处置");

        if (disposalDate <= PurchaseDate)
            throw new InvalidOperationException("处置日期必须晚于购置日期");

        DisposalDate = disposalDate;
        Status = AssetStatus.Disposed;
    }
}

public class DepreciationRecord : AuditableEntity
{
    public DateTime DepreciationDate { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; }

    private DepreciationRecord() { }

    public static DepreciationRecord Create(
        DateTime depreciationDate,
        decimal amount,
        string description = null)
    {
        return new DepreciationRecord
        {
            DepreciationDate = depreciationDate,
            Amount = amount,
            Description = description
        };
    }
}

public enum AssetStatus
{
    Active = 0,
    Disposed = 1
}