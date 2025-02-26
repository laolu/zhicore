using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序依赖应用服务
    /// </summary>
    public class OperationDependencyAppService : ApplicationService, IOperationDependencyAppService
    {
        private readonly OperationDependencyManager _dependencyManager;
        private readonly ILogger<OperationDependencyAppService> _logger;

        public OperationDependencyAppService(
            OperationDependencyManager dependencyManager,
            ILogger<OperationDependencyAppService> logger)
        {
            _dependencyManager = dependencyManager;
            _logger = logger;
        }

        /// <summary>
        /// 添加工序依赖关系
        /// </summary>
        public async Task<OperationDependencyDto> AddDependencyAsync(AddOperationDependencyDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始添加工序依赖关系，源工序ID：{SourceId}，目标工序ID：{TargetId}", 
                    input.SourceOperationId, input.TargetOperationId);
                var dependency = await _dependencyManager.AddDependencyAsync(
                    input.SourceOperationId,
                    input.TargetOperationId,
                    input.DependencyType,
                    input.Description);
                _logger.LogInformation("工序依赖关系添加成功，ID：{Id}", dependency.Id);
                return ObjectMapper.Map<OperationDependency, OperationDependencyDto>(dependency);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加工序依赖关系失败，源工序ID：{SourceId}，目标工序ID：{TargetId}", 
                    input.SourceOperationId, input.TargetOperationId);
                throw new UserFriendlyException("添加工序依赖关系失败", ex);
            }
        }

        /// <summary>
        /// 移除工序依赖关系
        /// </summary>
        public async Task RemoveDependencyAsync(RemoveOperationDependencyDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始移除工序依赖关系，源工序ID：{SourceId}，目标工序ID：{TargetId}", 
                    input.SourceOperationId, input.TargetOperationId);
                await _dependencyManager.RemoveDependencyAsync(
                    input.SourceOperationId,
                    input.TargetOperationId);
                _logger.LogInformation("工序依赖关系移除成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "移除工序依赖关系失败，源工序ID：{SourceId}，目标工序ID：{TargetId}", 
                    input.SourceOperationId, input.TargetOperationId);
                throw new UserFriendlyException("移除工序依赖关系失败", ex);
            }
        }

        /// <summary>
        /// 获取工序的依赖工序列表
        /// </summary>
        public async Task<List<OperationDependencyDto>> GetDependenciesAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("获取工序依赖关系列表，工序ID：{OperationId}", operationId);
                var dependencies = await _dependencyManager.GetDependenciesAsync(operationId);
                return ObjectMapper.Map<List<OperationDependency>, List<OperationDependencyDto>>(dependencies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序依赖关系列表失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序依赖关系列表失败", ex);
            }
        }

        /// <summary>
        /// 验证工序依赖关系是否有效
        /// </summary>
        public async Task<bool> ValidateDependencyAsync(ValidateOperationDependencyDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            try
            {
                _logger.LogInformation("开始验证工序依赖关系，源工序ID：{SourceId}，目标工序ID：{TargetId}", 
                    input.SourceOperationId, input.TargetOperationId);
                return await _dependencyManager.ValidateDependencyAsync(
                    input.SourceOperationId,
                    input.TargetOperationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "验证工序依赖关系失败，源工序ID：{SourceId}，目标工序ID：{TargetId}", 
                    input.SourceOperationId, input.TargetOperationId);
                throw new UserFriendlyException("验证工序依赖关系失败", ex);
            }
        }
    }
}