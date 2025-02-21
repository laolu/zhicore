using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using System.Collections.Generic;
using Volo.Abp.EventBus.Local;

namespace MesNet.Production
{
    /// <summary>
    /// 物料层级关系管理器 - 用于管理物料层级关系的生命周期和业务规则
    /// </summary>
    public class MaterialLevelManager : DomainService
    {
        private readonly IRepository<MaterialLevel, Guid> _materialLevelRepository;
        private readonly IRepository<Material, Guid> _materialRepository;
        private readonly ILocalEventBus _localEventBus;

        public MaterialLevelManager(
            IRepository<MaterialLevel, Guid> materialLevelRepository,
            IRepository<Material, Guid> materialRepository,
            ILocalEventBus localEventBus)
        {
            _materialLevelRepository = materialLevelRepository;
            _materialRepository = materialRepository;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建物料层级关系
        /// </summary>
        public async Task<MaterialLevel> CreateMaterialLevelAsync(
            Guid parentMaterialId,
            Guid childMaterialId,
            decimal quantity,
            string unit,
            string version,
            bool isAlternative = false,
            int? alternativePriority = null,
            DateTime? effectiveDate = null,
            DateTime? expiryDate = null)
        {
            // 验证父子物料是否存在
            var parentMaterial = await _materialRepository.GetAsync(parentMaterialId);
            var childMaterial = await _materialRepository.GetAsync(childMaterialId);

            // 获取父物料的路径
            var parentPath = await GetMaterialPathAsync(parentMaterialId);

            // 验证是否存在循环引用
            if (parentPath.Contains(childMaterialId.ToString()))
            {
                throw new BusinessException("MaterialLevelDomainErrorCodes.CircularReferenceNotAllowed");
            }

            // 计算新的层级深度和路径
            var level = parentPath.Split('/').Length;
            var materialPath = $"{parentPath}/{childMaterialId}";

            // 创建物料层级关系
            var materialLevel = new MaterialLevel(
                GuidGenerator.Create(),
                parentMaterialId,
                childMaterialId,
                level,
                materialPath,
                true,
                GuidGenerator.Create(), // 生成新的BOM ID
                quantity,
                unit,
                version,
                isAlternative,
                alternativePriority,
                effectiveDate,
                expiryDate);

            await _materialLevelRepository.InsertAsync(materialLevel);

            // 发布物料层级创建事件
            await _localEventBus.PublishAsync(new MaterialLevelCreatedEto
            {
                MaterialLevelId = materialLevel.Id,
                ParentMaterialId = parentMaterialId,
                ChildMaterialId = childMaterialId,
                Level = level,
                MaterialPath = materialPath
            });

            return materialLevel;
        }

        /// <summary>
        /// 获取物料的完整路径
        /// </summary>
        private async Task<string> GetMaterialPathAsync(Guid materialId)
        {
            var materialLevel = await _materialLevelRepository.FindAsync(x => x.ChildMaterialId == materialId && x.IsDirectRelation);
            if (materialLevel == null)
            {
                return materialId.ToString();
            }

            return materialLevel.MaterialPath;
        }

        /// <summary>
        /// 更新物料层级关系的用量信息
        /// </summary>
        public async Task UpdateQuantityAsync(Guid materialLevelId, decimal quantity, string unit)
        {
            var materialLevel = await _materialLevelRepository.GetAsync(materialLevelId);
            materialLevel.UpdateQuantity(quantity, unit);

            await _materialLevelRepository.UpdateAsync(materialLevel);

            // 发布用量更新事件
            await _localEventBus.PublishAsync(new MaterialLevelQuantityUpdatedEto
            {
                MaterialLevelId = materialLevelId,
                Quantity = quantity,
                Unit = unit
            });
        }

        /// <summary>
        /// 设置替代料
        /// </summary>
        public async Task SetAsAlternativeAsync(Guid materialLevelId, int priority)
        {
            var materialLevel = await _materialLevelRepository.GetAsync(materialLevelId);
            materialLevel.SetAsAlternative(priority);

            await _materialLevelRepository.UpdateAsync(materialLevel);

            // 发布替代料设置事件
            await _localEventBus.PublishAsync(new MaterialLevelAlternativeSetEto
            {
                MaterialLevelId = materialLevelId,
                Priority = priority
            });
        }

        /// <summary>
        /// 取消替代料设置
        /// </summary>
        public async Task UnsetAlternativeAsync(Guid materialLevelId)
        {
            var materialLevel = await _materialLevelRepository.GetAsync(materialLevelId);
            materialLevel.UnsetAlternative();

            await _materialLevelRepository.UpdateAsync(materialLevel);

            // 发布取消替代料事件
            await _localEventBus.PublishAsync(new MaterialLevelAlternativeUnsetEto
            {
                MaterialLevelId = materialLevelId
            });
        }

        /// <summary>
        /// 更新物料层级关系的有效期
        /// </summary>
        public async Task UpdateEffectivePeriodAsync(Guid materialLevelId, DateTime effectiveDate, DateTime? expiryDate)
        {
            var materialLevel = await _materialLevelRepository.GetAsync(materialLevelId);
            materialLevel.UpdateEffectivePeriod(effectiveDate, expiryDate);

            await _materialLevelRepository.UpdateAsync(materialLevel);

            // 发布有效期更新事件
            await _localEventBus.PublishAsync(new MaterialLevelEffectivePeriodUpdatedEto
            {
                MaterialLevelId = materialLevelId,
                EffectiveDate = effectiveDate,
                ExpiryDate = expiryDate
            });
        }
    }
}