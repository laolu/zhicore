using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备成本分析应用服务，用于分析设备的运营成本和维护成本
    /// </summary>
    public class EquipmentCostAppService : ApplicationService
    {
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly QualityCostAnalysisManager _qualityCostAnalysisManager;
        private readonly ILogger<EquipmentCostAppService> _logger;

        public EquipmentCostAppService(
            IRepository<Equipment, Guid> equipmentRepository,
            QualityCostAnalysisManager qualityCostAnalysisManager,
            ILogger<EquipmentCostAppService> logger)
        {
            _equipmentRepository = equipmentRepository;
            _qualityCostAnalysisManager = qualityCostAnalysisManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取设备运营成本分析
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public async Task<OperatingCostAnalysis> GetOperatingCostAnalysisAsync(
            Guid equipmentId,
            DateTime startTime,
            DateTime endTime)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            return await _qualityCostAnalysisManager.AnalyzeOperatingCostAsync(
                equipmentId, startTime, endTime);
        }

        /// <summary>
        /// 获取设备维护成本分析
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public async Task<MaintenanceCostAnalysis> GetMaintenanceCostAnalysisAsync(
            Guid equipmentId,
            DateTime startTime,
            DateTime endTime)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            return await _qualityCostAnalysisManager.AnalyzeMaintenanceCostAsync(
                equipmentId, startTime, endTime);
        }

        /// <summary>
        /// 获取设备质量成本分析
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public async Task<QualityCostAnalysis> GetQualityCostAnalysisAsync(
            Guid equipmentId,
            DateTime startTime,
            DateTime endTime)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            return await _qualityCostAnalysisManager.AnalyzeQualityCostAsync(
                equipmentId, startTime, endTime);
        }

        /// <summary>
        /// 获取设备总成本分析
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public async Task<TotalCostAnalysis> GetTotalCostAnalysisAsync(
            Guid equipmentId,
            DateTime startTime,
            DateTime endTime)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            return await _qualityCostAnalysisManager.AnalyzeTotalCostAsync(
                equipmentId, startTime, endTime);
        }

        /// <summary>
        /// 获取成本优化建议
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        public async Task<CostOptimizationSuggestions> GetCostOptimizationSuggestionsAsync(
            Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            return await _qualityCostAnalysisManager.GenerateCostOptimizationSuggestionsAsync(equipmentId);
        }
    }
}