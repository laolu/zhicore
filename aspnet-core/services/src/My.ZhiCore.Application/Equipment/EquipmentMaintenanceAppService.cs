using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;
using My.ZhiCore.Equipment.Dtos;
using My.ZhiCore.Equipment.Enums;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备维护应用服务
    /// </summary>
    public class EquipmentMaintenanceAppService : ApplicationService, IEquipmentMaintenanceAppService
    {
        private readonly EquipmentManager _equipmentManager;
        private readonly MaintenanceRecordManager _maintenanceManager;
        private readonly ILogger<EquipmentMaintenanceAppService> _logger;

        public EquipmentMaintenanceAppService(
            EquipmentManager equipmentManager,
            MaintenanceRecordManager maintenanceManager,
            ILogger<EquipmentMaintenanceAppService> logger)
        {
            _equipmentManager = equipmentManager;
            _maintenanceManager = maintenanceManager;
            _logger = logger;
        }

        /// <summary>
        /// 开始设备维护
        /// </summary>
        public async Task<MaintenanceRecordDto> StartMaintenanceAsync(StartMaintenanceDto input)
        {
            try
            {
                _logger.LogInformation("开始设备维护，设备ID：{EquipmentId}", input.EquipmentId);
                var equipment = await _equipmentManager.GetAsync(input.EquipmentId);
                var record = await _maintenanceManager.CreateMaintenanceRecordAsync(
                    input.EquipmentId,
                    input.MaintenanceType,
                    input.Description);
                _logger.LogInformation("设备维护开始成功，记录ID：{Id}", record.Id);
                return ObjectMapper.Map<MaintenanceRecord, MaintenanceRecordDto>(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "开始设备维护失败，设备ID：{EquipmentId}", input.EquipmentId);
                throw new UserFriendlyException("开始设备维护失败", ex);
            }
        }

        /// <summary>
        /// 完成设备维护
        /// </summary>
        public async Task<MaintenanceRecordDto> CompleteMaintenanceAsync(CompleteMaintenanceDto input)
        {
            try
            {
                _logger.LogInformation("完成设备维护，记录ID：{Id}", input.MaintenanceRecordId);
                var record = await _maintenanceManager.CompleteMaintenanceRecordAsync(
                    input.MaintenanceRecordId,
                    input.MaintenanceResult,
                    input.Remarks);
                _logger.LogInformation("设备维护完成成功，记录ID：{Id}", record.Id);
                return ObjectMapper.Map<MaintenanceRecord, MaintenanceRecordDto>(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "完成设备维护失败，记录ID：{Id}", input.MaintenanceRecordId);
                throw new UserFriendlyException("完成设备维护失败", ex);
            }
        }

        /// <summary>
        /// 获取设备维护记录
        /// </summary>
        public async Task<List<MaintenanceRecordDto>> GetMaintenanceRecordsAsync(Guid equipmentId)
        {
            try
            {
                _logger.LogInformation("获取设备维护记录，设备ID：{EquipmentId}", equipmentId);
                var records = await _maintenanceManager.GetMaintenanceRecordsAsync(equipmentId);
                return ObjectMapper.Map<List<MaintenanceRecord>, List<MaintenanceRecordDto>>(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取设备维护记录失败，设备ID：{EquipmentId}", equipmentId);
                throw new UserFriendlyException("获取设备维护记录失败", ex);
            }
        }
    }
}