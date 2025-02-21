using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料替代料管理领域服务
    /// </summary>
    public class MaterialSubstituteManager : DomainService
    {
        private readonly IRepository<MaterialSubstitute, Guid> _materialSubstituteRepository;
        private readonly IRepository<Material, Guid> _materialRepository;
        private readonly ILocalEventBus _localEventBus;

        public MaterialSubstituteManager(
            IRepository<MaterialSubstitute, Guid> materialSubstituteRepository,
            IRepository<Material, Guid> materialRepository,
            ILocalEventBus localEventBus)
        {
            _materialSubstituteRepository = materialSubstituteRepository;
            _materialRepository = materialRepository;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建物料替代料
        /// </summary>
        public async Task<MaterialSubstitute> CreateAsync(
            Guid primaryMaterialId,
            Guid substituteMaterialId,
            decimal substituteRatio,
            int priority,
            string substituteRule,
            string technicalRequirements,
            bool requiresApproval,
            DateTime effectiveDate,
            DateTime? expirationDate = null)
        {
            await ValidateMaterialExistenceAsync(primaryMaterialId);
            await ValidateMaterialExistenceAsync(substituteMaterialId);
            await ValidateSubstituteNotExistsAsync(primaryMaterialId, substituteMaterialId);

            var materialSubstitute = new MaterialSubstitute(
                GuidGenerator.Create(),
                primaryMaterialId,
                substituteMaterialId,
                substituteRatio,
                priority,
                substituteRule,
                technicalRequirements,
                requiresApproval,
                effectiveDate,
                expirationDate
            );

            await _materialSubstituteRepository.InsertAsync(materialSubstitute);
            await _localEventBus.PublishAsync(new MaterialSubstituteCreatedEto
            {
                Id = materialSubstitute.Id,
                PrimaryMaterialId = materialSubstitute.PrimaryMaterialId,
                SubstituteMaterialId = materialSubstitute.SubstituteMaterialId
            });

            return materialSubstitute;
        }

        /// <summary>
        /// 更新替代料比例
        /// </summary>
        public async Task UpdateSubstituteRatioAsync(Guid id, decimal ratio)
        {
            var materialSubstitute = await _materialSubstituteRepository.GetAsync(id);
            materialSubstitute.UpdateSubstituteRatio(ratio);
            await _materialSubstituteRepository.UpdateAsync(materialSubstitute);
            await _localEventBus.PublishAsync(new MaterialSubstituteUpdatedEto
            {
                Id = materialSubstitute.Id,
                SubstituteRatio = ratio
            });
        }

        /// <summary>
        /// 更新替代料有效期
        /// </summary>
        public async Task UpdateEffectiveDatesAsync(Guid id, DateTime effectiveDate, DateTime? expirationDate)
        {
            var materialSubstitute = await _materialSubstituteRepository.GetAsync(id);
            materialSubstitute.UpdateEffectiveDates(effectiveDate, expirationDate);
            await _materialSubstituteRepository.UpdateAsync(materialSubstitute);
            await _localEventBus.PublishAsync(new MaterialSubstituteDatesUpdatedEto
            {
                Id = materialSubstitute.Id,
                EffectiveDate = effectiveDate,
                ExpirationDate = expirationDate
            });
        }

        /// <summary>
        /// 启用替代料
        /// </summary>
        public async Task ActivateAsync(Guid id)
        {
            var materialSubstitute = await _materialSubstituteRepository.GetAsync(id);
            materialSubstitute.Activate();
            await _materialSubstituteRepository.UpdateAsync(materialSubstitute);
            await _localEventBus.PublishAsync(new MaterialSubstituteStatusChangedEto
            {
                Id = materialSubstitute.Id,
                IsActive = true
            });
        }

        /// <summary>
        /// 停用替代料
        /// </summary>
        public async Task DeactivateAsync(Guid id)
        {
            var materialSubstitute = await _materialSubstituteRepository.GetAsync(id);
            materialSubstitute.Deactivate();
            await _materialSubstituteRepository.UpdateAsync(materialSubstitute);
            await _localEventBus.PublishAsync(new MaterialSubstituteStatusChangedEto
            {
                Id = materialSubstitute.Id,
                IsActive = false
            });
        }

        private async Task ValidateMaterialExistenceAsync(Guid materialId)
        {
            if (!await _materialRepository.AnyAsync(m => m.Id == materialId))
            {
                throw new MaterialNotFoundException(materialId);
            }
        }

        private async Task ValidateSubstituteNotExistsAsync(Guid primaryMaterialId, Guid substituteMaterialId)
        {
            if (await _materialSubstituteRepository.AnyAsync(ms =>
                ms.PrimaryMaterialId == primaryMaterialId &&
                ms.SubstituteMaterialId == substituteMaterialId))
            {
                throw new MaterialSubstituteAlreadyExistsException(primaryMaterialId, substituteMaterialId);
            }
        }
    }
}