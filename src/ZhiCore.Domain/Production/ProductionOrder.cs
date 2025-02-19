using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;
using ZhiCore.Domain.Inventory;

namespace ZhiCore.Domain.Production;

public class ProductionOrder : AuditableEntity, IAggregateRoot
{
    public string OrderNumber { get; private set; }
    public DateTime PlannedStartDate { get; private set; }
    public DateTime PlannedEndDate { get; private set; }
    public ProductionOrderStatus Status { get; private set; }
    public int Quantity { get; private set; }
    public string ProductCode { get; private set; }
    public string Description { get; private set; }
    private readonly List<ProductionTask> _tasks = new();
    public IReadOnlyCollection<ProductionTask> Tasks => _tasks.AsReadOnly();

    private ProductionOrder() { }

    public static ProductionOrder Create(
        string productCode,
        int quantity,
        DateTime plannedStartDate,
        DateTime plannedEndDate,
        string description = null)
    {
        var order = new ProductionOrder
        {
            OrderNumber = GenerateOrderNumber(),
            ProductCode = productCode,
            Quantity = quantity,
            PlannedStartDate = plannedStartDate,
            PlannedEndDate = plannedEndDate,
            Description = description,
            Status = ProductionOrderStatus.Created
        };

        return order;
    }

    public void Start()
    {
        if (Status != ProductionOrderStatus.Created)
            throw new InvalidOperationException("只有新建状态的生产订单可以开始生产");

        Status = ProductionOrderStatus.InProgress;
    }

    public void Complete()
    {
        if (Status != ProductionOrderStatus.InProgress)
            throw new InvalidOperationException("只有进行中的生产订单可以完成");

        if (_tasks.Exists(t => t.Status != TaskStatus.Completed))
            throw new InvalidOperationException("存在未完成的生产任务");

        Status = ProductionOrderStatus.Completed;
    }

    public void Cancel(string reason)
    {
        if (Status == ProductionOrderStatus.Completed || Status == ProductionOrderStatus.Cancelled)
            throw new InvalidOperationException("已完成或已取消的生产订单不能取消");

        Status = ProductionOrderStatus.Cancelled;
    }

    public void AddTask(ProductionTask task)
    {
        if (Status != ProductionOrderStatus.Created)
            throw new InvalidOperationException("只能在新建状态下添加生产任务");

        _tasks.Add(task);
    }

    private static string GenerateOrderNumber()
    {
        return $"PO{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public enum ProductionOrderStatus
{
    Created = 0,
    InProgress = 1,
    Completed = 2,
    Cancelled = 3
}