using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Shift
{
    /// <summary>
    /// 班次交接应用服务
    /// </summary>
    public class ShiftHandoverAppService : ApplicationService
    {
        private readonly ShiftManager _shiftManager;
        private readonly ILogger<ShiftHandoverAppService> _logger;

        public ShiftHandoverAppService(
            ShiftManager shiftManager,
            ILogger<ShiftHandoverAppService> logger)
        {
            _shiftManager = shiftManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建交接记录
        /// </summary>
        public async Task<ShiftHandoverRecordDto> CreateHandoverRecordAsync(CreateHandoverRecordDto input)
        {
            try
            {
                _logger.LogInformation("开始创建班次交接记录，交接班次ID：{ShiftId}", input.ShiftId);
                var record = await _shiftManager.CreateHandoverRecordAsync(
                    input.ShiftId,
                    input.FromEmployeeId,
                    input.ToEmployeeId,
                    input.HandoverTime,
                    input.HandoverContent,
                    input.Remark);
                _logger.LogInformation("班次交接记录创建成功，记录ID：{Id}", record.Id);
                return ObjectMapper.Map<ShiftHandoverRecord, ShiftHandoverRecordDto>(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建班次交接记录失败，交接班次ID：{ShiftId}", input.ShiftId);
                throw new UserFriendlyException("创建班次交接记录失败", ex);
            }
        }

        /// <summary>
        /// 确认交接记录
        /// </summary>
        public async Task<ShiftHandoverRecordDto> ConfirmHandoverRecordAsync(Guid id, ConfirmHandoverRecordDto input)
        {
            try
            {
                _logger.LogInformation("开始确认班次交接记录，记录ID：{Id}", id);
                var record = await _shiftManager.ConfirmHandoverRecordAsync(
                    id,
                    input.ConfirmTime,
                    input.ConfirmRemark);
                _logger.LogInformation("班次交接记录确认成功，记录ID：{Id}", record.Id);
                return ObjectMapper.Map<ShiftHandoverRecord, ShiftHandoverRecordDto>(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "确认班次交接记录失败，记录ID：{Id}", id);
                throw new UserFriendlyException("确认班次交接记录失败", ex);
            }
        }

        /// <summary>
        /// 获取交接记录
        /// </summary>
        public async Task<ShiftHandoverRecordDto> GetHandoverRecordAsync(Guid id)
        {
            var record = await _shiftManager.GetHandoverRecordAsync(id);
            return ObjectMapper.Map<ShiftHandoverRecord, ShiftHandoverRecordDto>(record);
        }
    }
}