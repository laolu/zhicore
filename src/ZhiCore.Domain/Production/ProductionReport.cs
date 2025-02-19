using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ProductionReport : AuditableEntity
{
    public DateTime ReportDate { get; private set; }
    public ReportType Type { get; private set; }
    public int WorkShiftId { get; private set; }
    public decimal PlannedOutput { get; private set; }
    public decimal ActualOutput { get; private set; }
    public decimal QualifiedOutput { get; private set; }
    public decimal DefectiveOutput { get; private set; }
    public decimal OEE { get; private set; }  // Overall Equipment Effectiveness
    public decimal Availability { get; private set; }
    public decimal Performance { get; private set; }
    public decimal Quality { get; private set; }
    public string Notes { get; private set; }
    
    private readonly List<ProductionKPI> _kpis = new();
    public IReadOnlyCollection<ProductionKPI> KPIs => _kpis.AsReadOnly();

    private ProductionReport() { }

    public static ProductionReport Create(
        DateTime reportDate,
        ReportType type,
        int workShiftId,
        decimal plannedOutput,
        decimal actualOutput,
        decimal qualifiedOutput,
        decimal defectiveOutput,
        decimal availability,
        decimal performance,
        decimal quality,
        string notes = null)
    {
        if (plannedOutput < 0)
            throw new ArgumentException("计划产量不能为负数", nameof(plannedOutput));

        if (actualOutput < 0)
            throw new ArgumentException("实际产量不能为负数", nameof(actualOutput));

        if (qualifiedOutput < 0)
            throw new ArgumentException("合格品数量不能为负数", nameof(qualifiedOutput));

        if (defectiveOutput < 0)
            throw new ArgumentException("不合格品数量不能为负数", nameof(defectiveOutput));

        if (availability < 0 || availability > 1)
            throw new ArgumentException("设备可用率必须在0到1之间", nameof(availability));

        if (performance < 0 || performance > 1)
            throw new ArgumentException("性能效率必须在0到1之间", nameof(performance));

        if (quality < 0 || quality > 1)
            throw new ArgumentException("质量合格率必须在0到1之间", nameof(quality));

        var oee = availability * performance * quality;

        return new ProductionReport
        {
            ReportDate = reportDate,
            Type = type,
            WorkShiftId = workShiftId,
            PlannedOutput = plannedOutput,
            ActualOutput = actualOutput,
            QualifiedOutput = qualifiedOutput,
            DefectiveOutput = defectiveOutput,
            OEE = oee,
            Availability = availability,
            Performance = performance,
            Quality = quality,
            Notes = notes
        };
    }

    public void AddKPI(
        string name,
        decimal target,
        decimal actual,
        string unit,
        string notes = null)
    {
        var kpi = ProductionKPI.Create(
            name,
            target,
            actual,
            unit,
            notes);

        _kpis.Add(kpi);
    }

    public void UpdateKPIActual(string name, decimal newActual)
    {
        var kpi = _kpis.Find(k => k.Name == name);
        if (kpi == null)
            throw new InvalidOperationException($"未找到名为{name}的KPI指标");

        kpi.UpdateActual(newActual);
    }
}

public class ProductionKPI : AuditableEntity
{
    public string Name { get; private set; }
    public decimal Target { get; private set; }
    public decimal Actual { get; private set; }
    public string Unit { get; private set; }
    public decimal Achievement => Target == 0 ? 0 : Actual / Target;
    public string Notes { get; private set; }

    private ProductionKPI() { }

    public static ProductionKPI Create(
        string name,
        decimal target,
        decimal actual,
        string unit,
        string notes = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("KPI名称不能为空", nameof(name));

        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentException("计量单位不能为空", nameof(unit));

        return new ProductionKPI
        {
            Name = name,
            Target = target,
            Actual = actual,
            Unit = unit,
            Notes = notes
        };
    }

    public void UpdateActual(decimal newActual)
    {
        Actual = newActual;
    }
}

public enum ReportType
{
    Daily = 1,    // 日报
    Weekly = 2,   // 周报
    Monthly = 3   // 月报
}