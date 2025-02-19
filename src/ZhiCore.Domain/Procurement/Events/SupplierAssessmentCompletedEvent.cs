using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement.Events;

public class SupplierAssessmentCompletedEvent : DomainEvent
{
    public string SupplierCode { get; }
    public string SupplierName { get; }
    public decimal TotalScore { get; }
    public AssessmentResult Result { get; }
    public DateTime CompletionTime { get; }

    public SupplierAssessmentCompletedEvent(
        string supplierCode,
        string supplierName,
        decimal totalScore,
        AssessmentResult result)
    {
        SupplierCode = supplierCode;
        SupplierName = supplierName;
        TotalScore = totalScore;
        Result = result;
        CompletionTime = DateTime.Now;
    }
}