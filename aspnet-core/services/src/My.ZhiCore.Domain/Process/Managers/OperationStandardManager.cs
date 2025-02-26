using System;
using System.Threading.Tasks;
using My.ZhiCore.Process.Events;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序标准管理器 - 负责管理工序的标准和规范
    /// </summary>
    public class OperationStandardManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationStandard, Guid> _operationStandardRepository;

        public OperationStandardManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationStandard, Guid> operationStandardRepository)
        {
            _operationRepository = operationRepository;
            _operationStandardRepository = operationStandardRepository;
        }

        /// <summary>
        /// 创建工序标准
        /// </summary>
        public async Task<OperationStandard> CreateStandardAsync(
            Guid operationId,
            string standardCode,
            string standardName,
            string description,
            string requirements,
            string qualityMetrics)
        {
            var operation = await _operationRepository.GetAsync(operationId);

            var standard = new OperationStandard
            {
                OperationId = operationId,
                StandardCode = standardCode,
                StandardName = standardName,
                Description = description,
                Requirements = requirements,
                QualityMetrics = qualityMetrics,
                Status = OperationStandardStatus.Active
            };

            await _operationStandardRepository.InsertAsync(standard);

            await LocalEventBus.PublishAsync(
                new OperationStandardCreatedEto
                {
                    Id = standard.Id,
                    OperationId = standard.OperationId,
                    StandardCode = standard.StandardCode,
                    StandardName = standard.StandardName
                });

            return standard;
        }

        /// <summary>
        /// 更新工序标准
        /// </summary>
        public async Task<OperationStandard> UpdateStandardAsync(
            Guid id,
            string standardName,
            string description,
            string requirements,
            string qualityMetrics)
        {
            var standard = await _operationStandardRepository.GetAsync(id);

            standard.StandardName = standardName;
            standard.Description = description;
            standard.Requirements = requirements;
            standard.QualityMetrics = qualityMetrics;

            await _operationStandardRepository.UpdateAsync(standard);

            await LocalEventBus.PublishAsync(
                new OperationStandardUpdatedEto
                {
                    Id = standard.Id,
                    OperationId = standard.OperationId,
                    StandardCode = standard.StandardCode,
                    StandardName = standard.StandardName
                });

            return standard;
        }

        /// <summary>
        /// 激活工序标准
        /// </summary>
        public async Task ActivateStandardAsync(Guid standardId)
        {
            var standard = await _operationStandardRepository.GetAsync(standardId);
            standard.Activate();
            await _operationStandardRepository.UpdateAsync(standard);

            await LocalEventBus.PublishAsync(
                new OperationStandardActivatedEto
                {
                    Id = standard.Id,
                    OperationId = standard.OperationId,
                    StandardCode = standard.StandardCode
                });
        }

        /// <summary>
        /// 停用工序标准
        /// </summary>
        public async Task DeactivateStandardAsync(Guid standardId)
        {
            var standard = await _operationStandardRepository.GetAsync(standardId);
            standard.Deactivate();
            await _operationStandardRepository.UpdateAsync(standard);

            await LocalEventBus.PublishAsync(
                new OperationStandardDeactivatedEto
                {
                    Id = standard.Id,
                    OperationId = standard.OperationId,
                    StandardCode = standard.StandardCode
                });
        }
    }
}