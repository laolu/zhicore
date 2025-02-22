using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序调度管理器 - 负责处理工序的调度和排程
    /// </summary>
    public class OperationScheduleManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationPlan, Guid> _operationPlanRepository;
        private readonly IRepository<OperationExecution, Guid> _operationExecutionRepository;

        public OperationScheduleManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationPlan, Guid> operationPlanRepository,
            IRepository<OperationExecution, Guid> operationExecutionRepository)
        {
            _operationRepository = operationRepository;
            _operationPlanRepository = operationPlanRepository;
            _operationExecutionRepository = operationExecutionRepository;
        }

        /// <summary>
        /// 调整工序优先级
        /// </summary>
        public async Task AdjustOperationPriorityAsync(Guid operationId, int priority)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            operation.Priority = priority;
            await _operationRepository.UpdateAsync(operation);

            await LocalEventBus.PublishAsync(
                new OperationPriorityAdjustedEto
                {
                    Id = operation.Id,
                    Priority = priority
                });
        }

        /// <summary>
        /// 重新排程工序计划
        /// </summary>
        public async Task RescheduleOperationPlanAsync(
            Guid planId,
            DateTime newStartTime,
            DateTime newEndTime)
        {
            var plan = await _operationPlanRepository.GetAsync(planId);
            
            plan.PlannedStartTime = newStartTime;
            plan.PlannedEndTime = newEndTime;
            
            await _operationPlanRepository.UpdateAsync(plan);

            await LocalEventBus.PublishAsync(
                new OperationPlanRescheduledEto
                {
                    Id = plan.Id,
                    OperationId = plan.OperationId,
                    NewStartTime = newStartTime,
                    NewEndTime = newEndTime
                });
        }

        /// <summary>
        /// 优化工序排程
        /// </summary>
        public async Task OptimizeOperationScheduleAsync(
            DateTime startTime,
            DateTime endTime,
            List<Guid> operationIds = null)
        {
            var query = await _operationPlanRepository.GetQueryableAsync();
            var plans = query.Where(p => p.PlannedStartTime >= startTime && p.PlannedEndTime <= endTime);
            
            if (operationIds != null && operationIds.Any())
            {
                plans = plans.Where(p => operationIds.Contains(p.OperationId));
            }

            var planList = await AsyncExecuter.ToListAsync(plans);
            
            // 在这里实现具体的排程优化算法
            // 可以考虑多种因素：工序优先级、资源利用率、交期等
            
            foreach (var plan in planList)
            {
                await _operationPlanRepository.UpdateAsync(plan);
            }

            await LocalEventBus.PublishAsync(
                new OperationScheduleOptimizedEto
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    OptimizedPlanIds = planList.Select(p => p.Id).ToList()
                });
        }
    }
}