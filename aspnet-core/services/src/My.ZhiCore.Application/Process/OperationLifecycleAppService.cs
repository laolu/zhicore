using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序生命周期应用服务
    /// </summary>
    public class OperationLifecycleAppService : ApplicationService, IOperationLifecycleAppService
    {
        private readonly OperationLifecycleManager _lifecycleManager;
        private readonly ILogger<OperationLifecycleAppService> _logger;

        public OperationLifecycleAppService(
            OperationLifecycleManager lifecycleManager,
            ILogger<OperationLifecycleAppService> logger)
        {
            _lifecycleManager = lifecycleManager;
            _logger = logger;
        }

        /// <summary>
        /// 激活工序
        /// </summary>
        public async Task<OperationLifecycleDto> ActivateOperationAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("开始激活工序，工序ID：{OperationId}", operationId);
                var lifecycle = await _lifecycleManager.ActivateOperationAsync(operationId);
                _logger.LogInformation("工序激活成功，工序ID：{OperationId}", operationId);
                return ObjectMapper.Map<OperationLifecycle, OperationLifecycleDto>(lifecycle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "工序激活失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("工序激活失败", ex);
            }
        }

        /// <summary>
        /// 暂停工序
        /// </summary>
        public async Task<OperationLifecycleDto> SuspendOperationAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("开始暂停工序，工序ID：{OperationId}", operationId);
                var lifecycle = await _lifecycleManager.SuspendOperationAsync(operationId);
                _logger.LogInformation("工序暂停成功，工序ID：{OperationId}", operationId);
                return ObjectMapper.Map<OperationLifecycle, OperationLifecycleDto>(lifecycle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "工序暂停失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("工序暂停失败", ex);
            }
        }

        /// <summary>
        /// 停用工序
        /// </summary>
        public async Task<OperationLifecycleDto> DeactivateOperationAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("开始停用工序，工序ID：{OperationId}", operationId);
                var lifecycle = await _lifecycleManager.DeactivateOperationAsync(operationId);
                _logger.LogInformation("工序停用成功，工序ID：{OperationId}", operationId);
                return ObjectMapper.Map<OperationLifecycle, OperationLifecycleDto>(lifecycle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "工序停用失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("工序停用失败", ex);
            }
        }

        /// <summary>
        /// 获取工序生命周期状态
        /// </summary>
        public async Task<OperationLifecycleDto> GetOperationLifecycleAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("获取工序生命周期状态，工序ID：{OperationId}", operationId);
                var lifecycle = await _lifecycleManager.GetOperationLifecycleAsync(operationId);
                return ObjectMapper.Map<OperationLifecycle, OperationLifecycleDto>(lifecycle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序生命周期状态失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序生命周期状态失败", ex);
            }
        }
    }
}