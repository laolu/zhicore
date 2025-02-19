using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class OutsourcingOrderMaterial : AuditableEntity
{
    public int MaterialId { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalCost => Quantity * UnitPrice;
    public string Notes { get; private set; }
    
    // 发料相关属性
    public decimal IssuedQuantity { get; private set; }
    public DateTime? IssueDate { get; private set; }
    
    // 收料相关属性
    public decimal ReceivedQuantity { get; private set; }
    public decimal DefectiveQuantity { get; private set; }
    public DateTime? ReceiveDate { get; private set; }
    
    private OutsourcingOrderMaterial() { }
    
    public static OutsourcingOrderMaterial Create(
        int materialId,
        decimal quantity,
        decimal unitPrice,
        string notes = null)
    {
        if (materialId <= 0)
            throw new ArgumentException("物料ID必须大于0", nameof(materialId));
            
        if (quantity <= 0)
            throw new ArgumentException("数量必须大于0", nameof(quantity));
            
        if (unitPrice < 0)
            throw new ArgumentException("单价不能为负数", nameof(unitPrice));
            
        return new OutsourcingOrderMaterial
        {
            MaterialId = materialId,
            Quantity = quantity,
            UnitPrice = unitPrice,
            Notes = notes,
            IssuedQuantity = 0,
            ReceivedQuantity = 0,
            DefectiveQuantity = 0
        };
    }
    
    public void RecordIssue(decimal issuedQuantity)
    {
        if (issuedQuantity <= 0)
            throw new ArgumentException("发料数量必须大于0", nameof(issuedQuantity));
            
        if (issuedQuantity > Quantity)
            throw new ArgumentException("发料数量不能超过计划数量");
            
        IssuedQuantity = issuedQuantity;
        IssueDate = DateTime.Now;
    }
    
    public void RecordReceive(
        decimal receivedQuantity,
        decimal defectiveQuantity)
    {
        if (receivedQuantity < 0)
            throw new ArgumentException("收料数量不能为负数", nameof(receivedQuantity));
            
        if (defectiveQuantity < 0)
            throw new ArgumentException("不良品数量不能为负数", nameof(defectiveQuantity));
            
        if (receivedQuantity + defectiveQuantity > IssuedQuantity)
            throw new ArgumentException("收料数量和不良品数量之和不能超过发料数量");
            
        ReceivedQuantity = receivedQuantity;
        DefectiveQuantity = defectiveQuantity;
        ReceiveDate = DateTime.Now;
    }
    
    public void UpdateUnitPrice(decimal newUnitPrice)
    {
        if (newUnitPrice < 0)
            throw new ArgumentException("单价不能为负数", nameof(newUnitPrice));
            
        UnitPrice = newUnitPrice;
    }
}