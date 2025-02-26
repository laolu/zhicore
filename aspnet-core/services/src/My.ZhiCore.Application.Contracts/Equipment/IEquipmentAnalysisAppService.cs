using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备分析服务接口
    /// </summary>
    public interface IEquipmentAnalysisAppService : IApplicationService
    {
        /// <summary>
        /// 获取设备运行状态分析
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>设备运行状态分析数据</returns>
        Task<EquipmentOperationAnalysisDto> GetOperationAnalysisAsync(Guid equipmentId, DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取设备性能趋势分析
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>设备性能趋势分析数据</returns>
        Task<EquipmentPerformanceTrendDto> GetPerformanceTrendAsync(Guid equipmentId, DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取设备故障分析
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>设备故障分析数据</returns>
        Task<EquipmentFailureAnalysisDto> GetFailureAnalysisAsync(Guid equipmentId, DateTime startTime, DateTime endTime);
    }
}