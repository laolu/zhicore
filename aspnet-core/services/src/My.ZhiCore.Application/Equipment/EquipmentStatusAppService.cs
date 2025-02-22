using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备状态应用服务，用于管理设备的运行状态和性能指标
    /// </summary>
    public class EquipmentStatusAppService : ApplicationService
    {
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly PredictiveMaintenanceManager _predictiveMaintenanceManager;
        private readonly ILogger<EquipmentStatusAppService> _logger;

        public EquipmentStatusAppService(
            IRepository<Equipment, Guid> equipmentRepository,
            PredictiveMaintenanceManager predictiveMaintenanceManager,
            ILogger<EquipmentStatusAppService> logger)
        {
            _equipmentRepository = equipmentRepository;
            _predictiveMaintenanceManager = predictiveMaintenanceManager;
            _logger = logger;
        }

        /// <summary>
        /// 更新设备运行状态
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="status">新的运行状态</param>
        public async Task UpdateStatusAsync(Guid equipmentId, EquipmentStatus status)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            equipment.UpdateStatus(status);
            await _equipmentRepository.UpdateAsync(equipment);
            _logger.LogInformation($"设备 {equipment.Name} (ID: {equipmentId}) 状态已更新为 {status}");
        }

        /// <summary>
        /// 记录设备性能指标
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="temperature">温度</param>
        /// <param name="vibration">振动</param>
        /// <param name="noiseLevel">噪音级别</param>
        /// <param name="powerConsumption">功耗</param>
        /// <param name="runningSpeed">运行速度</param>
        public async Task RecordPerformanceMetricsAsync(
            Guid equipmentId,
            decimal temperature,
            decimal vibration,
            decimal noiseLevel,
            decimal powerConsumption,
            decimal runningSpeed)
        {
            await _predictiveMaintenanceManager.RecordAndAnalyzeConditionAsync(
                equipmentId,
                temperature,
                vibration,
                noiseLevel,
                powerConsumption,
                runningSpeed);

            _logger.LogInformation($"设备 {equipmentId} 的性能指标已记录");
        }

        /// <summary>
        /// 获取设备当前状态
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        public async Task<EquipmentStatus> GetStatusAsync(Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            return equipment.Status;
        }

        /// <summary>
        /// 获取设备健康评分
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        public async Task<decimal> GetHealthScoreAsync(Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            var latestMetrics = await _predictiveMaintenanceManager.GetLatestMetricsAsync(equipmentId);
            return latestMetrics?.HealthScore ?? 100m;
        }
    }
}