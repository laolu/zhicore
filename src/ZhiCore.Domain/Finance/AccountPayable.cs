using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Finance;

public class AccountPayable : AuditableEntity, IAggregateRoot
{
    public string Number { get; private set; }
    public string SupplierCode { get; private set; }
    public decimal Amount { get; private set; }
    public decimal PaidAmount { get; private set; }
    public DateTime DueDate { get; private set; }
    public string Description { get; private set; }
    public APStatus Status { get; private set; }
    private readonly List<APPayment> _payments = new();
    public IReadOnlyCollection<APPayment> Payments => _payments.AsReadOnly();

    private AccountPayable() { }

    public static AccountPayable Create(
        string supplierCode,
        decimal amount,
        DateTime dueDate,
        string description = null)
    {
        return new AccountPayable
        {
            Number = GenerateNumber(),
            SupplierCode = supplierCode,
            Amount = amount,
            PaidAmount = 0,
            DueDate = dueDate,
            Description = description,
            Status = APStatus.Outstanding
        };
    }

    public void AddPayment(
        decimal amount,
        string paymentMethod,
        string reference = null,
        string description = null)
    {
        if (Status != APStatus.Outstanding)
            throw new InvalidOperationException("只有未结清的应付账款可以付款");

        if (amount <= 0)
            throw new InvalidOperationException("付款金额必须大于零");

        if (PaidAmount + amount > Amount)
            throw new InvalidOperationException("付款金额不能超过应付金额");

        var payment = APPayment.Create(
            amount,
            paymentMethod,
            reference,
            description);

        _payments.Add(payment);
        PaidAmount += amount;

        if (PaidAmount >= Amount)
        {
            Status = APStatus.Settled;
        }
    }

    public void Cancel(string reason)
    {
        if (Status == APStatus.Cancelled)
            throw new InvalidOperationException("应付账款已经取消");

        if (PaidAmount > 0)
            throw new InvalidOperationException("已付款的应付账款不能取消");

        Status = APStatus.Cancelled;
    }

    private static string GenerateNumber()
    {
        return $"AP{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public class APPayment : AuditableEntity
{
    public decimal Amount { get; private set; }
    public string PaymentMethod { get; private set; }
    public string Reference { get; private set; }
    public string Description { get; private set; }

    private APPayment() { }

    public static APPayment Create(
        decimal amount,
        string paymentMethod,
        string reference = null,
        string description = null)
    {
        return new APPayment
        {
            Amount = amount,
            PaymentMethod = paymentMethod,
            Reference = reference,
            Description = description
        };
    }
}

public enum APStatus
{
    Outstanding = 0,
    Settled = 1,
    Cancelled = 2
}