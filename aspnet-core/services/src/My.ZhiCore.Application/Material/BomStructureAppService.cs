using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料清单结构应用服务
    /// </summary>
    public class BomStructureAppService : ApplicationService
    {
        private readonly BomManager _bomManager;
        private readonly ILogger<BomStructureAppService> _logger;

        public BomStructureAppService(
            BomManager bomManager,
            ILogger<BomStructureAppService> logger)
        {
            _bomManager = bomManager;
            _logger = logger;
        }

        /// <summary>
        /// 添加物料清单项
        /// </summary>
        public async Task<BomItemDto> AddItemAsync(AddBomItemDto input)
        {
            try
            {
                _logger.LogInformation("开始添加物料清单项，版本ID：{VersionId}", input.VersionId);
                var item = await _bomManager.AddItemAsync(
                    input.VersionId,
                    input.MaterialId,
                    input.Quantity,
                    input.UnitId,
                    input.ParentId,
                    input.Remark);
                _logger.LogInformation("物料清单项添加成功，ID：{Id}", item.Id);
                return ObjectMapper.Map<BomItem, BomItemDto>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加物料清单项失败，版本ID：{VersionId}", input.VersionId);
                throw new UserFriendlyException("添加物料清单项失败", ex);
            }
        }

        /// <summary>
        /// 删除物料清单项
        /// </summary>
        public async Task DeleteItemAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("开始删除物料清单项，ID：{Id}", id);
                await _bomManager.DeleteItemAsync(id);
                _logger.LogInformation("物料清单项删除成功，ID：{Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除物料清单项失败，ID：{Id}", id);
                throw new UserFriendlyException("删除物料清单项失败", ex);
            }
        }

        /// <summary>
        /// 更新物料清单项
        /// </summary>
        public async Task<BomItemDto> UpdateItemAsync(UpdateBomItemDto input)
        {
            try
            {
                _logger.LogInformation("开始更新物料清单项，ID：{Id}", input.Id);
                var item = await _bomManager.UpdateItemAsync(
                    input.Id,
                    input.Quantity,
                    input.UnitId,
                    input.Remark);
                _logger.LogInformation("物料清单项更新成功，ID：{Id}", item.Id);
                return ObjectMapper.Map<BomItem, BomItemDto>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新物料清单项失败，ID：{Id}", input.Id);
                throw new UserFriendlyException("更新物料清单项失败", ex);
            }
        }

        /// <summary>
        /// 获取物料清单结构树
        /// </summary>
        public async Task<List<BomTreeNodeDto>> GetBomTreeAsync(Guid versionId)
        {
            var tree = await _bomManager.GetBomTreeAsync(versionId);
            return ObjectMapper.Map<List<BomTreeNode>, List<BomTreeNodeDto>>(tree);
        }
    }
}