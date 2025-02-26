using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序执行应用服务
    /// </summary>
    public class OperationExecutionAppService : ApplicationService, IOperationExecutionAppService
    {
        private readonly OperationExecutionManager _executionManager;
        private readonly ILogger<OperationExecutionAppService> _logger;

        public OperationExecutionAppService(
            OperationExecutionManager executionManager,
            ILogger<OperationExecutionAppService> logger)
        {
            _executionManager = executionManager;
            _logger = logger;
        }

        /// <summary>
        /// 开始执行工序
        /// </summary>
        public async Task<OperationExecutionDto> StartOperationAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("开始执行工序，工序ID：{OperationId}", operationId);
                var execution = await _executionManager.StartOperationExecutionAsync(operationId, Guid.NewGuid().ToString());
                _logger.LogInformation("工序执行成功启动，执行ID：{ExecutionId}", execution.Id);
                return ObjectMapper.Map<OperationExecution, OperationExecutionDto>(execution);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "工序执行启动失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("工序执行启动失败", ex);
            }
        }

        /// <summary>
        /// 完成工序
        /// </summary>
        public async Task<OperationExecutionDto> CompleteOperationAsync(CompleteOperationDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("输入参数不能为空");
            }

            if (input.ExecutionId == Guid.Empty)
            {
                throw new UserFriendlyException("执行ID不能为空");
            }

            try
            {
                _logger.LogInformation("开始完成工序执行，执行ID：{ExecutionId}，执行结果：{IsSuccess}", input.ExecutionId, input.IsSuccess);
                await _executionManager.CompleteOperationExecutionAsync(input.ExecutionId, input.IsSuccess);
                var execution = await _executionManager.GetOperationExecutionAsync(input.ExecutionId);
                _logger.LogInformation("工序执行完成，执行ID：{ExecutionId}", execution.Id);
                return ObjectMapper.Map<OperationExecution, OperationExecutionDto>(execution);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "工序执行完成失败，执行ID：{ExecutionId}", input.ExecutionId);
                throw new UserFriendlyException("工序执行完成失败", ex);
            }
        }

        /// <summary>
        /// 获取工序执行记录
        /// </summary>
        public async Task<List<OperationExecutionDto>> GetOperationExecutionsAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("获取工序执行记录，工序ID：{OperationId}", operationId);
                var executions = await _executionManager.GetOperationExecutionsAsync(operationId);
                return ObjectMapper.Map<List<OperationExecution>, List<OperationExecutionDto>>(executions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序执行记录失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序执行记录失败", ex);
            }
        }
    }
}