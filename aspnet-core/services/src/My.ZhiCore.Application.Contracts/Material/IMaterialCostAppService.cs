using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料成本服务接口
    /// </summary>
    public interface IMaterialCostAppService : IApplicationService
    {
        /// <summary>
        /// 获取物料成本列表
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns>物料成本列表</returns>
        Task<PagedResultDto<MaterialCostDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        /// <summary>
        /// 获取物料成本详情
        /// </summary>
        /// <param name="id">成本记录ID</param>
        /// <returns>物料成本详情</returns>
        Task<MaterialCostDto> GetAsync(Guid id);

        /// <summary>
        /// 创建物料成本记录
        /// </summary>
        /// <param name="input">创建参数</param>
        /// <returns>创建后的成本记录</returns>
        Task<MaterialCostDto> CreateAsync(CreateMaterialCostDto input);

        /// <summary>
        /// 更新物料成本记录
        /// </summary>
        /// <param name="id">成本记录ID</param>
        /// <param name="input">更新参数</param>
        /// <returns>更新后的成本记录</returns>
        Task<MaterialCostDto> UpdateAsync(Guid id, UpdateMaterialCostDto input);

        /// <summary>
        /// 删除物料成本记录
        /// </summary>
        /// <param name="id">成本记录ID</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 获取物料的成本统计
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>成本统计数据</returns>
        Task<MaterialCostStatisticsDto> GetCostStatisticsAsync(Guid materialId, DateTime startTime, DateTime endTime);
    }

    /// <summary>
    /// 物料成本统计DTO
    /// </summary>
    public class MaterialCostStatisticsDto
    {
        /// <summary>
        /// 物料ID
        /// </summary>
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 总成本
        /// </summary>
        public decimal TotalCost { get; set; }

        /// <summary>
        /// 各类型成本明细
        /// </summary>
        public Dictionary<string, decimal> CostByType { get; set; }

        /// <summary>
        /// 成本趋势数据
        /// </summary>
        public List<MaterialCostTrendDto> CostTrend { get; set; }
    }

    /// <summary>
    /// 物料成本趋势DTO
    /// </summary>
    public class MaterialCostTrendDto
    {
        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 成本金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 成本类型
        /// </summary>
        public string CostType { get; set; }
    }
}