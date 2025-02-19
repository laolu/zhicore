using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales.Events;

public class SalesQuotationStatusChangedEvent : DomainEvent
{
    public string QuotationNumber { get; }
    public SalesQuotationStatus OldStatus { get; }
    public SalesQuotationStatus NewStatus { get; }
    public DateTime ChangedTime { get; }

    public SalesQuotationStatusChangedEvent(
        string quotationNumber,
        SalesQuotationStatus oldStatus,
        SalesQuotationStatus newStatus)
    {
        QuotationNumber = quotationNumber;
        OldStatus = oldStatus;
        NewStatus = newStatus;
        ChangedTime = DateTime.Now;
    }
}