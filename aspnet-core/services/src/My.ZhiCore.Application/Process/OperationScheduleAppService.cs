using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using My.ZhiCore.Process.Dtos;
using Microsoft.Extensions.Logging;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序调度应用服务
    /// </summary>
    public class OperationScheduleAppService : ApplicationService, IOperationScheduleAppService
    {
        private readonly OperationScheduleManager _scheduleManager;
        private readonly ILogger<OperationScheduleAppService> _logger;

        public OperationScheduleAppService(
            OperationScheduleManager scheduleManager,
            ILogger<OperationScheduleAppService> logger)
        {
            _scheduleManager = scheduleManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建工序调度计划
        /// </summary>
        public async Task<OperationScheduleDto> CreateScheduleAsync(CreateOperationScheduleDto input)
        {
            if (input.OperationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("创建工序调度计划，工序ID：{OperationId}", input.OperationId);
                var schedule = await _scheduleManager.CreateScheduleAsync(
                    input.OperationId,
                    input.Priority,
                    input.StartTime,
                    input.EndTime,
                    input.Resources);
                return ObjectMapper.Map<OperationSchedule, OperationScheduleDto>(schedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建工序调度计划失败，工序ID：{OperationId}", input.OperationId);
                throw new UserFriendlyException("创建工序调度计划失败", ex);
            }
        }

        /// <summary>
        /// 更新工序调度计划
        /// </summary>
        public async Task<OperationScheduleDto> UpdateScheduleAsync(UpdateOperationScheduleDto input)
        {
            if (input.ScheduleId == Guid.Empty)
            {
                throw new UserFriendlyException("调度计划ID不能为空");
            }

            try
            {
                _logger.LogInformation("更新工序调度计划，计划ID：{ScheduleId}", input.ScheduleId);
                var schedule = await _scheduleManager.UpdateScheduleAsync(
                    input.ScheduleId,
                    input.Priority,
                    input.StartTime,
                    input.EndTime,
                    input.Resources);
                return ObjectMapper.Map<OperationSchedule, OperationScheduleDto>(schedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新工序调度计划失败，计划ID：{ScheduleId}", input.ScheduleId);
                throw new UserFriendlyException("更新工序调度计划失败", ex);
            }
        }

        /// <summary>
        /// 删除工序调度计划
        /// </summary>
        public async Task DeleteScheduleAsync(Guid scheduleId)
        {
            if (scheduleId == Guid.Empty)
            {
                throw new UserFriendlyException("调度计划ID不能为空");
            }

            try
            {
                _logger.LogInformation("删除工序调度计划，计划ID：{ScheduleId}", scheduleId);
                await _scheduleManager.DeleteScheduleAsync(scheduleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除工序调度计划失败，计划ID：{ScheduleId}", scheduleId);
                throw new UserFriendlyException("删除工序调度计划失败", ex);
            }
        }

        /// <summary>
        /// 获取工序调度计划列表
        /// </summary>
        public async Task<List<OperationScheduleDto>> GetSchedulesAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("获取工序调度计划列表，工序ID：{OperationId}", operationId);
                var schedules = await _scheduleManager.GetSchedulesAsync(operationId);
                return ObjectMapper.Map<List<OperationSchedule>, List<OperationScheduleDto>>(schedules);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序调度计划列表失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序调度计划列表失败", ex);
            }
        }

        /// <summary>
        /// 优化工序调度计划
        /// </summary>
        public async Task<List<OperationScheduleDto>> OptimizeSchedulesAsync(OptimizeScheduleDto input)
        {
            try
            {
                _logger.LogInformation("优化工序调度计划，开始时间：{StartTime}，结束时间：{EndTime}", 
                    input.StartTime, input.EndTime);
                var schedules = await _scheduleManager.OptimizeSchedulesAsync(
                    input.StartTime,
                    input.EndTime,
                    input.OptimizationCriteria);
                return ObjectMapper.Map<List<OperationSchedule>, List<OperationScheduleDto>>(schedules);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "优化工序调度计划失败");
                throw new UserFriendlyException("优化工序调度计划失败", ex);
            }
        }
    }
}