using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料质量应用服务
    /// </summary>
    public class MaterialQualityAppService : ApplicationService
    {
        private readonly MaterialQualityManager _qualityManager;
        private readonly ILogger<MaterialQualityAppService> _logger;

        public MaterialQualityAppService(
            MaterialQualityManager qualityManager,
            ILogger<MaterialQualityAppService> logger)
        {
            _qualityManager = qualityManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建质检记录
        /// </summary>
        public async Task<MaterialQualityDto> CreateQualityCheckAsync(CreateQualityCheckDto input)
        {
            try
            {
                _logger.LogInformation("开始创建质检记录，物料ID：{MaterialId}", input.MaterialId);
                var quality = await _qualityManager.CreateQualityCheckAsync(
                    input.MaterialId,
                    input.BatchNo,
                    input.CheckType,
                    input.CheckItems,
                    input.CheckResult,
                    input.Remarks);
                _logger.LogInformation("质检记录创建成功，ID：{Id}", quality.Id);
                return ObjectMapper.Map<MaterialQuality, MaterialQualityDto>(quality);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建质检记录失败，物料ID：{MaterialId}", input.MaterialId);
                throw new UserFriendlyException("创建质检记录失败", ex);
            }
        }

        /// <summary>
        /// 更新质检记录
        /// </summary>
        public async Task<MaterialQualityDto> UpdateQualityCheckAsync(Guid id, UpdateQualityCheckDto input)
        {
            try
            {
                _logger.LogInformation("开始更新质检记录，ID：{Id}", id);
                var quality = await _qualityManager.UpdateQualityCheckAsync(
                    id,
                    input.CheckItems,
                    input.CheckResult,
                    input.Remarks);
                _logger.LogInformation("质检记录更新成功，ID：{Id}", id);
                return ObjectMapper.Map<MaterialQuality, MaterialQualityDto>(quality);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新质检记录失败，ID：{Id}", id);
                throw new UserFriendlyException("更新质检记录失败", ex);
            }
        }

        /// <summary>
        /// 获取质检记录
        /// </summary>
        public async Task<MaterialQualityDto> GetQualityCheckAsync(Guid id)
        {
            var quality = await _qualityManager.GetQualityCheckAsync(id);
            return ObjectMapper.Map<MaterialQuality, MaterialQualityDto>(quality);
        }

        /// <summary>
        /// 获取物料的质检记录列表
        /// </summary>
        public async Task<List<MaterialQualityDto>> GetQualityCheckListAsync(Guid materialId, string batchNo = null)
        {
            var qualities = await _qualityManager.GetQualityCheckListAsync(materialId, batchNo);
            return ObjectMapper.Map<List<MaterialQuality>, List<MaterialQualityDto>>(qualities);
        }

        /// <summary>
        /// 设置物料质量状态
        /// </summary>
        public async Task SetQualityStatusAsync(Guid materialId, string batchNo, QualityStatus status)
        {
            try
            {
                _logger.LogInformation("开始设置物料质量状态，物料ID：{MaterialId}，批次：{BatchNo}，状态：{Status}",
                    materialId, batchNo, status);
                await _qualityManager.SetQualityStatusAsync(materialId, batchNo, status);
                _logger.LogInformation("物料质量状态设置成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "设置物料质量状态失败，物料ID：{MaterialId}", materialId);
                throw new UserFriendlyException("设置物料质量状态失败", ex);
            }
        }
    }
}