using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料库存实体类，用于管理物料的库存信息和库存控制参数
    /// </summary>
    public class MaterialInventory : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>关联的物料ID</summary>
        public Guid MaterialId { get; private set; }

        /// <summary>当前库存数量</summary>
        public decimal CurrentQuantity { get; private set; }

        /// <summary>最大库存量</summary>
        public decimal MaximumQuantity { get; private set; }

        /// <summary>最小库存量</summary>
        public decimal MinimumQuantity { get; private set; }

        /// <summary>安全库存量</summary>
        public decimal SafetyStock { get; private set; }

        /// <summary>补货点</summary>
        public decimal ReorderPoint { get; private set; }

        /// <summary>标准补货量</summary>
        public decimal StandardOrderQuantity { get; private set; }

        /// <summary>标准成本</summary>
        public decimal StandardCost { get; private set; }

        /// <summary>移动平均成本</summary>
        public decimal AverageCost { get; private set; }

        /// <summary>最新采购成本</summary>
        public decimal LastPurchaseCost { get; private set; }

        /// <summary>库存位置</summary>
        public string Location { get; private set; }

        /// <summary>库存状态</summary>
        public InventoryStatus Status { get; private set; }

        protected MaterialInventory() { }

        public MaterialInventory(
            Guid id,
            Guid materialId,
            decimal maximumQuantity,
            decimal minimumQuantity,
            decimal safetyStock,
            decimal reorderPoint,
            decimal standardOrderQuantity,
            decimal standardCost,
            string location) : base(id)
        {
            MaterialId = materialId;
            MaximumQuantity = maximumQuantity;
            MinimumQuantity = minimumQuantity;
            SafetyStock = safetyStock;
            ReorderPoint = reorderPoint;
            StandardOrderQuantity = standardOrderQuantity;
            StandardCost = standardCost;
            Location = location;
            CurrentQuantity = 0;
            AverageCost = standardCost;
            LastPurchaseCost = standardCost;
            Status = InventoryStatus.Normal;
        }

        /// <summary>
        /// 更新库存数量
        /// </summary>
        public void UpdateQuantity(decimal quantity, decimal? cost = null)
        {
            if (quantity < 0 && Math.Abs(quantity) > CurrentQuantity)
                throw new InvalidOperationException("Cannot reduce inventory below zero.");

            var oldQuantity = CurrentQuantity;
            CurrentQuantity += quantity;

            // 更新移动平均成本
            if (cost.HasValue && quantity > 0)
            {
                LastPurchaseCost = cost.Value;
                AverageCost = ((oldQuantity * AverageCost) + (quantity * cost.Value)) / CurrentQuantity;
            }

            UpdateStatus();
            AddLocalEvent(new InventoryChangedEvent(Id, MaterialId, oldQuantity, CurrentQuantity));
        }

        /// <summary>
        /// 更新库存控制参数
        /// </summary>
        public void UpdateControlParameters(
            decimal maximumQuantity,
            decimal minimumQuantity,
            decimal safetyStock,
            decimal reorderPoint,
            decimal standardOrderQuantity)
        {
            if (maximumQuantity < minimumQuantity)
                throw new ArgumentException("Maximum quantity must be greater than minimum quantity.");

            if (minimumQuantity < safetyStock)
                throw new ArgumentException("Minimum quantity must be greater than safety stock.");

            if (reorderPoint < safetyStock)
                throw new ArgumentException("Reorder point must be greater than safety stock.");

            MaximumQuantity = maximumQuantity;
            MinimumQuantity = minimumQuantity;
            SafetyStock = safetyStock;
            ReorderPoint = reorderPoint;
            StandardOrderQuantity = standardOrderQuantity;

            UpdateStatus();
        }

        /// <summary>
        /// 更新成本信息
        /// </summary>
        public void UpdateCostInformation(decimal standardCost)
        {
            if (standardCost < 0)
                throw new ArgumentException("Cost cannot be negative.", nameof(standardCost));

            StandardCost = standardCost;
        }

        /// <summary>
        /// 更新库存状态
        /// </summary>
        private void UpdateStatus()
        {
            if (CurrentQuantity <= SafetyStock)
                Status = InventoryStatus.BelowSafety;
            else if (CurrentQuantity <= ReorderPoint)
                Status = InventoryStatus.BelowReorderPoint;
            else if (CurrentQuantity >= MaximumQuantity)
                Status = InventoryStatus.Overflow;
            else
                Status = InventoryStatus.Normal;
        }
    }

    public class InventoryChangedEvent : IEventData
    {
        public Guid InventoryId { get; }
        public Guid MaterialId { get; }
        public decimal OldQuantity { get; }
        public decimal NewQuantity { get; }

        public InventoryChangedEvent(Guid inventoryId, Guid materialId, decimal oldQuantity, decimal newQuantity)
        {
            InventoryId = inventoryId;
            MaterialId = materialId;
            OldQuantity = oldQuantity;
            NewQuantity = newQuantity;
        }
    }

    public enum InventoryStatus
    {
        /// <summary>正常</summary>
        Normal = 0,

        /// <summary>低于安全库存</summary>
        BelowSafety = 1,

        /// <summary>低于补货点</summary>
        BelowReorderPoint = 2,

        /// <summary>库存溢出</summary>
        Overflow = 3
    }
}