using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class OutsourcingProcessMaterial : AuditableEntity
{
    public int MaterialId { get; private set; }
    public decimal Quantity { get; private set; }
    public MaterialType Type { get; private set; }
    public string Notes { get; private set; }
    
    // 物料消耗相关属性
    public decimal ActualConsumption { get; private set; }
    public decimal WastageRate { get; private set; }
    
    // 成本相关属性
    public decimal UnitCost { get; private set; }
    public decimal TotalCost => Quantity * UnitCost;
    
    private OutsourcingProcessMaterial() { }
    
    public static OutsourcingProcessMaterial Create(
        int materialId,
        decimal quantity,
        MaterialType type,
        string notes = null)
    {
        if (materialId <= 0)
            throw new ArgumentException("物料ID必须大于0", nameof(materialId));
            
        if (quantity <= 0)
            throw new ArgumentException("数量必须大于0", nameof(quantity));
            
        return new OutsourcingProcessMaterial
        {
            MaterialId = materialId,
            Quantity = quantity,
            Type = type,
            Notes = notes,
            ActualConsumption = 0,
            WastageRate = 0,
            UnitCost = 0
        };
    }
    
    public void UpdateQuantity(decimal newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("数量必须大于0", nameof(newQuantity));
            
        Quantity = newQuantity;
    }
    
    public void UpdateActualConsumption(decimal consumption, decimal wastageRate)
    {
        if (consumption < 0)
            throw new ArgumentException("实际消耗不能为负数", nameof(consumption));
            
        if (wastageRate < 0 || wastageRate > 1)
            throw new ArgumentException("损耗率必须在0到1之间", nameof(wastageRate));
            
        ActualConsumption = consumption;
        WastageRate = wastageRate;
    }
    
    public void UpdateUnitCost(decimal newUnitCost)
    {
        if (newUnitCost < 0)
            throw new ArgumentException("单位成本不能为负数", nameof(newUnitCost));
            
        UnitCost = newUnitCost;
    }
}