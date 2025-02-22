using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序监控应用服务
    /// </summary>
    public class OperationMonitorAppService : ApplicationService, IOperationMonitorAppService
    {
        private readonly OperationMonitorManager _monitorManager;
        private readonly ILogger<OperationMonitorAppService> _logger;

        public OperationMonitorAppService(
            OperationMonitorManager monitorManager,
            ILogger<OperationMonitorAppService> logger)
        {
            _monitorManager = monitorManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取工序状态
        /// </summary>
        public async Task<OperationStatusDto> GetOperationStatusAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("获取工序状态，工序ID：{OperationId}", operationId);
                var status = await _monitorManager.GetOperationStatusAsync(operationId);
                return ObjectMapper.Map<OperationStatus, OperationStatusDto>(status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序状态失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序状态失败", ex);
            }
        }
    }
}