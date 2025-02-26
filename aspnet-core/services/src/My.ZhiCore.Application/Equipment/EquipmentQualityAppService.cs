using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备质量分析应用服务，用于分析设备的质量指标和趋势
    /// </summary>
    public class EquipmentQualityAppService : ApplicationService
    {
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly QualityStatisticsManager _qualityStatisticsManager;
        private readonly ILogger<EquipmentQualityAppService> _logger;

        public EquipmentQualityAppService(
            IRepository<Equipment, Guid> equipmentRepository,
            QualityStatisticsManager qualityStatisticsManager,
            ILogger<EquipmentQualityAppService> logger)
        {
            _equipmentRepository = equipmentRepository;
            _qualityStatisticsManager = qualityStatisticsManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取设备质量指标
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public async Task<QualityMetrics> GetQualityMetricsAsync(
            Guid equipmentId,
            DateTime startTime,
            DateTime endTime)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            return await _qualityStatisticsManager.GetQualityMetricsAsync(
                equipmentId, startTime, endTime);
        }

        /// <summary>
        /// 获取设备不良品分析
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public async Task<DefectAnalysis> GetDefectAnalysisAsync(
            Guid equipmentId,
            DateTime startTime,
            DateTime endTime)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            return await _qualityStatisticsManager.AnalyzeDefectsAsync(
                equipmentId, startTime, endTime);
        }

        /// <summary>
        /// 获取质量趋势分析
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public async Task<QualityTrend> GetQualityTrendAsync(
            Guid equipmentId,
            DateTime startTime,
            DateTime endTime)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            return await _qualityStatisticsManager.AnalyzeQualityTrendAsync(
                equipmentId, startTime, endTime);
        }

        /// <summary>
        /// 记录质量检测结果
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="inspectionResult">检测结果</param>
        public async Task RecordQualityInspectionAsync(
            Guid equipmentId,
            QualityInspectionResult inspectionResult)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            await _qualityStatisticsManager.RecordInspectionResultAsync(equipment, inspectionResult);
            _logger.LogInformation($"已记录设备 {equipment.Name} 的质量检测结果");
        }

        /// <summary>
        /// 获取质量改进建议
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        public async Task<List<QualityImprovementSuggestion>> GetQualityImprovementSuggestionsAsync(
            Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            var metrics = await _qualityStatisticsManager.GetLatestMetricsAsync(equipmentId);
            return await _qualityStatisticsManager.GenerateImprovementSuggestionsAsync(equipment, metrics);
        }
    }
}