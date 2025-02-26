using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序分析管理器 - 负责处理工序执行的数据分析和性能评估
    /// </summary>
    public class OperationAnalysisManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationExecution, Guid> _operationExecutionRepository;
        private readonly IRepository<OperationPlan, Guid> _operationPlanRepository;

        public OperationAnalysisManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationExecution, Guid> operationExecutionRepository,
            IRepository<OperationPlan, Guid> operationPlanRepository)
        {
            _operationRepository = operationRepository;
            _operationExecutionRepository = operationExecutionRepository;
            _operationPlanRepository = operationPlanRepository;
        }

        /// <summary>
        /// 分析工序执行效率
        /// </summary>
        public async Task<OperationEfficiencyAnalysis> AnalyzeOperationEfficiencyAsync(
            Guid operationId,
            DateTime startTime,
            DateTime endTime)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            var executions = await _operationExecutionRepository.GetListAsync(
                x => x.OperationId == operationId &&
                     x.StartTime >= startTime &&
                     x.EndTime <= endTime);

            var analysis = new OperationEfficiencyAnalysis
            {
                OperationId = operationId,
                StartTime = startTime,
                EndTime = endTime,
                TotalExecutions = executions.Count,
                AverageExecutionTime = CalculateAverageExecutionTime(executions),
                SuccessRate = CalculateSuccessRate(executions)
            };

            await LocalEventBus.PublishAsync(
                new OperationEfficiencyAnalyzedEto
                {
                    Id = analysis.Id,
                    OperationId = analysis.OperationId,
                    TotalExecutions = analysis.TotalExecutions,
                    AverageExecutionTime = analysis.AverageExecutionTime,
                    SuccessRate = analysis.SuccessRate
                });

            return analysis;
        }

        /// <summary>
        /// 分析计划完成情况
        /// </summary>
        public async Task<OperationPlanAnalysis> AnalyzeOperationPlanAsync(
            Guid operationId,
            DateTime startTime,
            DateTime endTime)
        {
            var plans = await _operationPlanRepository.GetListAsync(
                x => x.OperationId == operationId &&
                     x.PlannedStartTime >= startTime &&
                     x.PlannedEndTime <= endTime);

            var analysis = new OperationPlanAnalysis
            {
                OperationId = operationId,
                StartTime = startTime,
                EndTime = endTime,
                TotalPlans = plans.Count,
                CompletedPlans = plans.Count(x => x.Status == OperationPlanStatus.Completed),
                PlanAchievementRate = CalculatePlanAchievementRate(plans)
            };

            await LocalEventBus.PublishAsync(
                new OperationPlanAnalyzedEto
                {
                    Id = analysis.Id,
                    OperationId = analysis.OperationId,
                    TotalPlans = analysis.TotalPlans,
                    CompletedPlans = analysis.CompletedPlans,
                    PlanAchievementRate = analysis.PlanAchievementRate
                });

            return analysis;
        }

        /// <summary>
        /// 生成工序性能报告
        /// </summary>
        public async Task<OperationPerformanceReport> GeneratePerformanceReportAsync(
            Guid operationId,
            DateTime startTime,
            DateTime endTime)
        {
            var efficiencyAnalysis = await AnalyzeOperationEfficiencyAsync(operationId, startTime, endTime);
            var planAnalysis = await AnalyzeOperationPlanAsync(operationId, startTime, endTime);

            var report = new OperationPerformanceReport
            {
                OperationId = operationId,
                StartTime = startTime,
                EndTime = endTime,
                EfficiencyAnalysis = efficiencyAnalysis,
                PlanAnalysis = planAnalysis
            };

            await LocalEventBus.PublishAsync(
                new OperationPerformanceReportGeneratedEto
                {
                    Id = report.Id,
                    OperationId = report.OperationId,
                    StartTime = report.StartTime,
                    EndTime = report.EndTime
                });

            return report;
        }

        private TimeSpan CalculateAverageExecutionTime(List<OperationExecution> executions)
        {
            if (!executions.Any())
                return TimeSpan.Zero;

            var totalTime = executions
                .Where(x => x.EndTime.HasValue)
                .Sum(x => (x.EndTime.Value - x.StartTime).TotalMilliseconds);

            return TimeSpan.FromMilliseconds(totalTime / executions.Count);
        }

        private double CalculateSuccessRate(List<OperationExecution> executions)
        {
            if (!executions.Any())
                return 0;

            var successfulExecutions = executions.Count(x => 
                x.Status == OperationExecutionStatus.Completed);

            return (double)successfulExecutions / executions.Count;
        }

        private double CalculatePlanAchievementRate(List<OperationPlan> plans)
        {
            if (!plans.Any())
                return 0;

            var achievedPlans = plans.Count(x =>
                x.Status == OperationPlanStatus.Completed &&
                x.ActualQuantity >= x.PlannedQuantity);

            return (double)achievedPlans / plans.Count;
        }
    }
}