using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序统计管理器 - 负责统计和分析工序执行的各项指标
    /// </summary>
    public class OperationStatisticsManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationExecution, Guid> _operationExecutionRepository;
        private readonly IRepository<OperationPlan, Guid> _operationPlanRepository;

        public OperationStatisticsManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationExecution, Guid> operationExecutionRepository,
            IRepository<OperationPlan, Guid> operationPlanRepository)
        {
            _operationRepository = operationRepository;
            _operationExecutionRepository = operationExecutionRepository;
            _operationPlanRepository = operationPlanRepository;
        }

        /// <summary>
        /// 获取工序执行统计
        /// </summary>
        public async Task<OperationExecutionStatistics> GetExecutionStatisticsAsync(
            Guid operationId,
            DateTime? startTime = null,
            DateTime? endTime = null)
        {
            var query = await _operationExecutionRepository.GetQueryableAsync();
            query = query.Where(e => e.OperationId == operationId);

            if (startTime.HasValue)
            {
                query = query.Where(e => e.StartTime >= startTime.Value);
            }

            if (endTime.HasValue)
            {
                query = query.Where(e => e.EndTime <= endTime.Value);
            }

            var executions = await AsyncExecuter.ToListAsync(query);

            var statistics = new OperationExecutionStatistics
            {
                OperationId = operationId,
                TotalExecutions = executions.Count,
                CompletedExecutions = executions.Count(e => e.Status == OperationExecutionStatus.Completed),
                FailedExecutions = executions.Count(e => e.Status == OperationExecutionStatus.Failed),
                AverageExecutionTime = executions
                    .Where(e => e.EndTime.HasValue)
                    .Average(e => (e.EndTime.Value - e.StartTime).TotalMinutes)
            };

            await LocalEventBus.PublishAsync(
                new OperationStatisticsCalculatedEto
                {
                    OperationId = operationId,
                    Statistics = statistics
                });

            return statistics;
        }

        /// <summary>
        /// 获取工序计划完成率
        /// </summary>
        public async Task<OperationPlanCompletionRate> GetPlanCompletionRateAsync(
            Guid operationId,
            DateTime? startTime = null,
            DateTime? endTime = null)
        {
            var planQuery = await _operationPlanRepository.GetQueryableAsync();
            planQuery = planQuery.Where(p => p.OperationId == operationId);

            if (startTime.HasValue)
            {
                planQuery = planQuery.Where(p => p.PlannedStartTime >= startTime.Value);
            }

            if (endTime.HasValue)
            {
                planQuery = planQuery.Where(p => p.PlannedEndTime <= endTime.Value);
            }

            var plans = await AsyncExecuter.ToListAsync(planQuery);
            var executions = await _operationExecutionRepository.GetListAsync(
                e => e.OperationId == operationId &&
                (!startTime.HasValue || e.StartTime >= startTime.Value) &&
                (!endTime.HasValue || e.EndTime <= endTime.Value));

            var completionRate = new OperationPlanCompletionRate
            {
                OperationId = operationId,
                TotalPlannedQuantity = plans.Sum(p => p.PlannedQuantity),
                ActualCompletedQuantity = executions
                    .Where(e => e.Status == OperationExecutionStatus.Completed)
                    .Sum(e => e.CompletedQuantity),
                PlanCount = plans.Count,
                CompletedPlanCount = plans.Count(p => p.Status == OperationPlanStatus.Completed)
            };

            await LocalEventBus.PublishAsync(
                new OperationPlanCompletionRateCalculatedEto
                {
                    OperationId = operationId,
                    CompletionRate = completionRate
                });

            return completionRate;
        }

        /// <summary>
        /// 分析工序性能指标
        /// </summary>
        public async Task<OperationPerformanceMetrics> AnalyzePerformanceMetricsAsync(
            Guid operationId,
            DateTime startTime,
            DateTime endTime)
        {
            var executions = await _operationExecutionRepository.GetListAsync(
                e => e.OperationId == operationId &&
                e.StartTime >= startTime &&
                e.EndTime <= endTime);

            var metrics = new OperationPerformanceMetrics
            {
                OperationId = operationId,
                Period = new DateTimeRange(startTime, endTime),
                Availability = CalculateAvailability(executions),
                Quality = CalculateQuality(executions),
                Performance = CalculatePerformance(executions)
            };

            metrics.OEE = metrics.Availability * metrics.Quality * metrics.Performance;

            await LocalEventBus.PublishAsync(
                new OperationPerformanceMetricsCalculatedEto
                {
                    OperationId = operationId,
                    Metrics = metrics
                });

            return metrics;
        }

        private double CalculateAvailability(List<OperationExecution> executions)
        {
            // 计算设备可用性：实际运行时间 / 计划运行时间
            var totalPlannedTime = executions.Sum(e => (e.EndTime ?? Clock.Now) - e.StartTime).TotalMinutes;
            var actualRunningTime = executions
                .Where(e => e.Status == OperationExecutionStatus.Running || e.Status == OperationExecutionStatus.Completed)
                .Sum(e => (e.EndTime ?? Clock.Now) - e.StartTime).TotalMinutes;

            return totalPlannedTime > 0 ? actualRunningTime / totalPlannedTime : 0;
        }

        private double CalculateQuality(List<OperationExecution> executions)
        {
            // 计算质量指标：合格品数量 / 总生产数量
            var totalQuantity = executions.Sum(e => e.CompletedQuantity);
            var qualifiedQuantity = executions.Sum(e => e.QualifiedQuantity);

            return totalQuantity > 0 ? (double)qualifiedQuantity / totalQuantity : 0;
        }

        private double CalculatePerformance(List<OperationExecution> executions)
        {
            // 计算性能指标：实际产出 / 理论产出
            var actualOutput = executions.Sum(e => e.CompletedQuantity);
            var theoreticalOutput = executions.Sum(e => e.TheoreticalQuantity);

            return theoreticalOutput > 0 ? (double)actualOutput / theoreticalOutput : 0;
        }
    }
}