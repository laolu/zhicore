using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料管理器 - 负责物料的生命周期管理和业务规则验证
    /// </summary>
    public class MaterialManager : DomainService
    {
        private readonly IRepository<Material, Guid> _materialRepository;
        private readonly IRepository<MaterialInventory, Guid> _inventoryRepository;
        private readonly ILocalEventBus _localEventBus;

        public MaterialManager(
            IRepository<Material, Guid> materialRepository,
            IRepository<MaterialInventory, Guid> inventoryRepository,
            ILocalEventBus localEventBus)
        {
            _materialRepository = materialRepository;
            _inventoryRepository = inventoryRepository;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建新物料
        /// </summary>
        public async Task<Material> CreateAsync(
            string code,
            string name,
            MaterialType type,
            string specification = null,
            string unit = null,
            decimal? safetyStock = null)
        {
            // 验证物料编码唯一性
            var existingMaterial = await _materialRepository.FirstOrDefaultAsync(m => m.Code == code);
            if (existingMaterial != null)
            {
                throw new MaterialCodeAlreadyExistsException(code);
            }

            // 创建物料
            var material = new Material(code, name, type, specification, unit, safetyStock);
            await _materialRepository.InsertAsync(material);

            // 创建物料库存记录
            var inventory = new MaterialInventory(material.Id, 0, unit);
            await _inventoryRepository.InsertAsync(inventory);

            return material;
        }

        /// <summary>
        /// 更新物料库存
        /// </summary>
        public async Task UpdateStockAsync(Guid materialId, decimal quantity)
        {
            var material = await _materialRepository.GetAsync(materialId);
            var inventory = await _inventoryRepository.FirstOrDefaultAsync(i => i.MaterialId == materialId);

            if (inventory == null)
            {
                throw new MaterialInventoryNotFoundException(materialId);
            }

            // 更新库存
            inventory.UpdateQuantity(quantity);
            await _inventoryRepository.UpdateAsync(inventory);

            // 检查是否达到安全库存警戒线
            if (material.SafetyStock.HasValue && quantity <= material.SafetyStock.Value)
            {
                await _localEventBus.PublishAsync(new MaterialLowStockEvent(materialId, quantity, material.SafetyStock.Value));
            }
        }
    }

    public class MaterialCodeAlreadyExistsException : Exception
    {
        public string Code { get; }

        public MaterialCodeAlreadyExistsException(string code)
            : base($"Material with code {code} already exists.")
        {
            Code = code;
        }
    }

    public class MaterialInventoryNotFoundException : Exception
    {
        public Guid MaterialId { get; }

        public MaterialInventoryNotFoundException(Guid materialId)
            : base($"Material inventory not found for material {materialId}")
        {
            MaterialId = materialId;
        }
    }
}