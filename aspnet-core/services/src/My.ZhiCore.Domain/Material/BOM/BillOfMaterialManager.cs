using System;
using System.Threading.Tasks;
using My.ZhiCore.Material.BOM.Events;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Material.BOM
{
    /// <summary>
    /// BOM（物料清单）管理器，负责处理BOM相关的业务逻辑和生命周期管理
    /// </summary>
    public class BillOfMaterialManager : DomainService
    {
        private readonly ILocalEventBus _localEventBus;

        public BillOfMaterialManager(ILocalEventBus localEventBus)
        {
            _localEventBus = localEventBus;
        }
        /// <summary>
        /// 创建新的物料清单
        /// </summary>
        /// <param name="code">物料清单编码</param>
        /// <param name="name">物料清单名称</param>
        /// <param name="description">物料清单描述</param>
        /// <param name="version">版本号</param>
        /// <param name="type">BOM类型</param>
        /// <param name="materialId">关联的物料ID</param>
        /// <param name="effectiveDate">生效日期</param>
        /// <param name="expirationDate">失效日期（可选）</param>
        /// <returns>创建的物料清单实体</returns>
        public virtual async Task<BillOfMaterial> CreateAsync(
            string code,
            string name,
            string description,
            string version,
            BomType type,
            Guid materialId,
            DateTime effectiveDate,
            DateTime? expirationDate = null)
        {
            // 创建新的物料清单实例
            var billOfMaterial = new BillOfMaterial(
                GuidGenerator.Create(),
                code,
                name,
                description,
                version,
                type,
                materialId,
                effectiveDate,
                expirationDate
            );

            // 发布BOM创建事件
            await _localEventBus.PublishAsync(new BomCreatedEto
            {
                Id = billOfMaterial.Id,
                Code = code,
                Name = name,
                Version = version,
                Type = type,
                MaterialId = materialId
            });

            return billOfMaterial;
        }

        /// <summary>
        /// 创建物料清单的新版本
        /// </summary>
        /// <param name="existingBom">现有的物料清单</param>
        /// <param name="newVersion">新版本号</param>
        /// <param name="effectiveDate">新版本生效日期</param>
        /// <param name="expirationDate">新版本失效日期（可选）</param>
        /// <returns>创建的新版本物料清单实体</returns>
        public virtual async Task<BillOfMaterial> CreateNewVersionAsync(
            BillOfMaterial existingBom,
            string newVersion,
            DateTime effectiveDate,
            DateTime? expirationDate = null)
        {
            // 创建新版本的物料清单
            var newBom = new BillOfMaterial(
                GuidGenerator.Create(),
                existingBom.Code,
                existingBom.Name,
                existingBom.Description,
                newVersion,
                existingBom.Type,
                existingBom.MaterialId,
                effectiveDate,
                expirationDate
            );

            // 发布BOM版本变更事件
            await _localEventBus.PublishAsync(new BomVersionChangedEto
            {
                Id = newBom.Id,
                Code = existingBom.Code,
                NewVersion = newVersion,
                OldVersion = existingBom.Version,
                ChangeReason = existingBom.ChangeReason,
                ChangeDescription = existingBom.ChangeDescription,
                PreviousVersionId = existingBom.Id
            });

            return newBom;
        }

        /// <summary>
        /// 创建子装配BOM
        /// </summary>
        /// <param name="parentBomItem">父级BOM项</param>
        /// <param name="code">子装配BOM编码</param>
        /// <param name="name">子装配BOM名称</param>
        /// <param name="description">子装配BOM描述</param>
        /// <param name="version">版本号</param>
        /// <param name="effectiveDate">生效日期</param>
        /// <param name="expirationDate">失效日期（可选）</param>
        /// <returns>创建的子装配BOM实体</returns>
        public virtual async Task<BillOfMaterial> CreateSubAssemblyAsync(
            BomItem parentBomItem,
            string code,
            string name,
            string description,
            string version,
            DateTime effectiveDate,
            DateTime? expirationDate = null)
        {
            // 创建子装配BOM
            var subAssemblyBom = await CreateAsync(
                code,
                name,
                description,
                version,
                BomType.SubAssembly, // 子装配类型
                parentBomItem.MaterialId,
                effectiveDate,
                expirationDate
            );

            // 设置父级BOM项为子装配
            parentBomItem.SetAsSubAssembly(subAssemblyBom.Id);

            return subAssemblyBom;
        }
        /// <param name="originalBom">原始物料清单</param>
        /// <param name="newVersion">新版本号</param>
        /// <param name="effectiveDate">新版本生效日期</param>
        /// <param name="expirationDate">新版本失效日期（可选）</param>
        /// <returns>新版本的物料清单实体</returns>
        public virtual async Task<BillOfMaterial> CreateNewVersionAsync(
            BillOfMaterial originalBom,
            string newVersion,
            DateTime effectiveDate,
            DateTime? expirationDate = null)
        {
            // 创建新版本的物料清单
            var newBom = new BillOfMaterial(
                GuidGenerator.Create(),
                originalBom.Code,
                originalBom.Name,
                originalBom.Description,
                newVersion,
                originalBom.Type,
                originalBom.MaterialId,
                effectiveDate,
                expirationDate
            );

            // 发布BOM版本变更事件
            await _localEventBus.PublishAsync(new BomVersionChangedEto
            {
                Id = newBom.Id,
                Code = existingBom.Code,
                NewVersion = newVersion,
                OldVersion = existingBom.Version,
                ChangeReason = existingBom.ChangeReason,
                ChangeDescription = existingBom.ChangeDescription,
                PreviousVersionId = existingBom.Id
            });

            return newBom;
        }

        /// <summary>
        /// 更新物料清单的有效期
        /// </summary>
        /// <param name="billOfMaterial">物料清单实体</param>
        /// <param name="effectiveDate">新的生效日期</param>
        /// <param name="expirationDate">新的失效日期（可选）</param>
        public virtual void UpdateEffectiveDates(
            BillOfMaterial billOfMaterial,
            DateTime effectiveDate,
            DateTime? expirationDate)
        {
            billOfMaterial.UpdateEffectiveDates(effectiveDate, expirationDate);
        }

        /// <summary>
        /// 启用物料清单
        /// </summary>
        /// <param name="billOfMaterial">物料清单实体</param>
        public virtual void Activate(BillOfMaterial billOfMaterial)
        {
            billOfMaterial.Activate();
        }

        /// <summary>
        /// 禁用物料清单
        /// </summary>
        /// <param name="billOfMaterial">物料清单实体</param>
        public virtual void Deactivate(BillOfMaterial billOfMaterial)
        {
            billOfMaterial.Deactivate();
        }
        /// <summary>
        /// 复制物料清单
        /// </summary>
        /// <param name="sourceBomId">源物料清单ID</param>
        /// <param name="newCode">新物料清单编码</param>
        /// <param name="newVersion">新版本号</param>
        /// <param name="effectiveDate">生效日期</param>
        /// <param name="expirationDate">失效日期（可选）</param>
        /// <returns>新创建的物料清单实体</returns>
        public virtual async Task<BillOfMaterial> CopyAsync(
            Guid sourceBomId,
            string newCode,
            string newVersion,
            DateTime effectiveDate,
            DateTime? expirationDate = null)
        {
            // 获取源物料清单（此处假设有相应的仓储方法）
            var sourceBom = await GetByIdAsync(sourceBomId);
            if (sourceBom == null)
                throw new InvalidOperationException("Source BOM not found.");

            // 创建新的物料清单
            var newBom = new BillOfMaterial(
                GuidGenerator.Create(),
                newCode,
                sourceBom.Name,
                sourceBom.Description,
                newVersion,
                sourceBom.Type,
                sourceBom.MaterialId,
                effectiveDate,
                expirationDate
            );

            // 记录变更历史
            newBom.RecordChange(
                "复制创建",
                $"从物料清单 {sourceBom.Code} 复制创建",
                sourceBomId
            );

            // 发布BOM版本变更事件
            await _localEventBus.PublishAsync(new BomVersionChangedEto
            {
                Id = newBom.Id,
                Code = existingBom.Code,
                NewVersion = newVersion,
                OldVersion = existingBom.Version,
                ChangeReason = existingBom.ChangeReason,
                ChangeDescription = existingBom.ChangeDescription,
                PreviousVersionId = existingBom.Id
            });

            return newBom;
        }

        /// <summary>
        /// 比较两个物料清单的差异
        /// </summary>
        /// <param name="bomId1">物料清单1的ID</param>
        /// <param name="bomId2">物料清单2的ID</param>
        /// <returns>差异比较结果</returns>
        public virtual async Task<BomComparisonResult> CompareAsync(Guid bomId1, Guid bomId2)
        {
            // 获取两个物料清单（此处假设有相应的仓储方法）
            var bom1 = await GetByIdAsync(bomId1);
            var bom2 = await GetByIdAsync(bomId2);

            if (bom1 == null || bom2 == null)
                throw new InvalidOperationException("One or both BOMs not found.");

            var result = new BomComparisonResult
            {
                Bom1Id = bomId1,
                Bom2Id = bomId2,
                Bom1Code = bom1.Code,
                Bom2Code = bom2.Code,
                Bom1Version = bom1.Version,
                Bom2Version = bom2.Version,
                HasDifferences = false
            };

            // 比较基本属性
            if (bom1.Name != bom2.Name)
                result.AddDifference("Name", bom1.Name, bom2.Name);

            if (bom1.Description != bom2.Description)
                result.AddDifference("Description", bom1.Description, bom2.Description);

            if (bom1.Type != bom2.Type)
                result.AddDifference("Type", bom1.Type.ToString(), bom2.Type.ToString());

            if (bom1.MaterialId != bom2.MaterialId)
                result.AddDifference("MaterialId", bom1.MaterialId.ToString(), bom2.MaterialId.ToString());

            return result;
        }
    }
}