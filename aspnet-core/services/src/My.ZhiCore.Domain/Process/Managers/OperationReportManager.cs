using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序报告管理器 - 负责生成工序执行报告和统计分析
    /// </summary>
    public class OperationReportManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationExecution, Guid> _operationExecutionRepository;
        private readonly IRepository<OperationPerformanceMetrics, Guid> _metricsRepository;

        public OperationReportManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationExecution, Guid> operationExecutionRepository,
            IRepository<OperationPerformanceMetrics, Guid> metricsRepository)
        {
            _operationRepository = operationRepository;
            _operationExecutionRepository = operationExecutionRepository;
            _metricsRepository = metricsRepository;
        }

        /// <summary>
        /// 生成工序执行报告
        /// </summary>
        public async Task<OperationReport> GenerateExecutionReportAsync(
            Guid operationId,
            DateTime startTime,
            DateTime endTime)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            var executions = await _operationExecutionRepository
                .GetListAsync(e => e.OperationId == operationId 
                    && e.StartTime >= startTime 
                    && e.EndTime <= endTime);

            var metrics = await _metricsRepository
                .GetListAsync(m => m.OperationId == operationId
                    && m.RecordTime >= startTime
                    && m.RecordTime <= endTime);

            var report = new OperationReport
            {
                OperationId = operationId,
                StartTime = startTime,
                EndTime = endTime,
                TotalExecutions = executions.Count,
                CompletedExecutions = executions.Count(e => e.Status == OperationExecutionStatus.Completed),
                AverageCycleTime = metrics.Average(m => m.CycleTime),
                AverageOutputRate = metrics.Average(m => m.OutputRate),
                AverageQualityRate = metrics.Average(m => m.QualityRate),
                AverageEquipmentEfficiency = metrics.Average(m => m.EquipmentEfficiency)
            };

            await LocalEventBus.PublishAsync(
                new OperationReportGeneratedEto
                {
                    OperationId = operationId,
                    Report = report
                });

            return report;
        }

        /// <summary>
        /// 生成工序性能分析报告
        /// </summary>
        public async Task<OperationPerformanceAnalysis> GeneratePerformanceAnalysisAsync(
            Guid operationId,
            DateTime startTime,
            DateTime endTime)
        {
            var metrics = await _metricsRepository
                .GetListAsync(m => m.OperationId == operationId
                    && m.RecordTime >= startTime
                    && m.RecordTime <= endTime);

            var analysis = new OperationPerformanceAnalysis
            {
                OperationId = operationId,
                AnalysisPeriod = new DateTimeRange(startTime, endTime),
                PerformanceIndicators = new Dictionary<string, double>
                {
                    { "MinCycleTime", metrics.Min(m => m.CycleTime) },
                    { "MaxCycleTime", metrics.Max(m => m.CycleTime) },
                    { "AverageCycleTime", metrics.Average(m => m.CycleTime) },
                    { "StandardDeviation", CalculateStandardDeviation(metrics.Select(m => m.CycleTime)) }
                },
                TrendAnalysis = AnalyzePerformanceTrend(metrics)
            };

            await LocalEventBus.PublishAsync(
                new OperationPerformanceAnalysisGeneratedEto
                {
                    OperationId = operationId,
                    Analysis = analysis
                });

            return analysis;
        }

        private double CalculateStandardDeviation(IEnumerable<double> values)
        {
            var avg = values.Average();
            var sumOfSquares = values.Sum(x => Math.Pow(x - avg, 2));
            return Math.Sqrt(sumOfSquares / values.Count());
        }

        private Dictionary<string, string> AnalyzePerformanceTrend(List<OperationPerformanceMetrics> metrics)
        {
            // 实现趋势分析逻辑
            var trends = new Dictionary<string, string>();
            
            // 分析周期时间趋势
            var cycleTimeTrend = AnalyzeTrend(metrics.Select(m => m.CycleTime));
            trends.Add("CycleTimeTrend", cycleTimeTrend);

            // 分析产出率趋势
            var outputRateTrend = AnalyzeTrend(metrics.Select(m => m.OutputRate));
            trends.Add("OutputRateTrend", outputRateTrend);

            return trends;
        }

        private string AnalyzeTrend(IEnumerable<double> values)
        {
            var first = values.First();
            var last = values.Last();
            var change = ((last - first) / first) * 100;

            if (change > 5) return "上升";
            if (change < -5) return "下降";
            return "稳定";
        }
    }
}