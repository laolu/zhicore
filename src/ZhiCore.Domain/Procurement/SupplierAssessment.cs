using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Procurement;

public class SupplierAssessment : AuditableEntity, IAggregateRoot
{
    public string SupplierCode { get; private set; }
    public string SupplierName { get; private set; }
    public DateTime AssessmentDate { get; private set; }
    public AssessmentPeriod Period { get; private set; }
    public decimal TotalScore { get; private set; }
    public AssessmentResult Result { get; private set; }
    public string Remark { get; private set; }

    private readonly List<AssessmentItem> _items = new();
    public IReadOnlyCollection<AssessmentItem> Items => _items.AsReadOnly();

    private SupplierAssessment() { }

    public static SupplierAssessment Create(
        string supplierCode,
        string supplierName,
        AssessmentPeriod period,
        string remark = null)
    {
        return new SupplierAssessment
        {
            SupplierCode = supplierCode,
            SupplierName = supplierName,
            AssessmentDate = DateTime.Now,
            Period = period,
            Remark = remark,
            TotalScore = 0,
            Result = AssessmentResult.Draft
        };
    }

    public void AddItem(
        string indicator,
        decimal weight,
        decimal score,
        string comment = null)
    {
        if (Result != AssessmentResult.Draft)
            throw new InvalidOperationException("只有草稿状态的评估可以添加评分项");

        if (weight <= 0 || weight > 1)
            throw new InvalidOperationException("权重必须在0到1之间");

        if (score < 0 || score > 100)
            throw new InvalidOperationException("评分必须在0到100之间");

        var item = new AssessmentItem(
            indicator,
            weight,
            score,
            comment);

        _items.Add(item);
        CalculateTotalScore();
    }

    private void CalculateTotalScore()
    {
        TotalScore = 0;
        foreach (var item in _items)
        {
            TotalScore += item.Score * item.Weight;
        }
    }

    public void Submit()
    {
        if (Result != AssessmentResult.Draft)
            throw new InvalidOperationException("只有草稿状态的评估可以提交");

        if (!_items.Any())
            throw new InvalidOperationException("评估必须包含至少一个评分项");

        decimal totalWeight = _items.Sum(x => x.Weight);
        if (Math.Abs(totalWeight - 1) > 0.0001m)
            throw new InvalidOperationException("所有评分项的权重之和必须等于1");

        Result = AssessmentResult.Submitted;
    }

    public void Approve()
    {
        if (Result != AssessmentResult.Submitted)
            throw new InvalidOperationException("只有已提交的评估可以审批");

        Result = AssessmentResult.Approved;
    }

    public void Reject(string reason)
    {
        if (Result != AssessmentResult.Submitted)
            throw new InvalidOperationException("只有已提交的评估可以驳回");

        Result = AssessmentResult.Rejected;
    }
}

public class AssessmentItem : Entity
{
    public string Indicator { get; private set; }
    public decimal Weight { get; private set; }
    public decimal Score { get; private set; }
    public string Comment { get; private set; }

    private AssessmentItem() { }

    public AssessmentItem(
        string indicator,
        decimal weight,
        decimal score,
        string comment = null)
    {
        Indicator = indicator;
        Weight = weight;
        Score = score;
        Comment = comment;
    }
}

public enum AssessmentPeriod
{
    Monthly = 1,    // 月度评估
    Quarterly = 2,  // 季度评估
    Yearly = 3      // 年度评估
}

public enum AssessmentResult
{
    Draft = 1,      // 草稿
    Submitted = 2,  // 已提交
    Approved = 3,   // 已审批
    Rejected = 4    // 已驳回
}