using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Sales;

public class SalesTeam : Entity
{
    public string Name { get; private set; }
    public string Region { get; private set; }
    public string Manager { get; private set; }
    public List<string> Members { get; private set; }
    public decimal QuotaAmount { get; private set; }
    public DateTime QuotaPeriodStart { get; private set; }
    public DateTime QuotaPeriodEnd { get; private set; }
    public List<SalesPerformanceMetric> PerformanceMetrics { get; private set; }

    public SalesTeam(string name, string region, string manager, decimal quotaAmount, 
        DateTime quotaPeriodStart, DateTime quotaPeriodEnd)
    {
        if (quotaPeriodStart >= quotaPeriodEnd)
            throw new ArgumentException("配额周期的开始时间必须早于结束时间");

        Name = name;
        Region = region;
        Manager = manager;
        QuotaAmount = quotaAmount;
        QuotaPeriodStart = quotaPeriodStart;
        QuotaPeriodEnd = quotaPeriodEnd;
        Members = new List<string>();
        PerformanceMetrics = new List<SalesPerformanceMetric>();
    }

    public void AddMember(string memberId)
    {
        if (!Members.Contains(memberId))
        {
            Members.Add(memberId);
        }
    }

    public void RemoveMember(string memberId)
    {
        Members.Remove(memberId);
    }

    public void UpdateQuota(decimal newQuotaAmount, DateTime newStart, DateTime newEnd)
    {
        if (newStart >= newEnd)
            throw new ArgumentException("配额周期的开始时间必须早于结束时间");

        QuotaAmount = newQuotaAmount;
        QuotaPeriodStart = newStart;
        QuotaPeriodEnd = newEnd;
    }

    public void AddPerformanceMetric(string metricType, decimal targetValue, decimal actualValue, 
        DateTime recordDate)
    {
        var metric = new SalesPerformanceMetric(metricType, targetValue, actualValue, recordDate);
        PerformanceMetrics.Add(metric);
    }

    public decimal CalculateQuotaAchievementRate()
    {
        var totalSales = PerformanceMetrics
            .Where(p => p.MetricType == "Sales" && 
                   p.RecordDate >= QuotaPeriodStart && 
                   p.RecordDate <= QuotaPeriodEnd)
            .Sum(p => p.ActualValue);

        return QuotaAmount > 0 ? totalSales / QuotaAmount : 0;
    }
}

public class SalesPerformanceMetric
{
    public string MetricType { get; private set; } // 例如：销售额、客户拜访次数、成交率等
    public decimal TargetValue { get; private set; }
    public decimal ActualValue { get; private set; }
    public DateTime RecordDate { get; private set; }

    public SalesPerformanceMetric(string metricType, decimal targetValue, decimal actualValue, 
        DateTime recordDate)
    {
        MetricType = metricType;
        TargetValue = targetValue;
        ActualValue = actualValue;
        RecordDate = recordDate;
    }

    public decimal CalculateAchievementRate()
    {
        return TargetValue > 0 ? ActualValue / TargetValue : 0;
    }
}