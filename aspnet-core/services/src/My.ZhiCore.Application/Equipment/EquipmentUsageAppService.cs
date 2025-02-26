using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Equipment.Dtos;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备使用记录应用服务
    /// </summary>
    public class EquipmentUsageAppService : ApplicationService, IEquipmentUsageAppService
    {
        private readonly EquipmentUsageRecordManager _usageRecordManager;
        private readonly ILogger<EquipmentUsageAppService> _logger;

        public EquipmentUsageAppService(
            EquipmentUsageRecordManager usageRecordManager,
            ILogger<EquipmentUsageAppService> logger)
        {
            _usageRecordManager = usageRecordManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建设备使用记录
        /// </summary>
        public async Task<EquipmentUsageRecordDto> CreateUsageRecordAsync(CreateEquipmentUsageRecordDto input)
        {
            try
            {
                _logger.LogInformation("开始创建设备使用记录，设备ID：{EquipmentId}", input.EquipmentId);
                var record = await _usageRecordManager.CreateUsageRecordAsync(
                    input.EquipmentId,
                    input.OperatorId,
                    input.OperatorName,
                    input.Purpose);
                _logger.LogInformation("设备使用记录创建成功，记录ID：{Id}", record.Id);
                return ObjectMapper.Map<EquipmentUsageRecord, EquipmentUsageRecordDto>(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建设备使用记录失败，设备ID：{EquipmentId}", input.EquipmentId);
                throw new UserFriendlyException("创建设备使用记录失败", ex);
            }
        }

        /// <summary>
        /// 完成设备使用记录
        /// </summary>
        public async Task<EquipmentUsageRecordDto> CompleteUsageRecordAsync(CompleteEquipmentUsageRecordDto input)
        {
            try
            {
                _logger.LogInformation("完成设备使用记录，记录ID：{Id}", input.UsageRecordId);
                var record = await _usageRecordManager.CompleteUsageRecordAsync(
                    input.UsageRecordId,
                    input.EndTime,
                    input.Remarks);
                _logger.LogInformation("设备使用记录完成成功，记录ID：{Id}", record.Id);
                return ObjectMapper.Map<EquipmentUsageRecord, EquipmentUsageRecordDto>(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "完成设备使用记录失败，记录ID：{Id}", input.UsageRecordId);
                throw new UserFriendlyException("完成设备使用记录失败", ex);
            }
        }

        /// <summary>
        /// 获取设备使用记录列表
        /// </summary>
        public async Task<List<EquipmentUsageRecordDto>> GetUsageRecordsAsync(Guid equipmentId)
        {
            try
            {
                _logger.LogInformation("获取设备使用记录列表，设备ID：{EquipmentId}", equipmentId);
                var records = await _usageRecordManager.GetUsageRecordsAsync(equipmentId);
                return ObjectMapper.Map<List<EquipmentUsageRecord>, List<EquipmentUsageRecordDto>>(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取设备使用记录列表失败，设备ID：{EquipmentId}", equipmentId);
                throw new UserFriendlyException("获取设备使用记录列表失败", ex);
            }
        }
    }
}