using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备预测性维护实体类，用于记录和分析设备运行数据，预测可能的故障
    /// </summary>
    /// <remarks>
    /// 该类提供以下功能：
    /// - 记录设备运行数据（温度、振动、噪音等）
    /// - 分析设备性能趋势
    /// - 预测可能的故障
    /// - 生成维护建议
    /// - 统计运行时间和效率
    /// - 识别故障模式
    /// - 管理预测模型版本
    /// </remarks>
    public class PredictiveMaintenance : Entity<Guid>
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; private set; }

        /// <summary>
        /// 数据采集时间
        /// </summary>
        public DateTime CollectionTime { get; private set; }

        /// <summary>
        /// 设备温度（摄氏度）
        /// </summary>
        public decimal Temperature { get; private set; }

        /// <summary>
        /// 设备振动值（mm/s）
        /// </summary>
        public decimal Vibration { get; private set; }

        /// <summary>
        /// 设备噪音值（dB）
        /// </summary>
        public decimal NoiseLevel { get; private set; }

        /// <summary>
        /// 设备功率（kW）
        /// </summary>
        public decimal PowerConsumption { get; private set; }

        /// <summary>
        /// 运行速度（RPM）
        /// </summary>
        public decimal RunningSpeed { get; private set; }

        /// <summary>
        /// 预测的故障概率（0-100%）
        /// </summary>
        public decimal FailureProbability { get; private set; }

        /// <summary>
        /// 预测的剩余使用寿命（小时）
        /// </summary>
        public decimal PredictedRemainingLife { get; private set; }

        /// <summary>
        /// 健康状况评分（0-100）
        /// </summary>
        public decimal HealthScore { get; private set; }

        /// <summary>
        /// 维护建议
        /// </summary>
        public string MaintenanceRecommendation { get; private set; }

        /// <summary>
        /// 预警级别（正常/注意/警告/危险）
        /// </summary>
        public PredictiveMaintenanceAlertLevel AlertLevel { get; private set; }

        /// <summary>
        /// 累计运行时间（小时）
        /// </summary>
        public decimal TotalRunningHours { get; private set; }

        /// <summary>
        /// 设备综合效率（OEE - Overall Equipment Effectiveness）（0-100%）
        /// </summary>
        public decimal OverallEffectiveness { get; private set; }

        /// <summary>
        /// 识别到的故障模式
        /// </summary>
        public string FailureMode { get; private set; }

        /// <summary>
        /// 预测模型版本
        /// </summary>
        public string PredictionModelVersion { get; private set; }

        /// <summary>
        /// 预测准确度（0-100%）
        /// </summary>
        public decimal PredictionAccuracy { get; private set; }

        /// <summary>
        /// 上次预测时间
        /// </summary>
        public DateTime LastPredictionTime { get; private set; }

        /// <summary>
        /// 性能指标趋势（JSON格式存储历史数据）
        /// </summary>
        public string PerformanceTrend { get; private set; }

        /// <summary>
        /// 故障概率变化趋势（JSON格式存储历史数据）
        /// </summary>
        public string FailureProbabilityTrend { get; private set; }

        /// <summary>
        /// 维护建议历史（JSON格式存储历史建议）
        /// </summary>
        public string MaintenanceRecommendationHistory { get; private set; }

        /// <summary>
        /// 温度阈值上限（摄氏度）
        /// </summary>
        public decimal TemperatureThresholdHigh { get; private set; }

        /// <summary>
        /// 温度阈值下限（摄氏度）
        /// </summary>
        public decimal TemperatureThresholdLow { get; private set; }

        /// <summary>
        /// 振动阈值（mm/s）
        /// </summary>
        public decimal VibrationThreshold { get; private set; }

        /// <summary>
        /// 噪音阈值（dB）
        /// </summary>
        public decimal NoiseLevelThreshold { get; private set; }

        /// <summary>
        /// 功率阈值（kW）
        /// </summary>
        public decimal PowerConsumptionThreshold { get; private set; }

        /// <summary>
        /// 运行速度阈值（RPM）
        /// </summary>
        public decimal RunningSpeedThreshold { get; private set; }

        protected PredictiveMaintenance()
        {
        }

        public PredictiveMaintenance(
            Guid id,
            Guid equipmentId,
            DateTime collectionTime,
            decimal temperature,
            decimal vibration,
            decimal noiseLevel,
            decimal powerConsumption,
            decimal runningSpeed,
            decimal failureProbability,
            decimal predictedRemainingLife,
            decimal healthScore,
            string maintenanceRecommendation,
            PredictiveMaintenanceAlertLevel alertLevel,
            decimal totalRunningHours,
            decimal overallEffectiveness,
            string failureMode,
            string predictionModelVersion,
            decimal predictionAccuracy,
            DateTime lastPredictionTime,
            string performanceTrend = null,
            string failureProbabilityTrend = null,
            string maintenanceRecommendationHistory = null,
            decimal temperatureThresholdHigh = 0,
            decimal temperatureThresholdLow = 0,
            decimal vibrationThreshold = 0,
            decimal noiseLevelThreshold = 0,
            decimal powerConsumptionThreshold = 0,
            decimal runningSpeedThreshold = 0)
        {
            Id = id;
            EquipmentId = equipmentId;
            CollectionTime = collectionTime;
            Temperature = temperature;
            Vibration = vibration;
            NoiseLevel = noiseLevel;
            PowerConsumption = powerConsumption;
            RunningSpeed = runningSpeed;
            FailureProbability = failureProbability;
            PredictedRemainingLife = predictedRemainingLife;
            HealthScore = healthScore;
            MaintenanceRecommendation = maintenanceRecommendation;
            AlertLevel = alertLevel;
            TotalRunningHours = totalRunningHours;
            OverallEffectiveness = overallEffectiveness;
            FailureMode = failureMode;
            PredictionModelVersion = predictionModelVersion;
            PredictionAccuracy = predictionAccuracy;
            LastPredictionTime = lastPredictionTime;
            PerformanceTrend = performanceTrend;
            FailureProbabilityTrend = failureProbabilityTrend;
            MaintenanceRecommendationHistory = maintenanceRecommendationHistory;
            TemperatureThresholdHigh = temperatureThresholdHigh;
            TemperatureThresholdLow = temperatureThresholdLow;
            VibrationThreshold = vibrationThreshold;
            NoiseLevelThreshold = noiseLevelThreshold;
            PowerConsumptionThreshold = powerConsumptionThreshold;
            RunningSpeedThreshold = runningSpeedThreshold;
        }
    }
}