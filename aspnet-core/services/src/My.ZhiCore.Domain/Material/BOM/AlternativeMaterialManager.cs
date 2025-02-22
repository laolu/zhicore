using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace My.ZhiCore.Material.BOM
{
    /// <summary>
    /// 替代物料关系管理器，负责处理替代物料关系的业务逻辑
    /// </summary>
    public class AlternativeMaterialManager : DomainService
    {
        private readonly IRepository<AlternativeMaterial, Guid> _alternativeMaterialRepository;
        private readonly IRepository<Material, Guid> _materialRepository;

        public AlternativeMaterialManager(
            IRepository<AlternativeMaterial, Guid> alternativeMaterialRepository,
            IRepository<Material, Guid> materialRepository)
        {
            _alternativeMaterialRepository = alternativeMaterialRepository;
            _materialRepository = materialRepository;
        }

        /// <summary>
        /// 创建替代物料关系
        /// </summary>
        /// <param name="primaryMaterialId">主物料ID</param>
        /// <param name="substituteMaterialId">替代物料ID</param>
        /// <param name="conversionRate">转换比率</param>
        /// <param name="description">替代说明</param>
        /// <param name="priority">优先级</param>
        /// <returns>创建的替代物料关系实体</returns>
        public async Task<AlternativeMaterial> CreateAsync(
            Guid primaryMaterialId,
            Guid substituteMaterialId,
            decimal conversionRate,
            string description,
            int priority)
        {
            // 验证主物料和替代物料是否存在
            await ValidateMaterialExistsAsync(primaryMaterialId);
            await ValidateMaterialExistsAsync(substituteMaterialId);

            // 验证主物料和替代物料不能相同
            if (primaryMaterialId == substituteMaterialId)
            {
                throw new BusinessException(ZhiCoreDomainErrorCodes.MaterialCannotBeItsOwnSubstitute)
                    .WithData("primaryMaterialId", primaryMaterialId);
            }

            // 检查是否已存在相同的替代关系
            var existingRelation = await _alternativeMaterialRepository.FirstOrDefaultAsync(
                x => x.PrimaryMaterialId == primaryMaterialId && 
                     x.SubstituteMaterialId == substituteMaterialId);

            if (existingRelation != null)
            {
                throw new BusinessException(ZhiCoreDomainErrorCodes.AlternativeMaterialRelationAlreadyExists)
                    .WithData("primaryMaterialId", primaryMaterialId)
                    .WithData("substituteMaterialId", substituteMaterialId);
            }

            var alternativeMaterial = new AlternativeMaterial(
                GuidGenerator.Create(),
                primaryMaterialId,
                substituteMaterialId,
                conversionRate,
                description,
                priority);

            await _alternativeMaterialRepository.InsertAsync(alternativeMaterial);

            return alternativeMaterial;
        }

        /// <summary>
        /// 更新替代物料关系的转换比率
        /// </summary>
        /// <param name="id">替代物料关系ID</param>
        /// <param name="newRate">新的转换比率</param>
        public async Task UpdateConversionRateAsync(Guid id, decimal newRate)
        {
            var alternativeMaterial = await GetAndEnsureExistsAsync(id);
            alternativeMaterial.UpdateConversionRate(newRate);
            await _alternativeMaterialRepository.UpdateAsync(alternativeMaterial);
        }

        /// <summary>
        /// 更新替代物料关系的优先级
        /// </summary>
        /// <param name="id">替代物料关系ID</param>
        /// <param name="newPriority">新的优先级</param>
        public async Task UpdatePriorityAsync(Guid id, int newPriority)
        {
            var alternativeMaterial = await GetAndEnsureExistsAsync(id);
            alternativeMaterial.UpdatePriority(newPriority);
            await _alternativeMaterialRepository.UpdateAsync(alternativeMaterial);
        }

        /// <summary>
        /// 更新替代说明
        /// </summary>
        /// <param name="id">替代物料关系ID</param>
        /// <param name="newDescription">新的替代说明</param>
        public async Task UpdateDescriptionAsync(Guid id, string newDescription)
        {
            var alternativeMaterial = await GetAndEnsureExistsAsync(id);
            alternativeMaterial.UpdateDescription(newDescription);
            await _alternativeMaterialRepository.UpdateAsync(alternativeMaterial);
        }

        /// <summary>
        /// 启用替代物料关系
        /// </summary>
        /// <param name="id">替代物料关系ID</param>
        public async Task ActivateAsync(Guid id)
        {
            var alternativeMaterial = await GetAndEnsureExistsAsync(id);
            alternativeMaterial.Activate();
            await _alternativeMaterialRepository.UpdateAsync(alternativeMaterial);
        }

        /// <summary>
        /// 禁用替代物料关系
        /// </summary>
        /// <param name="id">替代物料关系ID</param>
        public async Task DeactivateAsync(Guid id)
        {
            var alternativeMaterial = await GetAndEnsureExistsAsync(id);
            alternativeMaterial.Deactivate();
            await _alternativeMaterialRepository.UpdateAsync(alternativeMaterial);
        }

        private async Task<AlternativeMaterial> GetAndEnsureExistsAsync(Guid id)
        {
            var alternativeMaterial = await _alternativeMaterialRepository.FindAsync(id);
            if (alternativeMaterial == null)
            {
                throw new BusinessException(ZhiCoreDomainErrorCodes.AlternativeMaterialNotFound)
                    .WithData("id", id);
            }
            return alternativeMaterial;
        }

        private async Task ValidateMaterialExistsAsync(Guid materialId)
        {
            if (!await _materialRepository.AnyAsync(x => x.Id == materialId))
            {
                throw new BusinessException(ZhiCoreDomainErrorCodes.MaterialNotFound)
                    .WithData("id", materialId);
            }
        }
    }
}