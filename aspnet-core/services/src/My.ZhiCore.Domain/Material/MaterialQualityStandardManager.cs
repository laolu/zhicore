using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料质量标准管理器，负责处理质量标准的生命周期管理
    /// </summary>
    public class MaterialQualityStandardManager : DomainService
    {
        private readonly IRepository<MaterialQualityStandard, Guid> _qualityStandardRepository;
        private readonly IRepository<Material, Guid> _materialRepository;
        private readonly ILocalEventBus _localEventBus;

        public MaterialQualityStandardManager(
            IRepository<MaterialQualityStandard, Guid> qualityStandardRepository,
            IRepository<Material, Guid> materialRepository,
            ILocalEventBus localEventBus)
        {
            _qualityStandardRepository = qualityStandardRepository;
            _materialRepository = materialRepository;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建新的质量标准
        /// </summary>
        public async Task<MaterialQualityStandard> CreateAsync(
            Guid materialId,
            string standardCode,
            string standardName,
            string technicalRequirements,
            string inspectionMethod,
            string tolerance,
            string qualityLevel,
            int inspectionCycle,
            bool requiresCertificate,
            string storageRequirements)
        {
            // 验证物料是否存在
            if (!await _materialRepository.AnyAsync(x => x.Id == materialId))
            {
                throw new ArgumentException("指定的物料不存在", nameof(materialId));
            }

            // 验证标准编号是否唯一
            if (await _qualityStandardRepository.AnyAsync(x => x.StandardCode == standardCode))
            {
                throw new ArgumentException("质量标准编号已存在", nameof(standardCode));
            }

            var qualityStandard = new MaterialQualityStandard(
                GuidGenerator.Create(),
                materialId,
                standardCode,
                standardName,
                technicalRequirements,
                inspectionMethod,
                tolerance,
                qualityLevel,
                inspectionCycle,
                requiresCertificate,
                storageRequirements);

            await _qualityStandardRepository.InsertAsync(qualityStandard);
            await _localEventBus.PublishAsync(new MaterialQualityStandardCreatedEto
            {
                Id = qualityStandard.Id,
                MaterialId = qualityStandard.MaterialId,
                StandardCode = qualityStandard.StandardCode,
                StandardName = qualityStandard.StandardName
            });

            return qualityStandard;
        }

        /// <summary>
        /// 提交质量标准审核
        /// </summary>
        public async Task SubmitForReviewAsync(Guid id)
        {
            var qualityStandard = await _qualityStandardRepository.GetAsync(id);
            qualityStandard.SubmitForReview();

            await _qualityStandardRepository.UpdateAsync(qualityStandard);
            await _localEventBus.PublishAsync(new MaterialQualityStandardStatusChangedEto
            {
                Id = qualityStandard.Id,
                Status = QualityStandardStatus.PendingReview
            });
        }

        /// <summary>
        /// 审核通过质量标准
        /// </summary>
        public async Task ApproveAsync(Guid id, string comments, DateTime effectiveDate, DateTime? expirationDate = null)
        {
            var qualityStandard = await _qualityStandardRepository.GetAsync(id);
            qualityStandard.Approve(comments, effectiveDate, expirationDate);

            await _qualityStandardRepository.UpdateAsync(qualityStandard);
            await _localEventBus.PublishAsync(new MaterialQualityStandardStatusChangedEto
            {
                Id = qualityStandard.Id,
                Status = QualityStandardStatus.Approved
            });
        }

        /// <summary>
        /// 驳回质量标准
        /// </summary>
        public async Task RejectAsync(Guid id, string comments)
        {
            var qualityStandard = await _qualityStandardRepository.GetAsync(id);
            qualityStandard.Reject(comments);

            await _qualityStandardRepository.UpdateAsync(qualityStandard);
            await _localEventBus.PublishAsync(new MaterialQualityStandardStatusChangedEto
            {
                Id = qualityStandard.Id,
                Status = QualityStandardStatus.Rejected
            });
        }

        /// <summary>
        /// 创建新版本的质量标准
        /// </summary>
        public async Task<MaterialQualityStandard> CreateNewVersionAsync(Guid id)
        {
            var qualityStandard = await _qualityStandardRepository.GetAsync(id);
            qualityStandard.CreateNewVersion();

            await _qualityStandardRepository.UpdateAsync(qualityStandard);
            await _localEventBus.PublishAsync(new MaterialQualityStandardVersionChangedEto
            {
                Id = qualityStandard.Id,
                Version = qualityStandard.Version
            });

            return qualityStandard;
        }

        /// <summary>
        /// 启用质量标准
        /// </summary>
        public async Task ActivateAsync(Guid id)
        {
            var qualityStandard = await _qualityStandardRepository.GetAsync(id);
            qualityStandard.Activate();

            await _qualityStandardRepository.UpdateAsync(qualityStandard);
            await _localEventBus.PublishAsync(new MaterialQualityStandardStatusChangedEto
            {
                Id = qualityStandard.Id,
                Status = QualityStandardStatus.Approved,
                IsActive = true
            });
        }

        /// <summary>
        /// 停用质量标准
        /// </summary>
        public async Task DeactivateAsync(Guid id)
        {
            var qualityStandard = await _qualityStandardRepository.GetAsync(id);
            qualityStandard.Deactivate();

            await _qualityStandardRepository.UpdateAsync(qualityStandard);
            await _localEventBus.PublishAsync(new MaterialQualityStandardStatusChangedEto
            {
                Id = qualityStandard.Id,
                Status = QualityStandardStatus.Approved,
                IsActive = false
            });
        }
    }

    /// <summary>
    /// 物料质量标准创建事件
    /// </summary>
    public class MaterialQualityStandardCreatedEto
    {
        public Guid Id { get; set; }
        public Guid MaterialId { get; set; }
        public string StandardCode { get; set; }
        public string StandardName { get; set; }
    }

    /// <summary>
    /// 物料质量标准状态变更事件
    /// </summary>
    public class MaterialQualityStandardStatusChangedEto
    {
        public Guid Id { get; set; }
        public QualityStandardStatus Status { get; set; }
        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// 物料质量标准版本变更事件
    /// </summary>
    public class MaterialQualityStandardVersionChangedEto
    {
        public Guid Id { get; set; }
        public string Version { get; set; }
    }
}