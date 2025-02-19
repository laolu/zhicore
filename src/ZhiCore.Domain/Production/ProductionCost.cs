using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ProductionCost : AuditableEntity
{
    public int WorkOrderId { get; private set; }
    public DateTime RecordDate { get; private set; }
    public decimal MaterialCost { get; private set; }
    public decimal LaborCost { get; private set; }
    public decimal OverheadCost { get; private set; }
    public decimal TotalCost { get; private set; }
    public string Notes { get; private set; }
    
    private readonly List<CostDetail> _costDetails = new();
    public IReadOnlyCollection<CostDetail> CostDetails => _costDetails.AsReadOnly();

    private ProductionCost() { }

    public static ProductionCost Create(
        int workOrderId,
        DateTime recordDate,
        decimal materialCost,
        decimal laborCost,
        decimal overheadCost,
        string notes = null)
    {
        if (workOrderId <= 0)
            throw new ArgumentException("工单ID必须大于0", nameof(workOrderId));

        if (materialCost < 0)
            throw new ArgumentException("材料成本不能为负数", nameof(materialCost));

        if (laborCost < 0)
            throw new ArgumentException("人工成本不能为负数", nameof(laborCost));

        if (overheadCost < 0)
            throw new ArgumentException("制造费用不能为负数", nameof(overheadCost));

        return new ProductionCost
        {
            WorkOrderId = workOrderId,
            RecordDate = recordDate,
            MaterialCost = materialCost,
            LaborCost = laborCost,
            OverheadCost = overheadCost,
            TotalCost = materialCost + laborCost + overheadCost,
            Notes = notes
        };
    }

    public void AddCostDetail(
        CostType costType,
        string description,
        decimal amount,
        string notes = null)
    {
        var detail = CostDetail.Create(
            costType,
            description,
            amount,
            notes);

        _costDetails.Add(detail);
        UpdateTotalCost();
    }

    private void UpdateTotalCost()
    {
        MaterialCost = 0;
        LaborCost = 0;
        OverheadCost = 0;

        foreach (var detail in _costDetails)
        {
            switch (detail.CostType)
            {
                case CostType.Material:
                    MaterialCost += detail.Amount;
                    break;
                case CostType.Labor:
                    LaborCost += detail.Amount;
                    break;
                case CostType.Overhead:
                    OverheadCost += detail.Amount;
                    break;
            }
        }

        TotalCost = MaterialCost + LaborCost + OverheadCost;
    }
}

public enum CostType
{
    Material = 1,    // 材料成本
    Labor = 2,       // 人工成本
    Overhead = 3     // 制造费用
}