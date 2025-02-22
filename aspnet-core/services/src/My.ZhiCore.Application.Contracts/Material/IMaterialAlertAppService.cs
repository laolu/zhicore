using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料预警服务接口
    /// </summary>
    public interface IMaterialAlertAppService : IApplicationService
    {
        /// <summary>
        /// 获取物料预警列表
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns>物料预警列表</returns>
        Task<PagedResultDto<MaterialAlertDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        /// <summary>
        /// 获取物料预警详情
        /// </summary>
        /// <param name="id">预警ID</param>
        /// <returns>物料预警详情</returns>
        Task<MaterialAlertDto> GetAsync(Guid id);

        /// <summary>
        /// 创建物料预警
        /// </summary>
        /// <param name="input">创建参数</param>
        /// <returns>创建后的预警记录</returns>
        Task<MaterialAlertDto> CreateAsync(CreateMaterialAlertDto input);

        /// <summary>
        /// 更新物料预警
        /// </summary>
        /// <param name="id">预警ID</param>
        /// <param name="input">更新参数</param>
        /// <returns>更新后的预警记录</returns>
        Task<MaterialAlertDto> UpdateAsync(Guid id, UpdateMaterialAlertDto input);

        /// <summary>
        /// 删除物料预警
        /// </summary>
        /// <param name="id">预警ID</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 获取物料的所有预警记录
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <returns>预警记录列表</returns>
        Task<List<MaterialAlertDto>> GetMaterialAlertsAsync(Guid materialId);

        /// <summary>
        /// 获取未处理的预警列表
        /// </summary>
        /// <returns>未处理的预警列表</returns>
        Task<List<MaterialAlertDto>> GetPendingAlertsAsync();

        /// <summary>
        /// 处理预警
        /// </summary>
        /// <param name="id">预警ID</param>
        /// <param name="status">处理状态</param>
        /// <param name="remarks">处理备注</param>
        Task ProcessAlertAsync(Guid id, string status, string remarks);

        /// <summary>
        /// 获取预警统计信息
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>预警统计信息</returns>
        Task<MaterialAlertStatisticsDto> GetAlertStatisticsAsync(DateTime startTime, DateTime endTime);
    }

    /// <summary>
    /// 物料预警统计DTO
    /// </summary>
    public class MaterialAlertStatisticsDto
    {
        /// <summary>
        /// 总预警数量
        /// </summary>
        public int TotalAlerts { get; set; }

        /// <summary>
        /// 未处理预警数量
        /// </summary>
        public int PendingAlerts { get; set; }

        /// <summary>
        /// 处理中预警数量
        /// </summary>
        public int ProcessingAlerts { get; set; }

        /// <summary>
        /// 已处理预警数量
        /// </summary>
        public int ProcessedAlerts { get; set; }

        /// <summary>
        /// 各类型预警数量统计
        /// </summary>
        public Dictionary<string, int> AlertsByType { get; set; }

        /// <summary>
        /// 各级别预警数量统计
        /// </summary>
        public Dictionary<string, int> AlertsByLevel { get; set; }
    }
}