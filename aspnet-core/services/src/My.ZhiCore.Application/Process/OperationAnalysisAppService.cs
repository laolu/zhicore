using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序执行分析应用服务
    /// </summary>
    public class OperationAnalysisAppService : ApplicationService, IOperationAnalysisAppService
    {
        private readonly OperationExecutionManager _executionManager;
        private readonly ILogger<OperationAnalysisAppService> _logger;

        public OperationAnalysisAppService(
            OperationExecutionManager executionManager,
            ILogger<OperationAnalysisAppService> logger)
        {
            _executionManager = executionManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取工序执行效率分析
        /// </summary>
        public async Task<OperationEfficiencyAnalysisDto> GetEfficiencyAnalysisAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("开始分析工序执行效率，工序ID：{OperationId}", operationId);
                var executions = await _executionManager.GetOperationExecutionsAsync(operationId);
                
                var analysis = new OperationEfficiencyAnalysisDto
                {
                    OperationId = operationId,
                    TotalExecutions = executions.Count,
                    SuccessfulExecutions = executions.Count(e => e.Status == OperationExecutionStatus.Completed && e.IsSuccess),
                    FailedExecutions = executions.Count(e => e.Status == OperationExecutionStatus.Completed && !e.IsSuccess),
                    AverageExecutionTime = executions.Where(e => e.Status == OperationExecutionStatus.Completed)
                        .Average(e => (e.EndTime - e.StartTime).TotalMinutes)
                };

                _logger.LogInformation("工序执行效率分析完成，工序ID：{OperationId}", operationId);
                return analysis;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "工序执行效率分析失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("工序执行效率分析失败", ex);
            }
        }

        /// <summary>
        /// 获取工序异常分析
        /// </summary>
        public async Task<List<OperationAnomalyAnalysisDto>> GetAnomalyAnalysisAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("开始分析工序执行异常，工序ID：{OperationId}", operationId);
                var executions = await _executionManager.GetOperationExecutionsAsync(operationId);
                
                var anomalies = executions
                    .Where(e => e.Status == OperationExecutionStatus.Completed && !e.IsSuccess)
                    .GroupBy(e => e.FailureReason)
                    .Select(g => new OperationAnomalyAnalysisDto
                    {
                        FailureReason = g.Key,
                        OccurrenceCount = g.Count(),
                        LastOccurrenceTime = g.Max(e => e.EndTime)
                    })
                    .OrderByDescending(a => a.OccurrenceCount)
                    .ToList();

                _logger.LogInformation("工序执行异常分析完成，工序ID：{OperationId}", operationId);
                return anomalies;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "工序执行异常分析失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("工序执行异常分析失败", ex);
            }
        }
    }
}