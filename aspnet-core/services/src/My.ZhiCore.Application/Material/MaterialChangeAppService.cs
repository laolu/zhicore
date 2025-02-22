using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料变更应用服务
    /// </summary>
    public class MaterialChangeAppService : ApplicationService
    {
        private readonly MaterialChangeManager _changeManager;
        private readonly ILogger<MaterialChangeAppService> _logger;

        public MaterialChangeAppService(
            MaterialChangeManager changeManager,
            ILogger<MaterialChangeAppService> logger)
        {
            _changeManager = changeManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建物料变更记录
        /// </summary>
        public async Task<MaterialChangeDto> CreateChangeAsync(CreateMaterialChangeDto input)
        {
            try
            {
                _logger.LogInformation("开始创建物料变更记录，物料ID：{MaterialId}", input.MaterialId);
                var change = await _changeManager.CreateChangeAsync(
                    input.MaterialId,
                    input.ChangeType,
                    input.ChangeReason,
                    input.ChangeContent,
                    input.ChangerId);
                _logger.LogInformation("物料变更记录创建成功，ID：{Id}", change.Id);
                return ObjectMapper.Map<MaterialChange, MaterialChangeDto>(change);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建物料变更记录失败，物料ID：{MaterialId}", input.MaterialId);
                throw new UserFriendlyException("创建物料变更记录失败", ex);
            }
        }

        /// <summary>
        /// 获取物料变更历史
        /// </summary>
        public async Task<List<MaterialChangeDto>> GetChangeHistoryAsync(Guid materialId)
        {
            var changes = await _changeManager.GetChangeHistoryAsync(materialId);
            return ObjectMapper.Map<List<MaterialChange>, List<MaterialChangeDto>>(changes);
        }

        /// <summary>
        /// 审核物料变更
        /// </summary>
        public async Task<MaterialChangeDto> ApproveChangeAsync(ApproveChangeDto input)
        {
            try
            {
                _logger.LogInformation("开始审核物料变更，变更记录ID：{ChangeId}", input.ChangeId);
                var change = await _changeManager.ApproveChangeAsync(
                    input.ChangeId,
                    input.ApproverId,
                    input.ApprovalResult,
                    input.ApprovalComments);
                _logger.LogInformation("物料变更审核成功，ID：{Id}", change.Id);
                return ObjectMapper.Map<MaterialChange, MaterialChangeDto>(change);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "物料变更审核失败，变更记录ID：{ChangeId}", input.ChangeId);
                throw new UserFriendlyException("物料变更审核失败", ex);
            }
        }
    }
}