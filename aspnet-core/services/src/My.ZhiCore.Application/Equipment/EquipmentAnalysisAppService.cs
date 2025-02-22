using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备分析应用服务，用于分析设备的运行效率和故障趋势
    /// </summary>
    public class EquipmentAnalysisAppService : ApplicationService
    {
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly PredictiveMaintenanceManager _predictiveMaintenanceManager;
        private readonly QualityStatisticsManager _qualityStatisticsManager;
        private readonly ILogger<EquipmentAnalysisAppService> _logger;

        public EquipmentAnalysisAppService(
            IRepository<Equipment, Guid> equipmentRepository,
            PredictiveMaintenanceManager predictiveMaintenanceManager,
            QualityStatisticsManager qualityStatisticsManager,
            ILogger<EquipmentAnalysisAppService> logger)
        {
            _equipmentRepository = equipmentRepository;
            _predictiveMaintenanceManager = predictiveMaintenanceManager;
            _qualityStatisticsManager = qualityStatisticsManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取设备运行效率分析
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public async Task<EquipmentEfficiencyAnalysis> GetEfficiencyAnalysisAsync(
            Guid equipmentId,
            DateTime startTime,
            DateTime endTime)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            var metrics = await _predictiveMaintenanceManager.GetMetricsInRangeAsync(
                equipmentId, startTime, endTime);
            var qualityStats = await _qualityStatisticsManager.GetStatisticsAsync(
                equipmentId, startTime, endTime);

            return new EquipmentEfficiencyAnalysis
            {
                EquipmentId = equipmentId,
                EquipmentName = equipment.Name,
                StartTime = startTime,
                EndTime = endTime,
                RunningTime = CalculateRunningTime(metrics),
                DownTime = CalculateDownTime(metrics),
                MaintenanceTime = CalculateMaintenanceTime(metrics),
                AverageEfficiency = CalculateAverageEfficiency(metrics),
                QualityRate = qualityStats.QualityRate,
                OEE = CalculateOEE(metrics, qualityStats)
            };
        }

        /// <summary>
        /// 获取设备故障趋势分析
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public async Task<EquipmentFailureTrendAnalysis> GetFailureTrendAnalysisAsync(
            Guid equipmentId,
            DateTime startTime,
            DateTime endTime)
        {
            var metrics = await _predictiveMaintenanceManager.GetMetricsInRangeAsync(
                equipmentId, startTime, endTime);
            var alarms = await Repository.GetListAsync<EquipmentAlarm>(
                a => a.EquipmentId == equipmentId &&
                     a.CreationTime >= startTime &&
                     a.CreationTime <= endTime);

            return new EquipmentFailureTrendAnalysis
            {
                EquipmentId = equipmentId,
                StartTime = startTime,
                EndTime = endTime,
                FailureFrequency = CalculateFailureFrequency(alarms),
                MTBF = CalculateMTBF(alarms, metrics),
                MTTR = CalculateMTTR(alarms),
                FailurePatterns = AnalyzeFailurePatterns(alarms),
                RiskLevel = CalculateRiskLevel(metrics, alarms),
                PredictedNextFailure = PredictNextFailure(metrics, alarms)
            };
        }

        /// <summary>
        /// 获取设备性能趋势分析
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public async Task<EquipmentPerformanceTrendAnalysis> GetPerformanceTrendAnalysisAsync(
            Guid equipmentId,
            DateTime startTime,
            DateTime endTime)
        {
            var metrics = await _predictiveMaintenanceManager.GetMetricsInRangeAsync(
                equipmentId, startTime, endTime);

            return new EquipmentPerformanceTrendAnalysis
            {
                EquipmentId = equipmentId,
                StartTime = startTime,
                EndTime = endTime,
                TemperatureTrend = AnalyzeTemperatureTrend(metrics),
                VibrationTrend = AnalyzeVibrationTrend(metrics),
                NoiseLevelTrend = AnalyzeNoiseLevelTrend(metrics),
                PowerConsumptionTrend = AnalyzePowerConsumptionTrend(metrics),
                PerformanceDegradation = CalculatePerformanceDegradation(metrics),
                OptimizationSuggestions = GenerateOptimizationSuggestions(metrics)
            };
        }

        #region 私有辅助方法

        private TimeSpan CalculateRunningTime(IEnumerable<PredictiveMaintenance> metrics)
        {
            // 计算设备运行时间
            return TimeSpan.FromHours(metrics.Count(m => m.IsRunning) * metrics.First().SamplingInterval.TotalHours);
        }

        private TimeSpan CalculateDownTime(IEnumerable<PredictiveMaintenance> metrics)
        {
            // 计算设备停机时间
            return TimeSpan.FromHours(metrics.Count(m => m.Status == EquipmentStatus.Fault) * metrics.First().SamplingInterval.TotalHours);
        }

        private TimeSpan CalculateMaintenanceTime(IEnumerable<PredictiveMaintenance> metrics)
        {
            // 计算设备维护时间
            return TimeSpan.FromHours(metrics.Count(m => m.Status == EquipmentStatus.Maintenance) * metrics.First().SamplingInterval.TotalHours);
        }

        private decimal CalculateAverageEfficiency(IEnumerable<PredictiveMaintenance> metrics)
        {
            // 计算平均效率
            return metrics.Where(m => m.IsRunning).Average(m => m.EfficiencyScore);
        }

        private decimal CalculateOEE(IEnumerable<PredictiveMaintenance> metrics, QualityStatistics qualityStats)
        {
            // 计算设备综合效率（OEE = 可用性 × 性能效率 × 质量率）
            var availability = CalculateAvailability(metrics);
            var performance = CalculatePerformanceEfficiency(metrics);
            var quality = qualityStats.QualityRate;

            return availability * performance * quality;
        }

        private decimal CalculateFailureFrequency(IEnumerable<EquipmentAlarm> alarms)
        {
            // 计算故障频率
            return alarms.Count(a => a.Level >= AlarmLevel.Critical);
        }

        private TimeSpan CalculateMTBF(IEnumerable<EquipmentAlarm> alarms, IEnumerable<PredictiveMaintenance> metrics)
        {
            // 计算平均故障间隔时间
            var totalRunningTime = CalculateRunningTime(metrics);
            var failureCount = CalculateFailureFrequency(alarms);
            return failureCount > 0 ? TimeSpan.FromHours(totalRunningTime.TotalHours / failureCount) : TimeSpan.MaxValue;
        }

        private TimeSpan CalculateMTTR(IEnumerable<EquipmentAlarm> alarms)
        {
            // 计算平均修复时间
            var handledAlarms = alarms.Where(a => a.IsHandled && a.Level >= AlarmLevel.Critical);
            return handledAlarms.Any()
                ? TimeSpan.FromHours(handledAlarms.Average(a => (a.HandlingTime - a.CreationTime).TotalHours))
                : TimeSpan.Zero;
        }

        private List<FailurePattern> AnalyzeFailurePatterns(IEnumerable<EquipmentAlarm> alarms)
        {
            // 分析故障模式
            return alarms
                .Where(a => a.Level >= AlarmLevel.Critical)
                .GroupBy(a => a.Type)
                .Select(g => new FailurePattern
                {
                    Type = g.Key,
                    Frequency = g.Count(),
                    AverageRepairTime = TimeSpan.FromHours(g.Average(a => (a.HandlingTime - a.CreationTime).TotalHours))
                })
                .ToList();
        }

        private RiskLevel CalculateRiskLevel(IEnumerable<PredictiveMaintenance> metrics, IEnumerable<EquipmentAlarm> alarms)
        {
            // 计算风险等级
            var healthScore = metrics.OrderByDescending(m => m.CreationTime).First().HealthScore;
            var recentAlarms = alarms.Where(a => a.CreationTime >= DateTime.Now.AddDays(-7));
            var criticalAlarmCount = recentAlarms.Count(a => a.Level >= AlarmLevel.Critical);

            if (healthScore < 60 || criticalAlarmCount >= 3)
                return RiskLevel.High;
            if (healthScore < 80 || criticalAlarmCount >= 1)
                return RiskLevel.Medium;
            return RiskLevel.Low;
        }

        private DateTime? PredictNextFailure(IEnumerable<PredictiveMaintenance> metrics, IEnumerable<EquipmentAlarm> alarms)
        {
            // 预测下次故障时间
            var latestMetrics = metrics.OrderByDescending(m => m.CreationTime).First();
            var degradationRate = CalculatePerformanceDegradation(metrics);
            var mtbf = CalculateMTBF(alarms, metrics);

            if (latestMetrics.HealthScore > 90 || degradationRate <= 0)
                return null;

            var estimatedTimeToFailure = TimeSpan.FromHours(
                (latestMetrics.HealthScore - 60) / (degradationRate * 24));

            return DateTime.Now.Add(estimatedTimeToFailure);
        }

        private decimal CalculatePerformanceDegradation(IEnumerable<PredictiveMaintenance> metrics)
        {
            // 计算性能退化率（每天）
            var orderedMetrics = metrics.OrderBy(m => m.CreationTime).ToList();
            if (orderedMetrics.Count < 2)
                return 0;

            var firstMetric = orderedMetrics.First();
            var lastMetric = orderedMetrics.Last();
            var daysDifference = (lastMetric.CreationTime - firstMetric.CreationTime).TotalDays;

            if (daysDifference <= 0)
                return 0;

            return (firstMetric.HealthScore - lastMetric.HealthScore) / (decimal)daysDifference;
        }

        private List<string> GenerateOptimizationSuggestions(IEnumerable<PredictiveMaintenance> metrics)
        {
            // 生成优化建议
            var suggestions = new List<string>();
            var latestMetrics = metrics.OrderByDescending(m => m.CreationTime).First();

            if (latestMetrics.Temperature > latestMetrics.TemperatureThreshold * 0.9m)
                suggestions.Add("建议检查设备散热系统，当前温度接近阈值");

            if (latestMetrics.Vibration > latestMetrics.VibrationThreshold * 0.9m)
                suggestions.Add("建议检查设备平衡性和紧固件，当前振动接近阈值");

            if (latestMetrics.PowerConsumption > latestMetrics.PowerConsumptionThreshold * 0.9m)
                suggestions.Add("建议优化设备运行参数，当前功耗接近阈值");

            return suggestions;
        }

        #endregion
    }
}