using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales.Events;

public class SalesInvoiceStatusChangedEvent : DomainEvent
{
    public string InvoiceNumber { get; }
    public SalesInvoiceStatus OldStatus { get; }
    public SalesInvoiceStatus NewStatus { get; }
    public DateTime ChangedTime { get; }

    public SalesInvoiceStatusChangedEvent(
        string invoiceNumber,
        SalesInvoiceStatus oldStatus,
        SalesInvoiceStatus newStatus)
    {
        InvoiceNumber = invoiceNumber;
        OldStatus = oldStatus;
        NewStatus = newStatus;
        ChangedTime = DateTime.Now;
    }
}