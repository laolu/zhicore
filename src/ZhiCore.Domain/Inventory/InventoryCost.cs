using System;
using System.Collections.Generic;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Inventory;

public class InventoryCost : AuditableEntity
{
    public int InventoryItemId { get; private set; }
    public CostingMethod Method { get; private set; }
    public decimal TotalCost { get; private set; }
    public decimal AverageCost { get; private set; }
    public DateTime CalculationDate { get; private set; }
    private readonly List<InventoryCostLayer> _costLayers = new();
    public IReadOnlyList<InventoryCostLayer> CostLayers => _costLayers.AsReadOnly();

    private InventoryCost() { }

    public static InventoryCost Create(
        int inventoryItemId,
        CostingMethod method)
    {
        if (inventoryItemId <= 0)
            throw new ArgumentException("库存项ID必须大于0", nameof(inventoryItemId));

        return new InventoryCost
        {
            InventoryItemId = inventoryItemId,
            Method = method,
            TotalCost = 0,
            AverageCost = 0,
            CalculationDate = DateTime.Now
        };
    }

    public void AddCostLayer(
        int quantity,
        decimal unitCost,
        DateTime receiptDate)
    {
        if (quantity <= 0)
            throw new ArgumentException("数量必须大于0", nameof(quantity));

        if (unitCost < 0)
            throw new ArgumentException("单位成本不能为负数", nameof(unitCost));

        var costLayer = new InventoryCostLayer(
            quantity,
            unitCost,
            receiptDate);

        _costLayers.Add(costLayer);
        UpdateCosts();
    }

    public void ConsumeCost(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("消耗数量必须大于0", nameof(quantity));

        if (Method == CostingMethod.FIFO)
        {
            ConsumeFIFO(quantity);
        }
        else
        {
            ConsumeAverageCost(quantity);
        }

        UpdateCosts();
    }

    private void ConsumeFIFO(int quantity)
    {
        var remainingQuantity = quantity;
        var layersToRemove = new List<InventoryCostLayer>();

        foreach (var layer in _costLayers)
        {
            if (remainingQuantity <= 0) break;

            if (layer.RemainingQuantity <= remainingQuantity)
            {
                remainingQuantity -= layer.RemainingQuantity;
                layersToRemove.Add(layer);
            }
            else
            {
                layer.Consume(remainingQuantity);
                remainingQuantity = 0;
            }
        }

        foreach (var layer in layersToRemove)
        {
            _costLayers.Remove(layer);
        }

        if (remainingQuantity > 0)
            throw new InvalidOperationException("库存不足");
    }

    private void ConsumeAverageCost(int quantity)
    {
        var totalQuantity = 0;
        foreach (var layer in _costLayers)
        {
            totalQuantity += layer.RemainingQuantity;
        }

        if (quantity > totalQuantity)
            throw new InvalidOperationException("库存不足");

        var consumeRatio = (decimal)quantity / totalQuantity;
        foreach (var layer in _costLayers)
        {
            var consumeQuantity = (int)(layer.RemainingQuantity * consumeRatio);
            if (consumeQuantity > 0)
            {
                layer.Consume(consumeQuantity);
            }
        }
    }

    private void UpdateCosts()
    {
        TotalCost = 0;
        var totalQuantity = 0;

        foreach (var layer in _costLayers)
        {
            TotalCost += layer.RemainingQuantity * layer.UnitCost;
            totalQuantity += layer.RemainingQuantity;
        }

        AverageCost = totalQuantity > 0 ? TotalCost / totalQuantity : 0;
        CalculationDate = DateTime.Now;
    }
}

public class InventoryCostLayer
{
    public int RemainingQuantity { get; private set; }
    public decimal UnitCost { get; private set; }
    public DateTime ReceiptDate { get; private set; }

    internal InventoryCostLayer(
        int quantity,
        decimal unitCost,
        DateTime receiptDate)
    {
        RemainingQuantity = quantity;
        UnitCost = unitCost;
        ReceiptDate = receiptDate;
    }

    internal void Consume(int quantity)
    {
        if (quantity > RemainingQuantity)
            throw new InvalidOperationException("消耗数量不能大于剩余数量");

        RemainingQuantity -= quantity;
    }
}

public enum CostingMethod
{
    WeightedAverage = 0,  // 加权平均法
    FIFO = 1             // 先进先出法
}