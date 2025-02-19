using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class WorkOrder : AuditableEntity
{
    public string OrderNumber { get; private set; }
    public int ProductId { get; private set; }
    public int BomId { get; private set; }
    public int RouteId { get; private set; }
    public decimal Quantity { get; private set; }
    public DateTime PlanStartDate { get; private set; }
    public DateTime PlanEndDate { get; private set; }
    public DateTime? ActualStartDate { get; private set; }
    public DateTime? ActualEndDate { get; private set; }
    public WorkOrderStatus Status { get; private set; }
    public string Notes { get; private set; }

    private readonly List<WorkOrderOperation> _operations = new();
    public IReadOnlyCollection<WorkOrderOperation> Operations => _operations.AsReadOnly();

    private WorkOrder() { }

    public static WorkOrder Create(
        string orderNumber,
        int productId,
        int bomId,
        int routeId,
        decimal quantity,
        DateTime planStartDate,
        DateTime planEndDate,
        string notes = null)
    {
        if (string.IsNullOrWhiteSpace(orderNumber))
            throw new ArgumentException("工单编号不能为空", nameof(orderNumber));

        if (productId <= 0)
            throw new ArgumentException("产品ID必须大于0", nameof(productId));

        if (bomId <= 0)
            throw new ArgumentException("BOM ID必须大于0", nameof(bomId));

        if (routeId <= 0)
            throw new ArgumentException("工艺路线ID必须大于0", nameof(routeId));

        if (quantity <= 0)
            throw new ArgumentException("生产数量必须大于0", nameof(quantity));

        if (planEndDate <= planStartDate)
            throw new ArgumentException("计划结束日期必须大于计划开始日期");

        return new WorkOrder
        {
            OrderNumber = orderNumber,
            ProductId = productId,
            BomId = bomId,
            RouteId = routeId,
            Quantity = quantity,
            PlanStartDate = planStartDate,
            PlanEndDate = planEndDate,
            Status = WorkOrderStatus.Created,
            Notes = notes
        };
    }

    public void AddOperation(
        int operationId,
        int sequence,
        decimal planQuantity,
        string workCenter,
        string notes = null)
    {
        var operation = WorkOrderOperation.Create(
            operationId,
            sequence,
            planQuantity,
            workCenter,
            notes);

        _operations.Add(operation);
    }

    public void Start()
    {
        if (Status != WorkOrderStatus.Created)
            throw new InvalidOperationException("只有新建状态的工单可以开始生产");

        Status = WorkOrderStatus.InProgress;
        ActualStartDate = DateTime.Now;
    }

    public void Complete()
    {
        if (Status != WorkOrderStatus.InProgress)
            throw new InvalidOperationException("只有进行中的工单可以完成");

        if (_operations.Any(o => o.Status != OperationStatus.Completed))
            throw new InvalidOperationException("存在未完成的工序");

        Status = WorkOrderStatus.Completed;
        ActualEndDate = DateTime.Now;
    }

    public void Cancel(string reason)
    {
        if (Status == WorkOrderStatus.Completed || Status == WorkOrderStatus.Cancelled)
            throw new InvalidOperationException("已完成或已取消的工单不能取消");

        Status = WorkOrderStatus.Cancelled;
        Notes = $"取消原因：{reason}";
    }
}

public class WorkOrderOperation : Entity
{
    public int OperationId { get; private set; }
    public int Sequence { get; private set; }
    public decimal PlanQuantity { get; private set; }
    public decimal CompletedQuantity { get; private set; }
    public decimal DefectQuantity { get; private set; }
    public string WorkCenter { get; private set; }
    public DateTime? StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public OperationStatus Status { get; private set; }
    public string Notes { get; private set; }

    private WorkOrderOperation() { }

    public static WorkOrderOperation Create(
        int operationId,
        int sequence,
        decimal planQuantity,
        string workCenter,
        string notes = null)
    {
        if (operationId <= 0)
            throw new ArgumentException("工序ID必须大于0", nameof(operationId));

        if (sequence <= 0)
            throw new ArgumentException("工序顺序必须大于0", nameof(sequence));

        if (planQuantity <= 0)
            throw new ArgumentException("计划数量必须大于0", nameof(planQuantity));

        if (string.IsNullOrWhiteSpace(workCenter))
            throw new ArgumentException("工作中心不能为空", nameof(workCenter));

        return new WorkOrderOperation
        {
            OperationId = operationId,
            Sequence = sequence,
            PlanQuantity = planQuantity,
            CompletedQuantity = 0,
            DefectQuantity = 0,
            WorkCenter = workCenter,
            Status = OperationStatus.Pending,
            Notes = notes
        };
    }

    public void Start()
    {
        if (Status != OperationStatus.Pending)
            throw new InvalidOperationException("只有待处理的工序可以开始");

        Status = OperationStatus.InProgress;
        StartTime = DateTime.Now;
    }

    public void Report(decimal completedQty, decimal defectQty)
    {
        if (Status != OperationStatus.InProgress)
            throw new InvalidOperationException("只有进行中的工序可以报工");

        if (completedQty < 0)
            throw new ArgumentException("完成数量不能为负数", nameof(completedQty));

        if (defectQty < 0)
            throw new ArgumentException("不良品数量不能为负数", nameof(defectQty));

        CompletedQuantity += completedQty;
        DefectQuantity += defectQty;

        if (CompletedQuantity >= PlanQuantity)
        {
            Status = OperationStatus.Completed;
            EndTime = DateTime.Now;
        }
    }
}

public enum WorkOrderStatus
{
    Created = 1,     // 新建
    InProgress = 2,   // 进行中
    Completed = 3,    // 已完成
    Cancelled = 4     // 已取消
}

public enum OperationStatus
{
    Pending = 1,      // 待处理
    InProgress = 2,    // 进行中
    Completed = 3      // 已完成
}