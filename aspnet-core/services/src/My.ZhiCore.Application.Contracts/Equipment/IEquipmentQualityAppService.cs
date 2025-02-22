using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备质量服务接口
    /// </summary>
    public interface IEquipmentQualityAppService : IApplicationService
    {
        /// <summary>
        /// 获取质量检测结果
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>质量检测结果数据</returns>
        Task<QualityInspectionResultDto> GetInspectionResultAsync(Guid equipmentId, DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取缺陷分析
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>缺陷分析数据</returns>
        Task<DefectAnalysisDto> GetDefectAnalysisAsync(Guid equipmentId, DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取质量趋势分析
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>质量趋势分析数据</returns>
        Task<QualityTrendAnalysisDto> GetQualityTrendAsync(Guid equipmentId, DateTime startTime, DateTime endTime);
    }

    /// <summary>
    /// 质量检测结果DTO
    /// </summary>
    public class QualityInspectionResultDto
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 检测项目列表
        /// </summary>
        public InspectionItemDto[] InspectionItems { get; set; }

        /// <summary>
        /// 合格率
        /// </summary>
        public decimal QualificationRate { get; set; }

        /// <summary>
        /// 检测时间
        /// </summary>
        public DateTime InspectionTime { get; set; }
    }

    /// <summary>
    /// 检测项目DTO
    /// </summary>
    public class InspectionItemDto
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 检测值
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 标准值
        /// </summary>
        public decimal StandardValue { get; set; }

        /// <summary>
        /// 允许误差范围
        /// </summary>
        public decimal Tolerance { get; set; }

        /// <summary>
        /// 是否合格
        /// </summary>
        public bool IsQualified { get; set; }
    }

    /// <summary>
    /// 缺陷分析DTO
    /// </summary>
    public class DefectAnalysisDto
    {
        /// <summary>
        /// 缺陷类型分布
        /// </summary>
        public DefectTypeDto[] DefectTypes { get; set; }

        /// <summary>
        /// 缺陷严重程度分布
        /// </summary>
        public DefectSeverityDto[] DefectSeverities { get; set; }

        /// <summary>
        /// 缺陷发生频率
        /// </summary>
        public decimal DefectFrequency { get; set; }
    }

    /// <summary>
    /// 缺陷类型DTO
    /// </summary>
    public class DefectTypeDto
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 发生次数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// 缺陷严重程度DTO
    /// </summary>
    public class DefectSeverityDto
    {
        /// <summary>
        /// 严重程度
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 发生次数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 影响程度描述
        /// </summary>
        public string Impact { get; set; }
    }

    /// <summary>
    /// 质量趋势分析DTO
    /// </summary>
    public class QualityTrendAnalysisDto
    {
        /// <summary>
        /// 时间点
        /// </summary>
        public DateTime[] TimePoints { get; set; }

        /// <summary>
        /// 合格率趋势
        /// </summary>
        public decimal[] QualificationRates { get; set; }

        /// <summary>
        /// 缺陷数量趋势
        /// </summary>
        public int[] DefectCounts { get; set; }

        /// <summary>
        /// 质量改进建议
        /// </summary>
        public string[] ImprovementSuggestions { get; set; }
    }
}