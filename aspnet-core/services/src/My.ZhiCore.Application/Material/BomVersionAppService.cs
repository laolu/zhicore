using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料清单版本应用服务
    /// </summary>
    public class BomVersionAppService : ApplicationService
    {
        private readonly BomManager _bomManager;
        private readonly ILogger<BomVersionAppService> _logger;

        public BomVersionAppService(
            BomManager bomManager,
            ILogger<BomVersionAppService> logger)
        {
            _bomManager = bomManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建物料清单版本
        /// </summary>
        public async Task<BomVersionDto> CreateVersionAsync(CreateBomVersionDto input)
        {
            try
            {
                _logger.LogInformation("开始创建物料清单版本，物料清单ID：{BomId}", input.BomId);
                var version = await _bomManager.CreateVersionAsync(
                    input.BomId,
                    input.VersionCode,
                    input.Description);
                _logger.LogInformation("物料清单版本创建成功，ID：{Id}", version.Id);
                return ObjectMapper.Map<BomVersion, BomVersionDto>(version);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建物料清单版本失败，物料清单ID：{BomId}", input.BomId);
                throw new UserFriendlyException("创建物料清单版本失败", ex);
            }
        }

        /// <summary>
        /// 获取物料清单版本
        /// </summary>
        public async Task<BomVersionDto> GetVersionAsync(Guid id)
        {
            var version = await _bomManager.GetVersionAsync(id);
            return ObjectMapper.Map<BomVersion, BomVersionDto>(version);
        }

        /// <summary>
        /// 获取物料清单的所有版本
        /// </summary>
        public async Task<List<BomVersionDto>> GetVersionListAsync(Guid bomId)
        {
            var versions = await _bomManager.GetVersionListAsync(bomId);
            return ObjectMapper.Map<List<BomVersion>, List<BomVersionDto>>(versions);
        }

        /// <summary>
        /// 设置默认版本
        /// </summary>
        public async Task SetDefaultVersionAsync(Guid versionId)
        {
            try
            {
                _logger.LogInformation("开始设置默认版本，版本ID：{VersionId}", versionId);
                await _bomManager.SetDefaultVersionAsync(versionId);
                _logger.LogInformation("设置默认版本成功，版本ID：{VersionId}", versionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "设置默认版本失败，版本ID：{VersionId}", versionId);
                throw new UserFriendlyException("设置默认版本失败", ex);
            }
        }
    }
}