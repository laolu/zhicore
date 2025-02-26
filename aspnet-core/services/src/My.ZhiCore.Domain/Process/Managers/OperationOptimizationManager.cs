using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序优化管理器 - 负责管理工序的优化建议和改进方案
    /// </summary>
    public class OperationOptimizationManager : ZhiCoreDomainService
    {   
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationOptimization, Guid> _optimizationRepository;

        public OperationOptimizationManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationOptimization, Guid> optimizationRepository)
        {
            _operationRepository = operationRepository;
            _optimizationRepository = optimizationRepository;
        }

        /// <summary>
        /// 创建工序优化建议
        /// </summary>
        public async Task<OperationOptimization> CreateOptimizationAsync(
            Guid operationId,
            string title,
            string description,
            string expectedBenefits,
            string implementationPlan)
        {
            var operation = await _operationRepository.GetAsync(operationId);

            var optimization = new OperationOptimization
            {
                OperationId = operationId,
                Title = title,
                Description = description,
                ExpectedBenefits = expectedBenefits,
                ImplementationPlan = implementationPlan,
                Status = OptimizationStatus.Proposed,
                CreationTime = Clock.Now
            };

            await _optimizationRepository.InsertAsync(optimization);

            await LocalEventBus.PublishAsync(
                new OperationOptimizationCreatedEto
                {
                    Id = optimization.Id,
                    OperationId = optimization.OperationId,
                    Title = optimization.Title
                });

            return optimization;
        }

        /// <summary>
        /// 更新工序优化建议
        /// </summary>
        public async Task<OperationOptimization> UpdateOptimizationAsync(
            Guid optimizationId,
            string title,
            string description,
            string expectedBenefits,
            string implementationPlan)
        {
            var optimization = await _optimizationRepository.GetAsync(optimizationId);

            optimization.Title = title;
            optimization.Description = description;
            optimization.ExpectedBenefits = expectedBenefits;
            optimization.ImplementationPlan = implementationPlan;
            optimization.LastModificationTime = Clock.Now;

            await _optimizationRepository.UpdateAsync(optimization);

            await LocalEventBus.PublishAsync(
                new OperationOptimizationUpdatedEto
                {
                    Id = optimization.Id,
                    OperationId = optimization.OperationId,
                    Title = optimization.Title
                });

            return optimization;
        }

        /// <summary>
        /// 批准工序优化建议
        /// </summary>
        public async Task ApproveOptimizationAsync(Guid optimizationId)
        {
            var optimization = await _optimizationRepository.GetAsync(optimizationId);
            optimization.Status = OptimizationStatus.Approved;
            optimization.ApprovalTime = Clock.Now;

            await _optimizationRepository.UpdateAsync(optimization);

            await LocalEventBus.PublishAsync(
                new OperationOptimizationApprovedEto
                {
                    Id = optimization.Id,
                    OperationId = optimization.OperationId
                });
        }

        /// <summary>
        /// 拒绝工序优化建议
        /// </summary>
        public async Task RejectOptimizationAsync(Guid optimizationId, string rejectionReason)
        {
            var optimization = await _optimizationRepository.GetAsync(optimizationId);
            optimization.Status = OptimizationStatus.Rejected;
            optimization.RejectionReason = rejectionReason;
            optimization.RejectionTime = Clock.Now;

            await _optimizationRepository.UpdateAsync(optimization);

            await LocalEventBus.PublishAsync(
                new OperationOptimizationRejectedEto
                {
                    Id = optimization.Id,
                    OperationId = optimization.OperationId,
                    RejectionReason = optimization.RejectionReason
                });
        }

        /// <summary>
        /// 开始实施工序优化
        /// </summary>
        public async Task StartImplementationAsync(Guid optimizationId)
        {
            var optimization = await _optimizationRepository.GetAsync(optimizationId);
            optimization.Status = OptimizationStatus.InProgress;
            optimization.ImplementationStartTime = Clock.Now;

            await _optimizationRepository.UpdateAsync(optimization);

            await LocalEventBus.PublishAsync(
                new OperationOptimizationImplementationStartedEto
                {
                    Id = optimization.Id,
                    OperationId = optimization.OperationId
                });
        }

        /// <summary>
        /// 完成工序优化实施
        /// </summary>
        public async Task CompleteImplementationAsync(Guid optimizationId, string implementationResults)
        {
            var optimization = await _optimizationRepository.GetAsync(optimizationId);
            optimization.Status = OptimizationStatus.Completed;
            optimization.ImplementationResults = implementationResults;
            optimization.CompletionTime = Clock.Now;

            await _optimizationRepository.UpdateAsync(optimization);

            await LocalEventBus.PublishAsync(
                new OperationOptimizationCompletedEto
                {
                    Id = optimization.Id,
                    OperationId = optimization.OperationId,
                    ImplementationResults = optimization.ImplementationResults
                });
        }
    }
}