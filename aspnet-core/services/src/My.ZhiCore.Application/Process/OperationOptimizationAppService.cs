using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序优化应用服务
    /// </summary>
    public class OperationOptimizationAppService : ApplicationService, IOperationOptimizationAppService
    {
        private readonly OperationExecutionManager _executionManager;
        private readonly OperationResourceManager _resourceManager;
        private readonly ILogger<OperationOptimizationAppService> _logger;

        public OperationOptimizationAppService(
            OperationExecutionManager executionManager,
            OperationResourceManager resourceManager,
            ILogger<OperationOptimizationAppService> logger)
        {
            _executionManager = executionManager;
            _resourceManager = resourceManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取工序历史数据优化建议
        /// </summary>
        public async Task<List<OperationOptimizationSuggestionDto>> GetHistoricalOptimizationSuggestionsAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("开始分析工序历史数据，工序ID：{OperationId}", operationId);
                var executions = await _executionManager.GetOperationExecutionsAsync(operationId);
                var suggestions = new List<OperationOptimizationSuggestionDto>();

                // 分析执行时间异常
                var avgExecutionTime = executions
                    .Where(e => e.Status == OperationExecutionStatus.Completed)
                    .Average(e => (e.EndTime - e.StartTime).TotalMinutes);

                var timeAnomalies = executions
                    .Where(e => e.Status == OperationExecutionStatus.Completed &&
                           (e.EndTime - e.StartTime).TotalMinutes > avgExecutionTime * 1.5)
                    .ToList();

                if (timeAnomalies.Any())
                {
                    suggestions.Add(new OperationOptimizationSuggestionDto
                    {
                        Category = "执行时间",
                        Description = $"发现{timeAnomalies.Count}次执行时间异常，建议优化工序执行流程",
                        Priority = OptimizationPriority.High
                    });
                }

                // 分析失败原因
                var failureGroups = executions
                    .Where(e => e.Status == OperationExecutionStatus.Completed && !e.IsSuccess)
                    .GroupBy(e => e.FailureReason)
                    .OrderByDescending(g => g.Count())
                    .Take(3)
                    .ToList();

                foreach (var group in failureGroups)
                {
                    suggestions.Add(new OperationOptimizationSuggestionDto
                    {
                        Category = "失败原因",
                        Description = $"频繁失败原因：{group.Key}，建议重点关注并制定预防措施",
                        Priority = OptimizationPriority.High
                    });
                }

                _logger.LogInformation("工序历史数据分析完成，工序ID：{OperationId}", operationId);
                return suggestions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序历史数据优化建议失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序历史数据优化建议失败", ex);
            }
        }

        /// <summary>
        /// 获取工序资源优化建议
        /// </summary>
        public async Task<List<OperationOptimizationSuggestionDto>> GetResourceOptimizationSuggestionsAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("开始分析工序资源使用情况，工序ID：{OperationId}", operationId);
                var resources = await _resourceManager.GetOperationResourcesAsync(operationId);
                var suggestions = new List<OperationOptimizationSuggestionDto>();

                // 分析资源利用率
                foreach (var resource in resources)
                {
                    if (resource.UtilizationRate < 0.5)
                    {
                        suggestions.Add(new OperationOptimizationSuggestionDto
                        {
                            Category = "资源利用",
                            Description = $"资源{resource.Name}利用率低于50%，建议优化资源配置",
                            Priority = OptimizationPriority.Medium
                        });
                    }
                }

                // 分析资源瓶颈
                var bottleneckResources = resources
                    .Where(r => r.UtilizationRate > 0.9)
                    .ToList();

                if (bottleneckResources.Any())
                {
                    suggestions.Add(new OperationOptimizationSuggestionDto
                    {
                        Category = "资源瓶颈",
                        Description = $"发现{bottleneckResources.Count}个资源利用率超过90%，可能存在瓶颈",
                        Priority = OptimizationPriority.High
                    });
                }

                _logger.LogInformation("工序资源使用情况分析完成，工序ID：{OperationId}", operationId);
                return suggestions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序资源优化建议失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序资源优化建议失败", ex);
            }
        }
    }
}