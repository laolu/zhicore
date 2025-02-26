using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料清单比较服务
    /// </summary>
    public class BomCompareAppService : ApplicationService
    {
        private readonly BomManager _bomManager;
        private readonly ILogger<BomCompareAppService> _logger;

        public BomCompareAppService(
            BomManager bomManager,
            ILogger<BomCompareAppService> logger)
        {
            _bomManager = bomManager;
            _logger = logger;
        }

        /// <summary>
        /// 比较两个版本的物料清单差异
        /// </summary>
        public async Task<BomCompareResultDto> CompareVersionsAsync(CompareBomVersionsDto input)
        {
            try
            {
                _logger.LogInformation("开始比较物料清单版本差异，版本1：{Version1Id}，版本2：{Version2Id}", input.Version1Id, input.Version2Id);
                var result = await _bomManager.CompareVersionsAsync(
                    input.Version1Id,
                    input.Version2Id);
                _logger.LogInformation("物料清单版本比较完成");
                return ObjectMapper.Map<BomCompareResult, BomCompareResultDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "比较物料清单版本差异失败");
                throw new UserFriendlyException("比较物料清单版本差异失败", ex);
            }
        }

        /// <summary>
        /// 获取物料清单版本的变更历史
        /// </summary>
        public async Task<BomChangeHistoryDto> GetVersionChangeHistoryAsync(Guid versionId)
        {
            try
            {
                _logger.LogInformation("开始获取物料清单版本变更历史，版本ID：{VersionId}", versionId);
                var history = await _bomManager.GetVersionChangeHistoryAsync(versionId);
                _logger.LogInformation("获取物料清单版本变更历史完成");
                return ObjectMapper.Map<BomChangeHistory, BomChangeHistoryDto>(history);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取物料清单版本变更历史失败");
                throw new UserFriendlyException("获取物料清单版本变更历史失败", ex);
            }
        }
    }
}