using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备预测性维护应用服务，用于管理设备的预测性维护和健康状态
    /// </summary>
    public class PredictiveMaintenanceAppService : ApplicationService
    {
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly PredictiveMaintenanceManager _predictiveMaintenanceManager;
        private readonly ILogger<PredictiveMaintenanceAppService> _logger;

        public PredictiveMaintenanceAppService(
            IRepository<Equipment, Guid> equipmentRepository,
            PredictiveMaintenanceManager predictiveMaintenanceManager,
            ILogger<PredictiveMaintenanceAppService> logger)
        {
            _equipmentRepository = equipmentRepository;
            _predictiveMaintenanceManager = predictiveMaintenanceManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取设备健康状态
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        public async Task<EquipmentHealthStatus> GetHealthStatusAsync(Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            var metrics = await _predictiveMaintenanceManager.GetLatestMetricsAsync(equipmentId);
            return await _predictiveMaintenanceManager.CalculateHealthStatusAsync(equipment, metrics);
        }

        /// <summary>
        /// 获取设备维护建议
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        public async Task<List<MaintenanceSuggestion>> GetMaintenanceSuggestionsAsync(Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            var metrics = await _predictiveMaintenanceManager.GetLatestMetricsAsync(equipmentId);
            return await _predictiveMaintenanceManager.GenerateMaintenanceSuggestionsAsync(equipment, metrics);
        }

        /// <summary>
        /// 更新设备维护计划
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="maintenanceItems">维护项目列表</param>
        public async Task UpdateMaintenancePlanAsync(
            Guid equipmentId,
            List<MaintenanceItem> maintenanceItems)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            await _predictiveMaintenanceManager.UpdateMaintenancePlanAsync(equipment, maintenanceItems);
            _logger.LogInformation($"已更新设备 {equipment.Name} 的维护计划");
        }

        /// <summary>
        /// 获取设备性能趋势
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public async Task<PerformanceTrend> GetPerformanceTrendAsync(
            Guid equipmentId,
            DateTime startTime,
            DateTime endTime)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            var metrics = await _predictiveMaintenanceManager.GetMetricsInRangeAsync(
                equipmentId, startTime, endTime);
            return await _predictiveMaintenanceManager.AnalyzePerformanceTrendAsync(equipment, metrics);
        }

        /// <summary>
        /// 记录设备维护执行情况
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="maintenanceResult">维护结果</param>
        public async Task RecordMaintenanceExecutionAsync(
            Guid equipmentId,
            MaintenanceResult maintenanceResult)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            await _predictiveMaintenanceManager.RecordMaintenanceExecutionAsync(equipment, maintenanceResult);
            _logger.LogInformation($"已记录设备 {equipment.Name} 的维护执行情况");
        }
    }
}