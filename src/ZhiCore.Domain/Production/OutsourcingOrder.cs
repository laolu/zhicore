using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class OutsourcingOrder : AuditableEntity
{
    public string OrderCode { get; private set; }
    public int SupplierId { get; private set; }
    public int ProcessId { get; private set; }
    
    private readonly List<ProcessResource> _processResources = new();
    public IReadOnlyCollection<ProcessResource> ProcessResources => _processResources.AsReadOnly();
    public DateTime PlannedStartDate { get; private set; }
    public DateTime PlannedEndDate { get; private set; }
    public DateTime? ActualStartDate { get; private set; }
    public DateTime? ActualEndDate { get; private set; }
    public decimal PlannedQuantity { get; private set; }
    public decimal CompletedQuantity { get; private set; }
    public decimal DefectiveQuantity { get; private set; }
    public OrderStatus Status { get; private set; }
    public string Notes { get; private set; }
    
    // 成本相关属性
    public decimal ProcessingFee { get; private set; }
    public decimal MaterialCost { get; private set; }
    public decimal TotalCost => ProcessingFee + MaterialCost;
    
    // 质检相关属性
    public bool QualityInspected { get; private set; }
    public string InspectionResults { get; private set; }
    
    private readonly List<OutsourcingOrderMaterial> _materials = new();
    public IReadOnlyCollection<OutsourcingOrderMaterial> Materials => _materials.AsReadOnly();
    
    private OutsourcingOrder() { }
    
    public static OutsourcingOrder Create(
        string orderCode,
        int supplierId,
        int processId,
        DateTime plannedStartDate,
        DateTime plannedEndDate,
        decimal plannedQuantity,
        string notes = null)
    {
        if (string.IsNullOrWhiteSpace(orderCode))
            throw new ArgumentException("订单编码不能为空", nameof(orderCode));
            
        if (supplierId <= 0)
            throw new ArgumentException("供应商ID必须大于0", nameof(supplierId));
            
        if (processId <= 0)
            throw new ArgumentException("工序ID必须大于0", nameof(processId));
            
        if (plannedStartDate >= plannedEndDate)
            throw new ArgumentException("计划开始日期必须早于计划结束日期");
            
        if (plannedQuantity <= 0)
            throw new ArgumentException("计划数量必须大于0", nameof(plannedQuantity));
            
        return new OutsourcingOrder
        {
            OrderCode = orderCode,
            SupplierId = supplierId,
            ProcessId = processId,
            PlannedStartDate = plannedStartDate,
            PlannedEndDate = plannedEndDate,
            PlannedQuantity = plannedQuantity,
            Notes = notes,
            Status = OrderStatus.Created,
            CompletedQuantity = 0,
            DefectiveQuantity = 0,
            ProcessingFee = 0,
            MaterialCost = 0,
            QualityInspected = false
        };
    }
    
    public void AddMaterial(
        int materialId,
        decimal quantity,
        decimal unitPrice,
        string notes = null)
    {
        var material = OutsourcingOrderMaterial.Create(
            materialId,
            quantity,
            unitPrice,
            notes);
            
        _materials.Add(material);
        RecalculateMaterialCost();
    }
    
    public void AddProcessResource(
        ResourceType type,
        int resourceId,
        decimal requiredQuantity,
        string notes = null)
    {
        var resource = ProcessResource.Create(
            ProcessId,
            type,
            resourceId,
            requiredQuantity,
            notes);
            
        _processResources.Add(resource);
    }
    
    public void UpdateStatus(OrderStatus newStatus)
    {
        if (newStatus == Status)
            return;
            
        ValidateStatusTransition(newStatus);
        Status = newStatus;
        
        switch (newStatus)
        {
            case OrderStatus.InProgress:
                ActualStartDate = DateTime.Now;
                break;
            case OrderStatus.Completed:
                ActualEndDate = DateTime.Now;
                break;
        }
    }
    
    public void UpdateProgress(
        decimal completedQuantity,
        decimal defectiveQuantity)
    {
        if (completedQuantity < 0)
            throw new ArgumentException("完成数量不能为负数", nameof(completedQuantity));
            
        if (defectiveQuantity < 0)
            throw new ArgumentException("不良品数量不能为负数", nameof(defectiveQuantity));
            
        if (completedQuantity + defectiveQuantity > PlannedQuantity)
            throw new ArgumentException("完成数量和不良品数量之和不能超过计划数量");
            
        CompletedQuantity = completedQuantity;
        DefectiveQuantity = defectiveQuantity;
    }
    
    public void UpdateProcessingFee(decimal fee)
    {
        if (fee < 0)
            throw new ArgumentException("加工费用不能为负数", nameof(fee));
            
        ProcessingFee = fee;
    }
    
    public void RecordQualityInspection(
        bool passed,
        string results)
    {
        QualityInspected = true;
        InspectionResults = results;
        
        if (!passed)
        {
            Status = OrderStatus.QualityRejected;
        }
    }
    
    private void RecalculateMaterialCost()
    {
        MaterialCost = 0;
        foreach (var material in _materials)
        {
            MaterialCost += material.TotalCost;
        }
    }
    
    private void ValidateStatusTransition(OrderStatus newStatus)
    {
        switch (Status)
        {
            case OrderStatus.Created when newStatus != OrderStatus.InProgress:
                throw new InvalidOperationException("新建状态只能转换为进行中状态");
                
            case OrderStatus.InProgress when newStatus != OrderStatus.Completed 
                && newStatus != OrderStatus.Cancelled:
                throw new InvalidOperationException("进行中状态只能转换为已完成或已取消状态");
                
            case OrderStatus.Completed when newStatus != OrderStatus.QualityRejected:
                throw new InvalidOperationException("已完成状态只能转换为质检不合格状态");
                
            case OrderStatus.QualityRejected:
            case OrderStatus.Cancelled:
                throw new InvalidOperationException("当前状态不能转换为其他状态");
        }
    }
}

public enum OrderStatus
{
    Created = 1,          // 新建
    InProgress = 2,       // 进行中
    Completed = 3,        // 已完成
    QualityRejected = 4,  // 质检不合格
    Cancelled = 5         // 已取消
}