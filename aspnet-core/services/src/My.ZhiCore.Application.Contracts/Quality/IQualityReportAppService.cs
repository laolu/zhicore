using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量报表服务接口
    /// </summary>
    public interface IQualityReportAppService : IApplicationService
    {
        /// <summary>
        /// 获取质量报表列表
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns>质量报表列表</returns>
        Task<PagedResultDto<QualityReportDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        /// <summary>
        /// 获取质量报表详情
        /// </summary>
        /// <param name="id">报表ID</param>
        /// <returns>质量报表详情</returns>
        Task<QualityReportDto> GetAsync(Guid id);

        /// <summary>
        /// 生成质量报表
        /// </summary>
        /// <param name="input">报表生成参数</param>
        /// <returns>生成的质量报表</returns>
        Task<QualityReportDto> GenerateAsync(GenerateQualityReportDto input);

        /// <summary>
        /// 导出质量报表
        /// </summary>
        /// <param name="id">报表ID</param>
        /// <param name="format">导出格式</param>
        /// <returns>导出文件的字节数组</returns>
        Task<byte[]> ExportAsync(Guid id, string format);

        /// <summary>
        /// 删除质量报表
        /// </summary>
        /// <param name="id">报表ID</param>
        Task DeleteAsync(Guid id);
    }

    /// <summary>
    /// 质量报表DTO
    /// </summary>
    public class QualityReportDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 报表编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 报表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 报表类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 统计开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 统计结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 报表数据
        /// </summary>
        public Dictionary<string, object> Data { get; set; }

        /// <summary>
        /// 报表状态
        /// </summary>
        public QualityReportStatus Status { get; set; }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime GeneratedTime { get; set; }
    }

    /// <summary>
    /// 生成质量报表DTO
    /// </summary>
    public class GenerateQualityReportDto
    {
        /// <summary>
        /// 报表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 报表类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 统计开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 统计结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 报表参数
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; }
    }

    /// <summary>
    /// 质量报表状态
    /// </summary>
    public enum QualityReportStatus
    {
        /// <summary>
        /// 生成中
        /// </summary>
        Generating = 0,

        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 1,

        /// <summary>
        /// 失败
        /// </summary>
        Failed = 2
    }
}