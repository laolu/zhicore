using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;
using My.ZhiCore.Equipment.Enums;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 预测性维护管理器，负责处理设备预测性维护相关的领域逻辑
    /// </summary>
    public class PredictiveMaintenanceManager : DomainService
    {
        private readonly IRepository<PredictiveMaintenance, Guid> _predictiveMaintenanceRepository;
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly ILocalEventBus _localEventBus;

        public PredictiveMaintenanceManager(
            IRepository<PredictiveMaintenance, Guid> predictiveMaintenanceRepository,
            IRepository<Equipment, Guid> equipmentRepository,
            ILocalEventBus localEventBus)
        {
            _predictiveMaintenanceRepository = predictiveMaintenanceRepository;
            _equipmentRepository = equipmentRepository;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建预测性维护记录
        /// </summary>
        public async Task<PredictiveMaintenance> CreateAsync(
            Guid equipmentId,
            decimal temperature,
            decimal vibration,
            decimal noiseLevel,
            decimal powerConsumption,
            decimal runningSpeed,
            decimal totalRunningHours)
        {
            // 验证设备是否存在
            var equipment = await _equipmentRepository.GetAsync(equipmentId);

            // 分析设备状态并计算相关指标
            var (failureProbability, predictedRemainingLife) = AnalyzeEquipmentCondition(
                temperature, vibration, noiseLevel, powerConsumption, runningSpeed);
            
            var healthScore = CalculateHealthScore(
                temperature, vibration, noiseLevel, powerConsumption, runningSpeed);
            
            var alertLevel = DetermineAlertLevel(healthScore, failureProbability);
            
            var maintenanceRecommendation = GenerateMaintenanceRecommendation(
                healthScore, failureProbability, predictedRemainingLife);
            
            var failureMode = IdentifyFailureMode(
                temperature, vibration, noiseLevel, powerConsumption);

            var maintenance = new PredictiveMaintenance(
                GuidGenerator.Create(),
                equipmentId,
                DateTime.Now,
                temperature,
                vibration,
                noiseLevel,
                powerConsumption,
                runningSpeed,
                failureProbability,
                predictedRemainingLife,
                healthScore,
                maintenanceRecommendation,
                alertLevel,
                totalRunningHours,
                CalculateOverallEffectiveness(equipment, healthScore),
                failureMode,
                "1.0.0" // 当前预测模型版本
            );

            await _predictiveMaintenanceRepository.InsertAsync(maintenance);

            // 如果预警级别较高，发布预警事件
            if (alertLevel >= PredictiveMaintenanceAlertLevel.Warning)
            {
                await _localEventBus.PublishAsync(new PredictiveMaintenanceAlertEventData(
                    maintenance.Id,
                    maintenance.EquipmentId,
                    alertLevel,
                    failureProbability,
                    predictedRemainingLife,
                    maintenanceRecommendation
                ));
            }

            return maintenance;
        }

        private (decimal failureProbability, decimal remainingLife) AnalyzeEquipmentCondition(
            decimal temperature,
            decimal vibration,
            decimal noiseLevel,
            decimal powerConsumption,
            decimal runningSpeed)
        {
            // 使用设备参数计算故障概率和剩余寿命
            // 这里使用简化的计算逻辑，实际应用中可能需要更复杂的预测模型
            var failureProbability = CalculateFailureProbability(
                temperature, vibration, noiseLevel, powerConsumption, runningSpeed);
            
            var remainingLife = CalculateRemainingLife(failureProbability);
            
            return (failureProbability, remainingLife);
        }

        private decimal CalculateFailureProbability(
            decimal temperature,
            decimal vibration,
            decimal noiseLevel,
            decimal powerConsumption,
            decimal runningSpeed)
        {
            // 简化的故障概率计算逻辑
            var probability = 0m;
            
            // 温度影响
            if (temperature > 80) probability += 20;
            else if (temperature > 60) probability += 10;
            
            // 振动影响
            if (vibration > 10) probability += 30;
            else if (vibration > 5) probability += 15;
            
            // 噪音影响
            if (noiseLevel > 90) probability += 20;
            else if (noiseLevel > 75) probability += 10;
            
            // 功率消耗影响
            if (powerConsumption > 90) probability += 15;
            else if (powerConsumption > 75) probability += 7.5m;
            
            // 运行速度影响
            if (runningSpeed < 50 || runningSpeed > 150) probability += 15;
            
            return Math.Min(probability, 100);
        }

        private decimal CalculateRemainingLife(decimal failureProbability)
        {
            // 基于故障概率估算剩余使用寿命（小时）
            return Math.Max(0, (100 - failureProbability) * 100);
        }

        private decimal CalculateHealthScore(
            decimal temperature,
            decimal vibration,
            decimal noiseLevel,
            decimal powerConsumption,
            decimal runningSpeed)
        {
            // 计算设备健康评分（0-100）
            var score = 100m;
            
            // 根据各项参数扣分
            if (temperature > 60) score -= (temperature - 60) / 2;
            if (vibration > 5) score -= (vibration - 5) * 3;
            if (noiseLevel > 75) score -= (noiseLevel - 75) / 3;
            if (powerConsumption > 75) score -= (powerConsumption - 75) / 2;
            if (Math.Abs(runningSpeed - 100) > 20) score -= Math.Abs(runningSpeed - 100) / 4;
            
            return Math.Max(0, score);
        }

        private PredictiveMaintenanceAlertLevel DetermineAlertLevel(
            decimal healthScore,
            decimal failureProbability)
        {
            if (healthScore < 50 || failureProbability > 70)
                return PredictiveMaintenanceAlertLevel.Danger;
            if (healthScore < 70 || failureProbability > 50)
                return PredictiveMaintenanceAlertLevel.Warning;
            if (healthScore < 85 || failureProbability > 30)
                return PredictiveMaintenanceAlertLevel.Attention;
            return PredictiveMaintenanceAlertLevel.Normal;
        }

        private string GenerateMaintenanceRecommendation(
            decimal healthScore,
            decimal failureProbability,
            decimal predictedRemainingLife)
        {
            var recommendation = "维护建议:\n";

            if (healthScore < 60)
            {
                recommendation += "1. 建议立即进行全面检修\n";
            }
            else if (healthScore < 80)
            {
                recommendation += "1. 建议在近期安排检修\n";
            }

            if (failureProbability > 50)
            {
                recommendation += $"2. 故障风险较高，预计剩余使用时间{predictedRemainingLife}小时，建议提前准备备件\n";
            }

            if (string.IsNullOrEmpty(recommendation))
            {
                recommendation += "设备运行状况良好，建议按计划进行日常维护。";
            }

            return recommendation;
        }

        private string IdentifyFailureMode(
            decimal temperature,
            decimal vibration,
            decimal noiseLevel,
            decimal powerConsumption)
        {
            var failureModes = "可能的故障模式:\n";

            if (temperature > 80)
                failureModes += "- 过热故障\n";
            if (vibration > 10)
                failureModes += "- 轴承故障或不平衡\n";
            if (noiseLevel > 90)
                failureModes += "- 机械磨损或松动\n";
            if (powerConsumption > 90)
                failureModes += "- 电机负载过大\n";

            return string.IsNullOrEmpty(failureModes) ? "未发现明显故障模式" : failureModes;
        }

        private decimal CalculateOverallEffectiveness(Equipment equipment, decimal healthScore)
        {
            // 简化的OEE计算
            // 实际OEE计算应考虑可用性、性能和质量三个因素
            return healthScore;
        }
    }

    /// <summary>
    /// 预测性维护预警事件数据
    /// </summary>
    public class PredictiveMaintenanceAlertEventData
    {
        public Guid MaintenanceId { get; }
        public Guid EquipmentId { get; }
        public PredictiveMaintenanceAlertLevel AlertLevel { get; }
        public decimal FailureProbability { get; }
        public decimal PredictedRemainingLife { get; }
        public string MaintenanceRecommendation { get; }

        public PredictiveMaintenanceAlertEventData(
            Guid maintenanceId,
            Guid equipmentId,
            PredictiveMaintenanceAlertLevel alertLevel,
            decimal failureProbability,
            decimal predictedRemainingLife,
            string maintenanceRecommendation)
        {
            MaintenanceId = maintenanceId;
            EquipmentId = equipmentId;
            AlertLevel = alertLevel;
            FailureProbability = failureProbability;
            PredictedRemainingLife = predictedRemainingLife;
            MaintenanceRecommendation = maintenanceRecommendation;
        }
    }
}