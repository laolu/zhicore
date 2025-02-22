using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Organization.WorkCenter
{
    /// <summary>
    /// 工作中心基础服务
    /// </summary>
    public class WorkCenterBaseAppService : ApplicationService
    {
        private readonly WorkCenterManager _workCenterManager;
        private readonly ILogger<WorkCenterBaseAppService> _logger;

        public WorkCenterBaseAppService(
            WorkCenterManager workCenterManager,
            ILogger<WorkCenterBaseAppService> logger)
        {
            _workCenterManager = workCenterManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建工作中心
        /// </summary>
        public async Task<WorkCenterDto> CreateAsync(CreateWorkCenterDto input)
        {
            try
            {
                _logger.LogInformation("开始创建工作中心，编码：{Code}", input.Code);
                var workCenter = await _workCenterManager.CreateAsync(
                    input.Code,
                    input.Name,
                    input.Type,
                    input.WorkshopId);
                _logger.LogInformation("工作中心创建成功，ID：{Id}", workCenter.Id);
                return ObjectMapper.Map<WorkCenter, WorkCenterDto>(workCenter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建工作中心失败，编码：{Code}", input.Code);
                throw new UserFriendlyException("创建工作中心失败", ex);
            }
        }

        /// <summary>
        /// 更新工作中心
        /// </summary>
        public async Task<WorkCenterDto> UpdateAsync(UpdateWorkCenterDto input)
        {
            try
            {
                _logger.LogInformation("开始更新工作中心，ID：{Id}", input.Id);
                var workCenter = await _workCenterManager.UpdateAsync(
                    input.Id,
                    input.Name,
                    input.Type);
                _logger.LogInformation("工作中心更新成功，ID：{Id}", workCenter.Id);
                return ObjectMapper.Map<WorkCenter, WorkCenterDto>(workCenter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新工作中心失败，ID：{Id}", input.Id);
                throw new UserFriendlyException("更新工作中心失败", ex);
            }
        }

        /// <summary>
        /// 删除工作中心
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("开始删除工作中心，ID：{Id}", id);
                await _workCenterManager.DeleteAsync(id);
                _logger.LogInformation("工作中心删除成功，ID：{Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除工作中心失败，ID：{Id}", id);
                throw new UserFriendlyException("删除工作中心失败", ex);
            }
        }

        /// <summary>
        /// 获取工作中心
        /// </summary>
        public async Task<WorkCenterDto> GetAsync(Guid id)
        {
            var workCenter = await _workCenterManager.GetAsync(id);
            return ObjectMapper.Map<WorkCenter, WorkCenterDto>(workCenter);
        }
    }
}