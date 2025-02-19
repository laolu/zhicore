using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales;

public class SalesContract : AuditableEntity
{
    public string ContractNumber { get; private set; }
    public int CustomerId { get; private set; }
    public DateTime SignDate { get; private set; }
    public DateTime EffectiveDate { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public decimal ContractAmount { get; private set; }
    public string Terms { get; private set; }
    public ContractStatus Status { get; private set; }
    public string Remarks { get; private set; }
    private readonly List<int> _relatedOrderIds = new();
    public IReadOnlyCollection<int> RelatedOrderIds => _relatedOrderIds.AsReadOnly();

    private SalesContract() { }

    public static SalesContract Create(
        int customerId,
        DateTime signDate,
        DateTime effectiveDate,
        DateTime expirationDate,
        decimal contractAmount,
        string terms = null,
        string remarks = null)
    {
        if (customerId <= 0)
            throw new ArgumentException("客户ID必须大于0", nameof(customerId));

        if (effectiveDate > expirationDate)
            throw new ArgumentException("生效日期不能晚于到期日期");

        if (contractAmount <= 0)
            throw new ArgumentException("合同金额必须大于0", nameof(contractAmount));

        return new SalesContract
        {
            ContractNumber = GenerateContractNumber(),
            CustomerId = customerId,
            SignDate = signDate,
            EffectiveDate = effectiveDate,
            ExpirationDate = expirationDate,
            ContractAmount = contractAmount,
            Terms = terms,
            Status = ContractStatus.Draft,
            Remarks = remarks
        };
    }

    public void LinkSalesOrder(int salesOrderId)
    {
        if (salesOrderId <= 0)
            throw new ArgumentException("销售订单ID必须大于0", nameof(salesOrderId));

        if (!_relatedOrderIds.Contains(salesOrderId))
            _relatedOrderIds.Add(salesOrderId);
    }

    public void Submit()
    {
        if (Status != ContractStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的合同可以提交");

        Status = ContractStatus.Submitted;
    }

    public void Approve()
    {
        if (Status != ContractStatus.Submitted)
            throw new InvalidOperationException("只有已提交的合同可以审批");

        Status = ContractStatus.Active;
    }

    public void Reject(string reason)
    {
        if (Status != ContractStatus.Submitted)
            throw new InvalidOperationException("只有已提交的合同可以驳回");

        Status = ContractStatus.Rejected;
        Remarks = reason;
    }

    public void Terminate(string reason)
    {
        if (Status != ContractStatus.Active)
            throw new InvalidOperationException("只有生效的合同可以终止");

        Status = ContractStatus.Terminated;
        Remarks = reason;
    }

    private static string GenerateContractNumber()
    {
        return $"SC{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public enum ContractStatus
{
    Draft = 0,
    Submitted = 1,
    Active = 2,
    Rejected = 3,
    Terminated = 4,
    Expired = 5
}