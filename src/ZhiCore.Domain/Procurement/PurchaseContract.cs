using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement;

public class PurchaseContract : AuditableEntity, IAggregateRoot
{
    public string ContractNumber { get; private set; }
    public string PurchaseOrderNumber { get; private set; }
    public int SupplierId { get; private set; }
    public DateTime SignDate { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string PaymentTerms { get; private set; }
    public string DeliveryTerms { get; private set; }
    public ContractStatus Status { get; private set; }
    public string Remarks { get; private set; }

    private PurchaseContract() { }

    public static PurchaseContract Create(
        string purchaseOrderNumber,
        int supplierId,
        DateTime signDate,
        DateTime startDate,
        DateTime endDate,
        decimal totalAmount,
        string paymentTerms,
        string deliveryTerms,
        string remarks = null)
    {
        if (string.IsNullOrWhiteSpace(purchaseOrderNumber))
            throw new ArgumentException("采购订单编号不能为空", nameof(purchaseOrderNumber));

        if (supplierId <= 0)
            throw new ArgumentException("供应商ID必须大于0", nameof(supplierId));

        if (startDate > endDate)
            throw new ArgumentException("合同开始日期不能晚于结束日期");

        if (totalAmount <= 0)
            throw new ArgumentException("合同金额必须大于0", nameof(totalAmount));

        if (string.IsNullOrWhiteSpace(paymentTerms))
            throw new ArgumentException("付款条款不能为空", nameof(paymentTerms));

        if (string.IsNullOrWhiteSpace(deliveryTerms))
            throw new ArgumentException("交付条款不能为空", nameof(deliveryTerms));

        return new PurchaseContract
        {
            ContractNumber = GenerateContractNumber(),
            PurchaseOrderNumber = purchaseOrderNumber,
            SupplierId = supplierId,
            SignDate = signDate,
            StartDate = startDate,
            EndDate = endDate,
            TotalAmount = totalAmount,
            PaymentTerms = paymentTerms,
            DeliveryTerms = deliveryTerms,
            Status = ContractStatus.Draft,
            Remarks = remarks
        };
    }

    public void Sign()
    {
        if (Status != ContractStatus.Draft)
            throw new InvalidOperationException("只有草稿状态的合同可以签署");

        Status = ContractStatus.Signed;
    }

    public void Terminate(string reason)
    {
        if (Status != ContractStatus.Signed)
            throw new InvalidOperationException("只有已签署的合同可以终止");

        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("终止原因不能为空", nameof(reason));

        Status = ContractStatus.Terminated;
        Remarks = reason;
    }

    public void Complete()
    {
        if (Status != ContractStatus.Signed)
            throw new InvalidOperationException("只有已签署的合同可以完成");

        Status = ContractStatus.Completed;
    }

    private static string GenerateContractNumber()
    {
        return $"PC{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public enum ContractStatus
{
    Draft = 0,
    Signed = 1,
    Terminated = 2,
    Completed = 3
}