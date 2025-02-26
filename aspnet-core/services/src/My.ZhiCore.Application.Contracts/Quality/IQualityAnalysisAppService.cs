using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量分析服务接口
    /// </summary>
    public interface IQualityAnalysisAppService : IApplicationService
    {
        /// <summary>
        /// 获取质量检验合格率统计
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>合格率统计数据</returns>
        Task<QualityStatisticsDto> GetQualificationRateAsync(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取质量问题分布统计
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>问题分布统计数据</returns>
        Task<QualityStatisticsDto> GetIssueDistributionAsync(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取质量趋势分析
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>质量趋势分析数据</returns>
        Task<QualityTrendDto> GetQualityTrendAsync(DateTime startTime, DateTime endTime);
    }

    /// <summary>
    /// 质量统计DTO
    /// </summary>
    public class QualityStatisticsDto
    {
        /// <summary>
        /// 统计项目
        /// </summary>
        public string[] Categories { get; set; }

        /// <summary>
        /// 统计数值
        /// </summary>
        public decimal[] Values { get; set; }
    }

    /// <summary>
    /// 质量趋势DTO
    /// </summary>
    public class QualityTrendDto
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
        /// 问题数量趋势
        /// </summary>
        public int[] IssueCount { get; set; }
    }
}