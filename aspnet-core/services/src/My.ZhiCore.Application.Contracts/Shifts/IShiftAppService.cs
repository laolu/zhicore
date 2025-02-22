using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using My.ZhiCore.Shifts.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Shifts
{
    /// <summary>
    /// 班次服务接口
    /// </summary>
    public interface IShiftAppService : IApplicationService
    {
        #region 班次基础管理

        /// <summary>
        /// 获取班次列表
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns>班次列表</returns>
        Task<PagedResultDto<ShiftBaseDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        /// <summary>
        /// 获取班次详情
        /// </summary>
        /// <param name="id">班次ID</param>
        /// <returns>班次详情</returns>
        Task<ShiftBaseDto> GetAsync(Guid id);

        /// <summary>
        /// 创建班次
        /// </summary>
        /// <param name="input">创建参数</param>
        /// <returns>创建后的班次</returns>
        Task<ShiftBaseDto> CreateAsync(CreateShiftDto input);

        /// <summary>
        /// 更新班次
        /// </summary>
        /// <param name="id">班次ID</param>
        /// <param name="input">更新参数</param>
        /// <returns>更新后的班次</returns>
        Task<ShiftBaseDto> UpdateAsync(Guid id, UpdateShiftDto input);

        /// <summary>
        /// 删除班次
        /// </summary>
        /// <param name="id">班次ID</param>
        Task DeleteAsync(Guid id);

        #endregion

        #region 班次考勤管理

        /// <summary>
        /// 获取班次考勤列表
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns>考勤列表</returns>
        Task<PagedResultDto<ShiftAttendanceDto>> GetAttendanceListAsync(PagedAndSortedResultRequestDto input);

        /// <summary>
        /// 获取班次考勤详情
        /// </summary>
        /// <param name="id">考勤ID</param>
        /// <returns>考勤详情</returns>
        Task<ShiftAttendanceDto> GetAttendanceAsync(Guid id);

        /// <summary>
        /// 创建班次考勤
        /// </summary>
        /// <param name="input">创建参数</param>
        /// <returns>创建后的考勤记录</returns>
        Task<ShiftAttendanceDto> CreateAttendanceAsync(CreateShiftAttendanceDto input);

        /// <summary>
        /// 更新班次考勤
        /// </summary>
        /// <param name="id">考勤ID</param>
        /// <param name="input">更新参数</param>
        /// <returns>更新后的考勤记录</returns>
        Task<ShiftAttendanceDto> UpdateAttendanceAsync(Guid id, UpdateShiftAttendanceDto input);

        #endregion

        #region 班次交接管理

        /// <summary>
        /// 获取班次交接列表
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns>交接列表</returns>
        Task<PagedResultDto<ShiftHandoverDto>> GetHandoverListAsync(PagedAndSortedResultRequestDto input);

        /// <summary>
        /// 获取班次交接详情
        /// </summary>
        /// <param name="id">交接ID</param>
        /// <returns>交接详情</returns>
        Task<ShiftHandoverDto> GetHandoverAsync(Guid id);

        /// <summary>
        /// 创建班次交接
        /// </summary>
        /// <param name="input">创建参数</param>
        /// <returns>创建后的交接记录</returns>
        Task<ShiftHandoverDto> CreateHandoverAsync(CreateShiftHandoverDto input);

        /// <summary>
        /// 更新班次交接
        /// </summary>
        /// <param name="id">交接ID</param>
        /// <param name="input">更新参数</param>
        /// <returns>更新后的交接记录</returns>
        Task<ShiftHandoverDto> UpdateHandoverAsync(Guid id, UpdateShiftHandoverDto input);

        #endregion
    }
}