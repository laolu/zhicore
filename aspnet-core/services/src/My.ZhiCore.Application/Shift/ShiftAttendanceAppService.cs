using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Shift
{
    /// <summary>
    /// 班次考勤应用服务
    /// </summary>
    public class ShiftAttendanceAppService : ApplicationService
    {
        private readonly ShiftManager _shiftManager;
        private readonly ILogger<ShiftAttendanceAppService> _logger;

        public ShiftAttendanceAppService(
            ShiftManager shiftManager,
            ILogger<ShiftAttendanceAppService> logger)
        {
            _shiftManager = shiftManager;
            _logger = logger;
        }

        /// <summary>
        /// 记录上班打卡
        /// </summary>
        public async Task<ShiftAttendanceRecordDto> ClockInAsync(ClockInDto input)
        {
            try
            {
                _logger.LogInformation("开始记录上班打卡，员工ID：{EmployeeId}，班次ID：{ShiftId}", input.EmployeeId, input.ShiftId);
                var record = await _shiftManager.RecordClockInAsync(
                    input.EmployeeId,
                    input.ShiftId,
                    input.ClockInTime,
                    input.Location,
                    input.Remark);
                _logger.LogInformation("上班打卡记录成功，记录ID：{Id}", record.Id);
                return ObjectMapper.Map<ShiftAttendanceRecord, ShiftAttendanceRecordDto>(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "记录上班打卡失败，员工ID：{EmployeeId}", input.EmployeeId);
                throw new UserFriendlyException("记录上班打卡失败", ex);
            }
        }

        /// <summary>
        /// 记录下班打卡
        /// </summary>
        public async Task<ShiftAttendanceRecordDto> ClockOutAsync(ClockOutDto input)
        {
            try
            {
                _logger.LogInformation("开始记录下班打卡，员工ID：{EmployeeId}，班次ID：{ShiftId}", input.EmployeeId, input.ShiftId);
                var record = await _shiftManager.RecordClockOutAsync(
                    input.EmployeeId,
                    input.ShiftId,
                    input.ClockOutTime,
                    input.Location,
                    input.Remark);
                _logger.LogInformation("下班打卡记录成功，记录ID：{Id}", record.Id);
                return ObjectMapper.Map<ShiftAttendanceRecord, ShiftAttendanceRecordDto>(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "记录下班打卡失败，员工ID：{EmployeeId}", input.EmployeeId);
                throw new UserFriendlyException("记录下班打卡失败", ex);
            }
        }

        /// <summary>
        /// 获取考勤记录
        /// </summary>
        public async Task<ShiftAttendanceRecordDto> GetAttendanceRecordAsync(Guid id)
        {
            var record = await _shiftManager.GetAttendanceRecordAsync(id);
            return ObjectMapper.Map<ShiftAttendanceRecord, ShiftAttendanceRecordDto>(record);
        }
    }
}