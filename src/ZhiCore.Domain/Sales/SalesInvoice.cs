using System;
using System.Collections.Generic;
using System.Linq;
using ZhiCore.Domain.Common;
using ZhiCore.Domain.Sales.Events;

namespace ZhiCore.Domain.Sales;

public class SalesInvoice : AuditableEntity
{
    public string InvoiceNumber { get; private set; }
    public string SalesOrderNumber { get; private set; }
    public int CustomerId { get; private set; }
    public DateTime InvoiceDate { get; private set; }
    public decimal InvoiceAmount { get; private set; }
    public decimal PaidAmount { get; private set; }
    public decimal RemainingAmount => InvoiceAmount - PaidAmount;
    public SalesInvoiceStatus Status { get; private set; }
    public string InvoiceType { get; private set; }
    public string TaxNumber { get; private set; }
    public decimal TaxRate { get; private set; }
    public decimal TaxAmount => InvoiceAmount * TaxRate;
    public string Remarks { get; private set; }

    private readonly List<SalesInvoicePayment> _payments = new();
    public IReadOnlyCollection<SalesInvoicePayment> Payments => _payments.AsReadOnly();

    private SalesInvoice() { }

    public static SalesInvoice Create(
        string salesOrderNumber,
        int customerId,
        decimal invoiceAmount,
        string invoiceType,
        string taxNumber,
        decimal taxRate,
        string remarks = null)
    {
        if (string.IsNullOrWhiteSpace(salesOrderNumber))
            throw new ArgumentException("销售订单号不能为空", nameof(salesOrderNumber));

        if (customerId <= 0)
            throw new ArgumentException("客户ID必须大于0", nameof(customerId));

        if (invoiceAmount <= 0)
            throw new ArgumentException("发票金额必须大于0", nameof(invoiceAmount));

        if (string.IsNullOrWhiteSpace(invoiceType))
            throw new ArgumentException("发票类型不能为空", nameof(invoiceType));

        if (string.IsNullOrWhiteSpace(taxNumber))
            throw new ArgumentException("税号不能为空", nameof(taxNumber));

        if (taxRate < 0)
            throw new ArgumentException("税率不能为负数", nameof(taxRate));

        return new SalesInvoice
        {
            InvoiceNumber = GenerateInvoiceNumber(),
            SalesOrderNumber = salesOrderNumber,
            CustomerId = customerId,
            InvoiceDate = DateTime.Now,
            InvoiceAmount = invoiceAmount,
            PaidAmount = 0,
            Status = SalesInvoiceStatus.Created,
            InvoiceType = invoiceType,
            TaxNumber = taxNumber,
            TaxRate = taxRate,
            Remarks = remarks
        };
    }

    public void Issue()
    {
        if (Status != SalesInvoiceStatus.Created)
            throw new InvalidOperationException("只有新建状态的发票可以开具");

        Status = SalesInvoiceStatus.Issued;
        AddDomainEvent(new SalesInvoiceStatusChangedEvent(InvoiceNumber, SalesInvoiceStatus.Created, Status));
    }

    public void AddPayment(decimal amount, string paymentMethod, string paymentReference)
    {
        if (Status != SalesInvoiceStatus.Issued)
            throw new InvalidOperationException("只有已开具的发票可以收款");

        if (amount <= 0)
            throw new ArgumentException("收款金额必须大于0", nameof(amount));

        if (PaidAmount + amount > InvoiceAmount)
            throw new ArgumentException("收款总额不能超过发票金额");

        var payment = new SalesInvoicePayment(amount, paymentMethod, paymentReference);
        _payments.Add(payment);
        PaidAmount += amount;

        if (PaidAmount == InvoiceAmount)
        {
            Status = SalesInvoiceStatus.Paid;
            AddDomainEvent(new SalesInvoiceStatusChangedEvent(InvoiceNumber, SalesInvoiceStatus.Issued, Status));
        }
    }

    public void Void(string reason)
    {
        if (Status == SalesInvoiceStatus.Voided)
            throw new InvalidOperationException("发票已作废");

        if (PaidAmount > 0)
            throw new InvalidOperationException("已收款的发票不能作废");

        var oldStatus = Status;
        Status = SalesInvoiceStatus.Voided;
        Remarks = $"作废原因：{reason}";
        AddDomainEvent(new SalesInvoiceStatusChangedEvent(InvoiceNumber, oldStatus, Status));
    }

    private static string GenerateInvoiceNumber()
    {
        return $"INV{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public enum SalesInvoiceStatus
{
    Created = 0,
    Issued = 1,
    Paid = 2,
    Voided = 3
}

public class SalesInvoicePayment
{
    public decimal Amount { get; private set; }
    public string PaymentMethod { get; private set; }
    public string PaymentReference { get; private set; }
    public DateTime PaymentDate { get; private set; }

    public SalesInvoicePayment(decimal amount, string paymentMethod, string paymentReference)
    {
        Amount = amount;
        PaymentMethod = paymentMethod;
        PaymentReference = paymentReference;
        PaymentDate = DateTime.Now;
    }
}