using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序质量管理器 - 负责处理工序质量相关的领域逻辑和事件
    /// </summary>
    public class OperationQualityManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationQualityRequirement, Guid> _qualityRequirementRepository;
        private readonly IRepository<OperationExecution, Guid> _operationExecutionRepository;

        public OperationQualityManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationQualityRequirement, Guid> qualityRequirementRepository,
            IRepository<OperationExecution, Guid> operationExecutionRepository)
        {
            _operationRepository = operationRepository;
            _qualityRequirementRepository = qualityRequirementRepository;
            _operationExecutionRepository = operationExecutionRepository;
        }

        /// <summary>
        /// 设置质量要求
        /// </summary>
        public async Task SetQualityRequirementsAsync(Guid operationId, List<OperationQualityRequirement> requirements)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            
            foreach (var requirement in requirements)
            {
                requirement.OperationId = operationId;
                await _qualityRequirementRepository.InsertAsync(requirement);
            }

            await LocalEventBus.PublishAsync(
                new OperationQualityRequirementsSetEto
                {
                    OperationId = operationId,
                    RequirementIds = requirements.Select(r => r.Id).ToList()
                });
        }

        /// <summary>
        /// 验证工序质量
        /// </summary>
        public async Task<bool> ValidateQualityAsync(Guid executionId)
        {
            var execution = await _operationExecutionRepository.GetAsync(executionId);
            var requirements = await _qualityRequirementRepository.GetListAsync(r => r.OperationId == execution.OperationId);
            
            // 在这里实现具体的质量验证逻辑
            var validationResult = true;
            
            await LocalEventBus.PublishAsync(
                new OperationQualityValidatedEto
                {
                    ExecutionId = executionId,
                    OperationId = execution.OperationId,
                    IsValid = validationResult
                });

            return validationResult;
        }

        /// <summary>
        /// 获取质量要求列表
        /// </summary>
        public async Task<List<OperationQualityRequirement>> GetQualityRequirementsAsync(Guid operationId)
        {
            return await _qualityRequirementRepository.GetListAsync(r => r.OperationId == operationId);
        }

        /// <summary>
        /// 删除质量要求
        /// </summary>
        public async Task DeleteQualityRequirementAsync(Guid requirementId)
        {
            var requirement = await _qualityRequirementRepository.GetAsync(requirementId);
            await _qualityRequirementRepository.DeleteAsync(requirement);

            await LocalEventBus.PublishAsync(
                new OperationQualityRequirementDeletedEto
                {
                    RequirementId = requirementId,
                    OperationId = requirement.OperationId
                });
        }
    }
}