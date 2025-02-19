using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Finance;

public class AccountReceivable : AuditableEntity, IAggregateRoot
{
    public string Number { get; private set; }
    public string CustomerCode { get; private set; }
    public decimal Amount { get; private set; }
    public decimal PaidAmount { get; private set; }
    public DateTime DueDate { get; private set; }
    public string Description { get; private set; }
    public ARStatus Status { get; private set; }
    private readonly List<ARPayment> _payments = new();
    public IReadOnlyCollection<ARPayment> Payments => _payments.AsReadOnly();

    private AccountReceivable() { }

    public static AccountReceivable Create(
        string customerCode,
        decimal amount,
        DateTime dueDate,
        string description = null)
    {
        return new AccountReceivable
        {
            Number = GenerateNumber(),
            CustomerCode = customerCode,
            Amount = amount,
            PaidAmount = 0,
            DueDate = dueDate,
            Description = description,
            Status = ARStatus.Outstanding
        };
    }

    public void AddPayment(
        decimal amount,
        string paymentMethod,
        string reference = null,
        string description = null)
    {
        if (Status != ARStatus.Outstanding)
            throw new InvalidOperationException("只有未结清的应收账款可以收款");

        if (amount <= 0)
            throw new InvalidOperationException("收款金额必须大于零");

        if (PaidAmount + amount > Amount)
            throw new InvalidOperationException("收款金额不能超过应收金额");

        var payment = ARPayment.Create(
            amount,
            paymentMethod,
            reference,
            description);

        _payments.Add(payment);
        PaidAmount += amount;

        if (PaidAmount >= Amount)
        {
            Status = ARStatus.Settled;
        }
    }

    public void Cancel(string reason)
    {
        if (Status == ARStatus.Cancelled)
            throw new InvalidOperationException("应收账款已经取消");

        if (PaidAmount > 0)
            throw new InvalidOperationException("已收款的应收账款不能取消");

        Status = ARStatus.Cancelled;
    }

    private static string GenerateNumber()
    {
        return $"AR{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public class ARPayment : AuditableEntity
{
    public decimal Amount { get; private set; }
    public string PaymentMethod { get; private set; }
    public string Reference { get; private set; }
    public string Description { get; private set; }

    private ARPayment() { }

    public static ARPayment Create(
        decimal amount,
        string paymentMethod,
        string reference = null,
        string description = null)
    {
        return new ARPayment
        {
            Amount = amount,
            PaymentMethod = paymentMethod,
            Reference = reference,
            Description = description
        };
    }
}

public enum ARStatus
{
    Outstanding = 0,
    Settled = 1,
    Cancelled = 2
}