using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料库存管理领域服务
    /// </summary>
    public class MaterialInventoryManager : DomainService
    {
        private readonly IRepository<MaterialInventory, Guid> _inventoryRepository;
        private readonly IRepository<Material, Guid> _materialRepository;
        private readonly ILocalEventBus _localEventBus;

        public MaterialInventoryManager(
            IRepository<MaterialInventory, Guid> inventoryRepository,
            IRepository<Material, Guid> materialRepository,
            ILocalEventBus localEventBus)
        {
            _inventoryRepository = inventoryRepository;
            _materialRepository = materialRepository;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建物料库存
        /// </summary>
        public async Task<MaterialInventory> CreateAsync(
            Guid materialId,
            decimal maximumQuantity,
            decimal minimumQuantity,
            decimal safetyStock,
            decimal reorderPoint,
            decimal standardOrderQuantity,
            decimal standardCost,
            string location)
        {
            // 验证物料是否存在
            var material = await _materialRepository.GetAsync(materialId);
            if (material == null)
                throw new InvalidOperationException("Material does not exist.");

            // 验证该物料是否已有库存记录
            var existingInventory = await _inventoryRepository.FirstOrDefaultAsync(x => x.MaterialId == materialId);
            if (existingInventory != null)
                throw new InvalidOperationException("Inventory already exists for this material.");

            // 创建新的库存记录
            var inventory = new MaterialInventory(
                GuidGenerator.Create(),
                materialId,
                maximumQuantity,
                minimumQuantity,
                safetyStock,
                reorderPoint,
                standardOrderQuantity,
                standardCost,
                location);

            return await _inventoryRepository.InsertAsync(inventory);
        }

        /// <summary>
        /// 入库操作
        /// </summary>
        public async Task<MaterialInventory> StockInAsync(
            Guid inventoryId,
            decimal quantity,
            decimal cost)
        {
            if (quantity <= 0)
                throw new ArgumentException("Stock in quantity must be positive.", nameof(quantity));

            if (cost < 0)
                throw new ArgumentException("Cost cannot be negative.", nameof(cost));

            var inventory = await _inventoryRepository.GetAsync(inventoryId);
            inventory.UpdateQuantity(quantity, cost);

            // 发布库存预警事件
            if (inventory.Status == InventoryStatus.Overflow)
            {
                await _localEventBus.PublishAsync(new InventoryWarningEvent
                {
                    InventoryId = inventoryId,
                    MaterialId = inventory.MaterialId,
                    WarningType = InventoryWarningType.Overflow,
                    CurrentQuantity = inventory.CurrentQuantity,
                    ThresholdQuantity = inventory.MaximumQuantity
                });
            }

            return await _inventoryRepository.UpdateAsync(inventory);
        }

        /// <summary>
        /// 出库操作
        /// </summary>
        public async Task<MaterialInventory> StockOutAsync(
            Guid inventoryId,
            decimal quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Stock out quantity must be positive.", nameof(quantity));

            var inventory = await _inventoryRepository.GetAsync(inventoryId);
            inventory.UpdateQuantity(-quantity);

            // 发布库存预警事件
            if (inventory.Status == InventoryStatus.BelowSafety)
            {
                await _localEventBus.PublishAsync(new InventoryWarningEvent
                {
                    InventoryId = inventoryId,
                    MaterialId = inventory.MaterialId,
                    WarningType = InventoryWarningType.BelowSafety,
                    CurrentQuantity = inventory.CurrentQuantity,
                    ThresholdQuantity = inventory.SafetyStock
                });
            }
            else if (inventory.Status == InventoryStatus.BelowReorderPoint)
            {
                await _localEventBus.PublishAsync(new InventoryWarningEvent
                {
                    InventoryId = inventoryId,
                    MaterialId = inventory.MaterialId,
                    WarningType = InventoryWarningType.BelowReorderPoint,
                    CurrentQuantity = inventory.CurrentQuantity,
                    ThresholdQuantity = inventory.ReorderPoint
                });
            }

            return await _inventoryRepository.UpdateAsync(inventory);
        }

        /// <summary>
        /// 更新库存控制参数
        /// </summary>
        public async Task<MaterialInventory> UpdateControlParametersAsync(
            Guid inventoryId,
            decimal maximumQuantity,
            decimal minimumQuantity,
            decimal safetyStock,
            decimal reorderPoint,
            decimal standardOrderQuantity)
        {
            var inventory = await _inventoryRepository.GetAsync(inventoryId);
            inventory.UpdateControlParameters(
                maximumQuantity,
                minimumQuantity,
                safetyStock,
                reorderPoint,
                standardOrderQuantity);

            return await _inventoryRepository.UpdateAsync(inventory);
        }

        /// <summary>
        /// 更新标准成本
        /// </summary>
        public async Task<MaterialInventory> UpdateStandardCostAsync(
            Guid inventoryId,
            decimal standardCost)
        {
            var inventory = await _inventoryRepository.GetAsync(inventoryId);
            inventory.UpdateCostInformation(standardCost);

            return await _inventoryRepository.UpdateAsync(inventory);
        }
    }

    /// <summary>
    /// 库存预警事件
    /// </summary>
    public class InventoryWarningEvent
    {
        public Guid InventoryId { get; set; }
        public Guid MaterialId { get; set; }
        public InventoryWarningType WarningType { get; set; }
        public decimal CurrentQuantity { get; set; }
        public decimal ThresholdQuantity { get; set; }
    }

    /// <summary>
    /// 库存预警类型
    /// </summary>
    public enum InventoryWarningType
    {
        /// <summary>低于安全库存</summary>
        BelowSafety = 0,

        /// <summary>低于补货点</summary>
        BelowReorderPoint = 1,

        /// <summary>库存溢出</summary>
        Overflow = 2
    }
}