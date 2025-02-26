using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Organization.Workshop
{
    /// <summary>
    /// 车间基础服务
    /// </summary>
    public class WorkshopBaseAppService : ApplicationService
    {
        private readonly WorkshopManager _workshopManager;
        private readonly ILogger<WorkshopBaseAppService> _logger;

        public WorkshopBaseAppService(
            WorkshopManager workshopManager,
            ILogger<WorkshopBaseAppService> logger)
        {
            _workshopManager = workshopManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建车间
        /// </summary>
        public async Task<WorkshopDto> CreateAsync(CreateWorkshopDto input)
        {
            try
            {
                _logger.LogInformation("开始创建车间，编码：{Code}", input.Code);
                var workshop = await _workshopManager.CreateAsync(
                    input.Code,
                    input.Name,
                    input.Type,
                    input.Description);
                _logger.LogInformation("车间创建成功，ID：{Id}", workshop.Id);
                return ObjectMapper.Map<Workshop, WorkshopDto>(workshop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建车间失败，编码：{Code}", input.Code);
                throw new UserFriendlyException("创建车间失败", ex);
            }
        }

        /// <summary>
        /// 更新车间
        /// </summary>
        public async Task<WorkshopDto> UpdateAsync(UpdateWorkshopDto input)
        {
            try
            {
                _logger.LogInformation("开始更新车间，ID：{Id}", input.Id);
                var workshop = await _workshopManager.UpdateAsync(
                    input.Id,
                    input.Name,
                    input.Type,
                    input.Description);
                _logger.LogInformation("车间更新成功，ID：{Id}", workshop.Id);
                return ObjectMapper.Map<Workshop, WorkshopDto>(workshop);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新车间失败，ID：{Id}", input.Id);
                throw new UserFriendlyException("更新车间失败", ex);
            }
        }

        /// <summary>
        /// 删除车间
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("开始删除车间，ID：{Id}", id);
                await _workshopManager.DeleteAsync(id);
                _logger.LogInformation("车间删除成功，ID：{Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除车间失败，ID：{Id}", id);
                throw new UserFriendlyException("删除车间失败", ex);
            }
        }

        /// <summary>
        /// 获取车间
        /// </summary>
        public async Task<WorkshopDto> GetAsync(Guid id)
        {
            var workshop = await _workshopManager.GetAsync(id);
            return ObjectMapper.Map<Workshop, WorkshopDto>(workshop);
        }
    }
}