using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement;

public class PricingAgreement : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; }
    public string SupplierCode { get; private set; }
    public string SupplierName { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public AgreementStatus Status { get; private set; }
    public string Description { get; private set; }
    public SettlementType SettlementType { get; private set; }
    public int PaymentDays { get; private set; }

    private readonly List<PricingItem> _items = new();
    public IReadOnlyCollection<PricingItem> Items => _items.AsReadOnly();

    private PricingAgreement() { }

    public static PricingAgreement Create(
        string code,
        string supplierCode,
        string supplierName,
        DateTime startDate,
        DateTime endDate,
        SettlementType settlementType,
        int paymentDays,
        string description = null)
    {
        if (startDate >= endDate)
            throw new InvalidOperationException("协议开始日期必须早于结束日期");

        if (paymentDays < 0)
            throw new InvalidOperationException("付款天数不能为负数");

        return new PricingAgreement
        {
            Code = code,
            SupplierCode = supplierCode,
            SupplierName = supplierName,
            StartDate = startDate,
            EndDate = endDate,
            SettlementType = settlementType,
            PaymentDays = paymentDays,
            Description = description,
            Status = AgreementStatus.Draft
        };
    }

    public void AddItem(
        string materialCode,
        string materialName,
        decimal price,
        string unit,
        decimal minQuantity = 0,
        string remark = null)
    {
        if (Status != AgreementStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的协议可以添加价格项");

        if (price < 0)
            throw new InvalidOperationException("价格不能为负数");

        if (minQuantity < 0)
            throw new InvalidOperationException("最小订购量不能为负数");

        var item = new PricingItem(
            materialCode,
            materialName,
            price,
            unit,
            minQuantity,
            remark);

        _items.Add(item);
    }

    public void RemoveItem(string materialCode)
    {
        if (Status != AgreementStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的协议可以删除价格项");

        var item = _items.Find(x => x.MaterialCode == materialCode);
        if (item != null)
        {
            _items.Remove(item);
        }
    }

    public void Submit()
    {
        if (Status != AgreementStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的协议可以提交");

        if (!_items.Any())
            throw new InvalidOperationException("协议必须包含至少一个价格项");

        Status = AgreementStatus.Submitted;
    }

    public void Approve()
    {
        if (Status != AgreementStatus.Submitted)
            throw new InvalidOperationException("只有已提交的协议可以审批");

        Status = AgreementStatus.Approved;
    }

    public void Reject(string reason)
    {
        if (Status != AgreementStatus.Submitted)
            throw new InvalidOperationException("只有已提交的协议可以驳回");

        Status = AgreementStatus.Rejected;
    }

    public void Terminate(string reason)
    {
        if (Status != AgreementStatus.Approved)
            throw new InvalidOperationException("只有已审批的协议可以终止");

        Status = AgreementStatus.Terminated;
        EndDate = DateTime.Now;
    }
}

public class PricingItem : Entity
{
    public string MaterialCode { get; private set; }
    public string MaterialName { get; private set; }
    public decimal Price { get; private set; }
    public string Unit { get; private set; }
    public decimal MinQuantity { get; private set; }
    public string Remark { get; private set; }

    private PricingItem() { }

    public PricingItem(
        string materialCode,
        string materialName,
        decimal price,
        string unit,
        decimal minQuantity,
        string remark = null)
    {
        MaterialCode = materialCode;
        MaterialName = materialName;
        Price = price;
        Unit = unit;
        MinQuantity = minQuantity;
        Remark = remark;
    }
}

public enum AgreementStatus
{
    Draft = 1,      // 草稿
    Submitted = 2,  // 已提交
    Approved = 3,   // 已审批
    Rejected = 4,   // 已驳回
    Terminated = 5  // 已终止
}

public enum SettlementType
{
    Monthly = 1,    // 月结
    Weekly = 2,     // 周结
    Daily = 3,      // 日结
    ByOrder = 4     // 单结
}