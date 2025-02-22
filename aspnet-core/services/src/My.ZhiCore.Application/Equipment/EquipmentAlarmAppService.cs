using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备报警应用服务，用于管理设备的报警信息和处理流程
    /// </summary>
    public class EquipmentAlarmAppService : ApplicationService
    {
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly PredictiveMaintenanceManager _predictiveMaintenanceManager;
        private readonly ILogger<EquipmentAlarmAppService> _logger;

        public EquipmentAlarmAppService(
            IRepository<Equipment, Guid> equipmentRepository,
            PredictiveMaintenanceManager predictiveMaintenanceManager,
            ILogger<EquipmentAlarmAppService> logger)
        {
            _equipmentRepository = equipmentRepository;
            _predictiveMaintenanceManager = predictiveMaintenanceManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建设备报警
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="alarmType">报警类型</param>
        /// <param name="alarmLevel">报警级别</param>
        /// <param name="description">报警描述</param>
        public async Task CreateAlarmAsync(
            Guid equipmentId,
            string alarmType,
            AlarmLevel alarmLevel,
            string description)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            var metrics = await _predictiveMaintenanceManager.GetLatestMetricsAsync(equipmentId);

            // 创建报警记录
            var alarm = new EquipmentAlarm(
                GuidGenerator.Create(),
                equipmentId,
                alarmType,
                alarmLevel,
                description,
                metrics);

            // 根据报警级别采取相应措施
            if (alarmLevel >= AlarmLevel.Critical)
            {
                equipment.UpdateStatus(EquipmentStatus.Fault);
                await _equipmentRepository.UpdateAsync(equipment);
                _logger.LogWarning($"设备 {equipment.Name} 因严重报警已停机");
            }

            _logger.LogInformation($"设备 {equipment.Name} 创建了新的{alarmLevel}级别报警");
        }

        /// <summary>
        /// 处理报警
        /// </summary>
        /// <param name="alarmId">报警ID</param>
        /// <param name="handlingResult">处理结果</param>
        /// <param name="remarks">备注</param>
        public async Task HandleAlarmAsync(
            Guid alarmId,
            string handlingResult,
            string remarks)
        {
            var alarm = await Repository.GetAsync<EquipmentAlarm>(alarmId);
            alarm.Handle(handlingResult, remarks);

            // 如果设备处于故障状态，且所有严重报警都已处理，则恢复设备状态
            var equipment = await _equipmentRepository.GetAsync(alarm.EquipmentId);
            if (equipment.Status == EquipmentStatus.Fault)
            {
                var hasUnhandledCriticalAlarms = await Repository
                    .AnyAsync<EquipmentAlarm>(a => 
                        a.EquipmentId == alarm.EquipmentId && 
                        a.Level >= AlarmLevel.Critical && 
                        !a.IsHandled);

                if (!hasUnhandledCriticalAlarms)
                {
                    equipment.UpdateStatus(EquipmentStatus.Standby);
                    await _equipmentRepository.UpdateAsync(equipment);
                    _logger.LogInformation($"设备 {equipment.Name} 的严重报警已处理完毕，状态已恢复");
                }
            }

            _logger.LogInformation($"报警 {alarmId} 已处理完成");
        }

        /// <summary>
        /// 获取设备未处理的报警列表
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        public async Task<List<EquipmentAlarm>> GetUnhandledAlarmsAsync(Guid equipmentId)
        {
            return await Repository.GetListAsync<EquipmentAlarm>(
                a => a.EquipmentId == equipmentId && !a.IsHandled);
        }

        /// <summary>
        /// 获取设备的报警历史
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public async Task<List<EquipmentAlarm>> GetAlarmHistoryAsync(
            Guid equipmentId,
            DateTime startTime,
            DateTime endTime)
        {
            return await Repository.GetListAsync<EquipmentAlarm>(
                a => a.EquipmentId == equipmentId &&
                     a.CreationTime >= startTime &&
                     a.CreationTime <= endTime);
        }
    }
}