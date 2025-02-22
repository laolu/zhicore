using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料替代应用服务
    /// </summary>
    public class MaterialSubstituteAppService : ApplicationService
    {
        private readonly MaterialSubstituteManager _substituteManager;
        private readonly ILogger<MaterialSubstituteAppService> _logger;

        public MaterialSubstituteAppService(
            MaterialSubstituteManager substituteManager,
            ILogger<MaterialSubstituteAppService> logger)
        {
            _substituteManager = substituteManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建物料替代关系
        /// </summary>
        public async Task<MaterialSubstituteDto> CreateSubstituteAsync(CreateMaterialSubstituteDto input)
        {
            try
            {
                _logger.LogInformation("开始创建物料替代关系，原物料ID：{OriginalMaterialId}，替代物料ID：{SubstituteMaterialId}", 
                    input.OriginalMaterialId, input.SubstituteMaterialId);
                var substitute = await _substituteManager.CreateSubstituteAsync(
                    input.OriginalMaterialId,
                    input.SubstituteMaterialId,
                    input.SubstituteRatio,
                    input.Priority,
                    input.ValidStartDate,
                    input.ValidEndDate,
                    input.Remark);
                _logger.LogInformation("物料替代关系创建成功，ID：{Id}", substitute.Id);
                return ObjectMapper.Map<MaterialSubstitute, MaterialSubstituteDto>(substitute);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建物料替代关系失败，原物料ID：{OriginalMaterialId}", input.OriginalMaterialId);
                throw new UserFriendlyException("创建物料替代关系失败", ex);
            }
        }

        /// <summary>
        /// 更新物料替代关系
        /// </summary>
        public async Task<MaterialSubstituteDto> UpdateSubstituteAsync(UpdateMaterialSubstituteDto input)
        {
            try
            {
                _logger.LogInformation("开始更新物料替代关系，ID：{Id}", input.Id);
                var substitute = await _substituteManager.UpdateSubstituteAsync(
                    input.Id,
                    input.SubstituteRatio,
                    input.Priority,
                    input.ValidStartDate,
                    input.ValidEndDate,
                    input.Remark);
                _logger.LogInformation("物料替代关系更新成功，ID：{Id}", substitute.Id);
                return ObjectMapper.Map<MaterialSubstitute, MaterialSubstituteDto>(substitute);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新物料替代关系失败，ID：{Id}", input.Id);
                throw new UserFriendlyException("更新物料替代关系失败", ex);
            }
        }

        /// <summary>
        /// 获取物料的替代关系列表
        /// </summary>
        public async Task<List<MaterialSubstituteDto>> GetSubstituteListAsync(Guid materialId, bool? isValid = null)
        {
            var substitutes = await _substituteManager.GetSubstituteListAsync(materialId, isValid);
            return ObjectMapper.Map<List<MaterialSubstitute>, List<MaterialSubstituteDto>>(substitutes);
        }

        /// <summary>
        /// 删除物料替代关系
        /// </summary>
        public async Task DeleteSubstituteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("开始删除物料替代关系，ID：{Id}", id);
                await _substituteManager.DeleteSubstituteAsync(id);
                _logger.LogInformation("物料替代关系删除成功，ID：{Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除物料替代关系失败，ID：{Id}", id);
                throw new UserFriendlyException("删除物料替代关系失败", ex);
            }
        }

        /// <summary>
        /// 获取可替代物料列表
        /// </summary>
        public async Task<List<MaterialSubstituteDto>> GetAvailableSubstitutesAsync(Guid materialId, DateTime? date = null)
        {
            var substitutes = await _substituteManager.GetAvailableSubstitutesAsync(materialId, date);
            return ObjectMapper.Map<List<MaterialSubstitute>, List<MaterialSubstituteDto>>(substitutes);
        }
    }
}