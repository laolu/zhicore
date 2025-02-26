using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Organization.WorkCenter
{
    /// <summary>
    /// 工作中心状态服务
    /// </summary>
    public class WorkCenterStatusAppService : ApplicationService
    {
        private readonly WorkCenterManager _workCenterManager;
        private readonly ILogger<WorkCenterStatusAppService> _logger;

        public WorkCenterStatusAppService(
            WorkCenterManager workCenterManager,
            ILogger<WorkCenterStatusAppService> logger)
        {
            _workCenterManager = workCenterManager;
            _logger = logger;
        }

        /// <summary>
        /// 更新工作中心状态
        /// </summary>
        public async Task<WorkCenterDto> UpdateStatusAsync(Guid id, WorkCenterStatus status)
        {
            try
            {
                _logger.LogInformation("开始更新工作中心状态，ID：{Id}，状态：{Status}", id, status);
                var workCenter = await _workCenterManager.UpdateStatusAsync(id, status);
                _logger.LogInformation("工作中心状态更新成功，ID：{Id}", workCenter.Id);
                return ObjectMapper.Map<WorkCenter, WorkCenterDto>(workCenter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新工作中心状态失败，ID：{Id}", id);
                throw new UserFriendlyException("更新工作中心状态失败", ex);
            }
        }

        /// <summary>
        /// 获取工作中心状态
        /// </summary>
        public async Task<WorkCenterStatus> GetStatusAsync(Guid id)
        {
            try
            {
                var workCenter = await _workCenterManager.GetAsync(id);
                return workCenter.Status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工作中心状态失败，ID：{Id}", id);
                throw new UserFriendlyException("获取工作中心状态失败", ex);
            }
        }
    }
}