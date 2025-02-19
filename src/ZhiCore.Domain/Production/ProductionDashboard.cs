using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ProductionDashboard : AuditableEntity
{
    public DateTime UpdateTime { get; private set; }
    public int TotalWorkOrders { get; private set; }
    public int InProgressWorkOrders { get; private set; }
    public int DelayedWorkOrders { get; private set; }
    public decimal AverageCompletionRate { get; private set; }
    public decimal AverageQualityRate { get; private set; }
    public int TotalExceptions { get; private set; }
    public int OpenExceptions { get; private set; }
    public decimal OEE { get; private set; }  // Overall Equipment Effectiveness
    
    private readonly List<EquipmentStatus> _equipmentStatuses = new();
    public IReadOnlyCollection<EquipmentStatus> EquipmentStatuses => _equipmentStatuses.AsReadOnly();
    
    private readonly List<ProductionMetric> _metrics = new();
    public IReadOnlyCollection<ProductionMetric> Metrics => _metrics.AsReadOnly();

    private ProductionDashboard() { }

    public static ProductionDashboard Create(
        int totalWorkOrders,
        int inProgressWorkOrders,
        int delayedWorkOrders,
        decimal averageCompletionRate,
        decimal averageQualityRate,
        int totalExceptions,
        int openExceptions,
        decimal oee)
    {
        if (totalWorkOrders < 0)
            throw new ArgumentException("总工单数不能为负数", nameof(totalWorkOrders));

        if (inProgressWorkOrders < 0)
            throw new ArgumentException("进行中工单数不能为负数", nameof(inProgressWorkOrders));

        if (delayedWorkOrders < 0)
            throw new ArgumentException("延迟工单数不能为负数", nameof(delayedWorkOrders));

        if (averageCompletionRate < 0 || averageCompletionRate > 100)
            throw new ArgumentException("平均完成率必须在0到100之间", nameof(averageCompletionRate));

        if (averageQualityRate < 0 || averageQualityRate > 100)
            throw new ArgumentException("平均质量合格率必须在0到100之间", nameof(averageQualityRate));

        if (totalExceptions < 0)
            throw new ArgumentException("总异常数不能为负数", nameof(totalExceptions));

        if (openExceptions < 0)
            throw new ArgumentException("未处理异常数不能为负数", nameof(openExceptions));

        if (oee < 0 || oee > 100)
            throw new ArgumentException("OEE必须在0到100之间", nameof(oee));

        return new ProductionDashboard
        {
            UpdateTime = DateTime.Now,
            TotalWorkOrders = totalWorkOrders,
            InProgressWorkOrders = inProgressWorkOrders,
            DelayedWorkOrders = delayedWorkOrders,
            AverageCompletionRate = averageCompletionRate,
            AverageQualityRate = averageQualityRate,
            TotalExceptions = totalExceptions,
            OpenExceptions = openExceptions,
            OEE = oee
        };
    }

    public void AddEquipmentStatus(
        int equipmentId,
        string equipmentCode,
        EquipmentOperationStatus status,
        decimal utilization,
        string currentOperation = null)
    {
        var equipStatus = EquipmentStatus.Create(
            equipmentId,
            equipmentCode,
            status,
            utilization,
            currentOperation);

        _equipmentStatuses.Add(equipStatus);
    }

    public void AddMetric(
        string name,
        decimal value,
        string unit,
        MetricType type,
        string description = null)
    {
        var metric = ProductionMetric.Create(
            name,
            value,
            unit,
            type,
            description);

        _metrics.Add(metric);
    }

    public void UpdateMetric(string name, decimal newValue)
    {
        var metric = _metrics.Find(m => m.Name == name);
        if (metric != null)
        {
            metric.UpdateValue(newValue);
        }
    }
}

public class EquipmentStatus
{
    public int EquipmentId { get; private set; }
    public string EquipmentCode { get; private set; }
    public EquipmentOperationStatus Status { get; private set; }
    public decimal Utilization { get; private set; }
    public string CurrentOperation { get; private set; }
    public DateTime LastUpdateTime { get; private set; }

    private EquipmentStatus() { }

    public static EquipmentStatus Create(
        int equipmentId,
        string equipmentCode,
        EquipmentOperationStatus status,
        decimal utilization,
        string currentOperation = null)
    {
        if (equipmentId <= 0)
            throw new ArgumentException("设备ID必须大于0", nameof(equipmentId));

        if (string.IsNullOrWhiteSpace(equipmentCode))
            throw new ArgumentException("设备编码不能为空", nameof(equipmentCode));

        if (utilization < 0 || utilization > 100)
            throw new ArgumentException("设备利用率必须在0到100之间", nameof(utilization));

        return new EquipmentStatus
        {
            EquipmentId = equipmentId,
            EquipmentCode = equipmentCode,
            Status = status,
            Utilization = utilization,
            CurrentOperation = currentOperation,
            LastUpdateTime = DateTime.Now
        };
    }
}

public class ProductionMetric
{
    public string Name { get; private set; }
    public decimal Value { get; private set; }
    public string Unit { get; private set; }
    public MetricType Type { get; private set; }
    public string Description { get; private set; }
    public DateTime LastUpdateTime { get; private set; }

    private ProductionMetric() { }

    public static ProductionMetric Create(
        string name,
        decimal value,
        string unit,
        MetricType type,
        string description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("指标名称不能为空", nameof(name));

        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentException("单位不能为空", nameof(unit));

        return new ProductionMetric
        {
            Name = name,
            Value = value,
            Unit = unit,
            Type = type,
            Description = description,
            LastUpdateTime = DateTime.Now
        };
    }

    public void UpdateValue(decimal newValue)
    {
        Value = newValue;
        LastUpdateTime = DateTime.Now;
    }
}

public enum EquipmentOperationStatus
{
    Running = 1,     // 运行中
    Idle = 2,        // 空闲
    Maintenance = 3,  // 维护中
    Breakdown = 4,    // 故障
    Offline = 5      // 离线
}

public enum MetricType
{
    Production = 1,   // 生产相关
    Quality = 2,      // 质量相关
    Efficiency = 3,   // 效率相关
    Resource = 4,     // 资源相关
    Cost = 5         // 成本相关
}