using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备预测性维护服务接口
    /// </summary>
    public interface IPredictiveMaintenanceAppService : IApplicationService
    {
        /// <summary>
        /// 获取设备健康状态评估
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <returns>设备健康状态评估数据</returns>
        Task<EquipmentHealthAssessmentDto> GetHealthAssessmentAsync(Guid equipmentId);

        /// <summary>
        /// 获取预测性维护建议
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <returns>维护建议数据</returns>
        Task<MaintenanceRecommendationDto> GetMaintenanceRecommendationAsync(Guid equipmentId);

        /// <summary>
        /// 获取剩余使用寿命预测
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <returns>剩余使用寿命预测数据</returns>
        Task<RemainingLifePredictionDto> GetRemainingLifePredictionAsync(Guid equipmentId);

        /// <summary>
        /// 获取故障预警信息
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <returns>故障预警信息</returns>
        Task<FailureWarningDto> GetFailureWarningAsync(Guid equipmentId);
    }

    /// <summary>
    /// 设备健康状态评估DTO
    /// </summary>
    public class EquipmentHealthAssessmentDto
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 健康指数
        /// </summary>
        public decimal HealthIndex { get; set; }

        /// <summary>
        /// 健康状态评级
        /// </summary>
        public string HealthStatus { get; set; }

        /// <summary>
        /// 关键指标列表
        /// </summary>
        public KeyIndicatorDto[] KeyIndicators { get; set; }
    }

    /// <summary>
    /// 关键指标DTO
    /// </summary>
    public class KeyIndicatorDto
    {
        /// <summary>
        /// 指标名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 指标值
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 正常范围
        /// </summary>
        public string NormalRange { get; set; }

        /// <summary>
        /// 状态评估
        /// </summary>
        public string Status { get; set; }
    }

    /// <summary>
    /// 维护建议DTO
    /// </summary>
    public class MaintenanceRecommendationDto
    {
        /// <summary>
        /// 建议维护时间
        /// </summary>
        public DateTime RecommendedMaintenanceTime { get; set; }

        /// <summary>
        /// 维护项目列表
        /// </summary>
        public MaintenanceItemDto[] MaintenanceItems { get; set; }

        /// <summary>
        /// 预计维护成本
        /// </summary>
        public decimal EstimatedCost { get; set; }

        /// <summary>
        /// 维护优先级
        /// </summary>
        public string Priority { get; set; }
    }

    /// <summary>
    /// 维护项目DTO
    /// </summary>
    public class MaintenanceItemDto
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 预计工时
        /// </summary>
        public decimal EstimatedHours { get; set; }

        /// <summary>
        /// 所需备件
        /// </summary>
        public string[] RequiredParts { get; set; }
    }

    /// <summary>
    /// 剩余使用寿命预测DTO
    /// </summary>
    public class RemainingLifePredictionDto
    {
        /// <summary>
        /// 预计剩余寿命（小时）
        /// </summary>
        public decimal RemainingHours { get; set; }

        /// <summary>
        /// 置信度
        /// </summary>
        public decimal Confidence { get; set; }

        /// <summary>
        /// 影响因素列表
        /// </summary>
        public string[] ImpactFactors { get; set; }

        /// <summary>
        /// 建议更换时间
        /// </summary>
        public DateTime RecommendedReplacementTime { get; set; }
    }

    /// <summary>
    /// 故障预警DTO
    /// </summary>
    public class FailureWarningDto
    {
        /// <summary>
        /// 预警等级
        /// </summary>
        public string WarningLevel { get; set; }

        /// <summary>
        /// 可能的故障类型
        /// </summary>
        public string[] PotentialFailureTypes { get; set; }

        /// <summary>
        /// 故障概率
        /// </summary>
        public decimal FailureProbability { get; set; }

        /// <summary>
        /// 预警描述
        /// </summary>
        public string WarningDescription { get; set; }

        /// <summary>
        /// 建议采取的措施
        /// </summary>
        public string[] RecommendedActions { get; set; }
    }
}