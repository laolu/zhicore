using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料实体类，表示系统中的物料信息
    /// </summary>
    public class Material : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>物料名称</summary>
        public string Name { get; private set; }

        /// <summary>物料编码</summary>
        public string Code { get; private set; }

        /// <summary>规格型号</summary>
        public string Specification { get; private set; }

        /// <summary>计量单位</summary>
        public string Unit { get; private set; }

        /// <summary>当前库存数量</summary>
        public decimal StockQuantity { get; private set; }

        /// <summary>安全库存量</summary>
        public decimal SafetyStock { get; private set; }

        /// <summary>物料类型</summary>
        public MaterialType Type { get; private set; }

        /// <summary>物料描述</summary>
        public string Description { get; private set; }

        /// <summary>制造商</summary>
        public string Manufacturer { get; private set; }

        /// <summary>供应商</summary>
        public string Supplier { get; private set; }

        /// <summary>单价</summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// 保护的构造函数，供ORM使用
        /// </summary>
        protected Material() { }

        /// <summary>
        /// 创建新的物料实例
        /// </summary>
        /// <param name="id">物料ID</param>
        /// <param name="name">物料名称</param>
        /// <param name="code">物料编码</param>
        /// <param name="specification">规格型号</param>
        /// <param name="unit">计量单位</param>
        /// <param name="type">物料类型</param>
        /// <param name="safetyStock">安全库存量</param>
        /// <param name="description">物料描述</param>
        /// <param name="manufacturer">制造商</param>
        /// <param name="supplier">供应商</param>
        /// <param name="unitPrice">单价</param>
        public Material(
            Guid id,
            string name,
            string code,
            string specification,
            string unit,
            MaterialType type,
            decimal safetyStock,
            string description,
            string manufacturer,
            string supplier,
            decimal unitPrice) : base(id)
        {
            Name = name;
            Code = code;
            Specification = specification;
            Unit = unit;
            Type = type;
            SafetyStock = safetyStock;
            Description = description;
            Manufacturer = manufacturer;
            Supplier = supplier;
            UnitPrice = unitPrice;
            StockQuantity = 0;

            AddLocalEvent(new MaterialCreatedEvent(Id));
        }

        /// <summary>
        /// 更新库存数量
        /// </summary>
        /// <param name="quantity">变更数量（正数表示入库，负数表示出库）</param>
        /// <exception cref="InvalidOperationException">当出库数量大于当前库存时抛出异常</exception>
        public void UpdateStock(decimal quantity)
        {
            if (quantity < 0 && Math.Abs(quantity) > StockQuantity)
                throw new InvalidOperationException("Cannot reduce stock below zero.");

            var oldQuantity = StockQuantity;
            StockQuantity += quantity;

            AddLocalEvent(new MaterialStockChangedEvent(Id, oldQuantity, StockQuantity));

            if (StockQuantity <= SafetyStock)
            {
                AddLocalEvent(new MaterialLowStockEvent(Id, StockQuantity, SafetyStock));
            }
        }

        /// <summary>
        /// 更新安全库存量
        /// </summary>
        /// <param name="safetyStock">新的安全库存量</param>
        /// <exception cref="ArgumentException">当安全库存量为负数时抛出异常</exception>
        public void UpdateSafetyStock(decimal safetyStock)
        {
            if (safetyStock < 0)
                throw new ArgumentException("Safety stock cannot be negative.", nameof(safetyStock));

            SafetyStock = safetyStock;
        }
    }

    /// <summary>
    /// 物料创建事件
    /// </summary>
    public class MaterialCreatedEvent : IEventData
    {
        /// <summary>物料ID</summary>
        public Guid MaterialId { get; }

        /// <summary>
        /// 创建物料创建事件实例
        /// </summary>
        /// <param name="materialId">物料ID</param>
        public MaterialCreatedEvent(Guid materialId)
        {
            MaterialId = materialId;
        }
    }

    /// <summary>
    /// 物料库存变更事件
    /// </summary>
    public class MaterialStockChangedEvent : IEventData
    {
        /// <summary>物料ID</summary>
        public Guid MaterialId { get; }

        /// <summary>变更前数量</summary>
        public decimal OldQuantity { get; }

        /// <summary>变更后数量</summary>
        public decimal NewQuantity { get; }

        /// <summary>
        /// 创建物料库存变更事件实例
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="oldQuantity">变更前数量</param>
        /// <param name="newQuantity">变更后数量</param>
        public MaterialStockChangedEvent(Guid materialId, decimal oldQuantity, decimal newQuantity)
        {
            MaterialId = materialId;
            OldQuantity = oldQuantity;
            NewQuantity = newQuantity;
        }
    }

    /// <summary>
    /// 物料库存不足事件
    /// </summary>
    public class MaterialLowStockEvent : IEventData
    {
        /// <summary>物料ID</summary>
        public Guid MaterialId { get; }

        /// <summary>当前库存</summary>
        public decimal CurrentStock { get; }

        /// <summary>安全库存</summary>
        public decimal SafetyStock { get; }

        /// <summary>
        /// 创建物料库存不足事件实例
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="currentStock">当前库存</param>
        /// <param name="safetyStock">安全库存</param>
        public MaterialLowStockEvent(Guid materialId, decimal currentStock, decimal safetyStock)
        {
            MaterialId = materialId;
            CurrentStock = currentStock;
            SafetyStock = safetyStock;
        }
    }
}