using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料替代服务接口
    /// </summary>
    public interface IMaterialSubstituteAppService : IApplicationService
    {
        /// <summary>
        /// 获取物料替代列表
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns>物料替代列表</returns>
        Task<PagedResultDto<MaterialSubstituteDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        /// <summary>
        /// 获取物料替代详情
        /// </summary>
        /// <param name="id">替代记录ID</param>
        /// <returns>物料替代详情</returns>
        Task<MaterialSubstituteDto> GetAsync(Guid id);

        /// <summary>
        /// 创建物料替代记录
        /// </summary>
        /// <param name="input">创建参数</param>
        /// <returns>创建后的替代记录</returns>
        Task<MaterialSubstituteDto> CreateAsync(CreateMaterialSubstituteDto input);

        /// <summary>
        /// 更新物料替代记录
        /// </summary>
        /// <param name="id">替代记录ID</param>
        /// <param name="input">更新参数</param>
        /// <returns>更新后的替代记录</returns>
        Task<MaterialSubstituteDto> UpdateAsync(Guid id, UpdateMaterialSubstituteDto input);

        /// <summary>
        /// 删除物料替代记录
        /// </summary>
        /// <param name="id">替代记录ID</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 获取物料的所有替代物料
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <returns>替代物料列表</returns>
        Task<List<MaterialSubstituteDto>> GetSubstitutesAsync(Guid materialId);

        /// <summary>
        /// 获取可作为替代的物料列表
        /// </summary>
        /// <param name="materialId">原物料ID</param>
        /// <returns>可替代物料列表</returns>
        Task<List<MaterialSubstituteDto>> GetAvailableSubstitutesAsync(Guid materialId);

        /// <summary>
        /// 启用物料替代关系
        /// </summary>
        /// <param name="id">替代记录ID</param>
        Task EnableAsync(Guid id);

        /// <summary>
        /// 禁用物料替代关系
        /// </summary>
        /// <param name="id">替代记录ID</param>
        Task DisableAsync(Guid id);
    }
}