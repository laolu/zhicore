using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序计划管理器 - 负责处理工序计划安排的领域逻辑和事件
    /// </summary>
    public class OperationPlanManager : ZhiCoreDomainService
    {        
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationPlan, Guid> _operationPlanRepository;

        public OperationPlanManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationPlan, Guid> operationPlanRepository)
        {
            _operationRepository = operationRepository;
            _operationPlanRepository = operationPlanRepository;
        }

        /// <summary>
        /// 创建工序计划
        /// </summary>
        public async Task<OperationPlan> CreateOperationPlanAsync(
            Guid operationId,
            DateTime plannedStartTime,
            DateTime plannedEndTime,
            int plannedQuantity)
        {
            var operation = await _operationRepository.GetAsync(operationId);

            var plan = new OperationPlan
            {
                OperationId = operationId,
                PlannedStartTime = plannedStartTime,
                PlannedEndTime = plannedEndTime,
                PlannedQuantity = plannedQuantity,
                Status = OperationPlanStatus.Created
            };

            await _operationPlanRepository.InsertAsync(plan);

            await LocalEventBus.PublishAsync(
                new OperationPlanCreatedEto
                {
                    Id = plan.Id,
                    OperationId = plan.OperationId,
                    PlannedStartTime = plan.PlannedStartTime,
                    PlannedEndTime = plan.PlannedEndTime,
                    PlannedQuantity = plan.PlannedQuantity
                });

            return plan;
        }

        /// <summary>
        /// 更新工序计划
        /// </summary>
        public async Task UpdateOperationPlanAsync(
            Guid planId,
            DateTime? plannedStartTime = null,
            DateTime? plannedEndTime = null,
            int? plannedQuantity = null)
        {
            var plan = await _operationPlanRepository.GetAsync(planId);

            if (plannedStartTime.HasValue)
                plan.PlannedStartTime = plannedStartTime.Value;

            if (plannedEndTime.HasValue)
                plan.PlannedEndTime = plannedEndTime.Value;

            if (plannedQuantity.HasValue)
                plan.PlannedQuantity = plannedQuantity.Value;

            await _operationPlanRepository.UpdateAsync(plan);

            await LocalEventBus.PublishAsync(
                new OperationPlanUpdatedEto
                {
                    Id = plan.Id,
                    OperationId = plan.OperationId,
                    PlannedStartTime = plan.PlannedStartTime,
                    PlannedEndTime = plan.PlannedEndTime,
                    PlannedQuantity = plan.PlannedQuantity
                });
        }

        /// <summary>
        /// 取消工序计划
        /// </summary>
        public async Task CancelOperationPlanAsync(Guid planId)
        {
            var plan = await _operationPlanRepository.GetAsync(planId);
            plan.Status = OperationPlanStatus.Cancelled;
            await _operationPlanRepository.UpdateAsync(plan);

            await LocalEventBus.PublishAsync(
                new OperationPlanCancelledEto
                {
                    Id = plan.Id,
                    OperationId = plan.OperationId
                });
        }

        /// <summary>
        /// 完成工序计划
        /// </summary>
        public async Task CompleteOperationPlanAsync(
            Guid planId,
            int actualQuantity)
        {
            var plan = await _operationPlanRepository.GetAsync(planId);
            plan.Status = OperationPlanStatus.Completed;
            plan.ActualQuantity = actualQuantity;
            plan.CompletionTime = Clock.Now;
            await _operationPlanRepository.UpdateAsync(plan);

            await LocalEventBus.PublishAsync(
                new OperationPlanCompletedEto
                {
                    Id = plan.Id,
                    OperationId = plan.OperationId,
                    ActualQuantity = plan.ActualQuantity,
                    CompletionTime = plan.CompletionTime
                });
        }
    }
}