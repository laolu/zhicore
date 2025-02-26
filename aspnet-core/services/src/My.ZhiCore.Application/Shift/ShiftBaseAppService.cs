using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Shift
{
    /// <summary>
    /// 班次基础应用服务
    /// </summary>
    public class ShiftBaseAppService : ApplicationService
    {
        private readonly ShiftManager _shiftManager;
        private readonly ILogger<ShiftBaseAppService> _logger;

        public ShiftBaseAppService(
            ShiftManager shiftManager,
            ILogger<ShiftBaseAppService> logger)
        {
            _shiftManager = shiftManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建班次
        /// </summary>
        public async Task<ShiftDto> CreateAsync(CreateShiftDto input)
        {
            try
            {
                _logger.LogInformation("开始创建班次，名称：{Name}", input.Name);
                var shift = await _shiftManager.CreateAsync(
                    input.Name,
                    input.StartTime,
                    input.EndTime,
                    input.Description);
                _logger.LogInformation("班次创建成功，ID：{Id}", shift.Id);
                return ObjectMapper.Map<Shift, ShiftDto>(shift);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建班次失败，名称：{Name}", input.Name);
                throw new UserFriendlyException("创建班次失败", ex);
            }
        }

        /// <summary>
        /// 更新班次
        /// </summary>
        public async Task<ShiftDto> UpdateAsync(Guid id, UpdateShiftDto input)
        {
            try
            {
                _logger.LogInformation("开始更新班次，ID：{Id}", id);
                var shift = await _shiftManager.UpdateAsync(
                    id,
                    input.Name,
                    input.StartTime,
                    input.EndTime,
                    input.Description);
                _logger.LogInformation("班次更新成功，ID：{Id}", shift.Id);
                return ObjectMapper.Map<Shift, ShiftDto>(shift);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新班次失败，ID：{Id}", id);
                throw new UserFriendlyException("更新班次失败", ex);
            }
        }

        /// <summary>
        /// 删除班次
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("开始删除班次，ID：{Id}", id);
                await _shiftManager.DeleteAsync(id);
                _logger.LogInformation("班次删除成功，ID：{Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除班次失败，ID：{Id}", id);
                throw new UserFriendlyException("删除班次失败", ex);
            }
        }

        /// <summary>
        /// 获取班次
        /// </summary>
        public async Task<ShiftDto> GetAsync(Guid id)
        {
            var shift = await _shiftManager.GetAsync(id);
            return ObjectMapper.Map<Shift, ShiftDto>(shift);
        }
    }
}