using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料清单基础应用服务
    /// </summary>
    public class BomBaseAppService : ApplicationService
    {
        private readonly BomManager _bomManager;
        private readonly ILogger<BomBaseAppService> _logger;

        public BomBaseAppService(
            BomManager bomManager,
            ILogger<BomBaseAppService> logger)
        {
            _bomManager = bomManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建物料清单
        /// </summary>
        public async Task<BomDto> CreateAsync(CreateBomDto input)
        {
            try
            {
                _logger.LogInformation("开始创建物料清单，物料ID：{MaterialId}", input.MaterialId);
                var bom = await _bomManager.CreateAsync(
                    input.MaterialId,
                    input.Description);
                _logger.LogInformation("物料清单创建成功，ID：{Id}", bom.Id);
                return ObjectMapper.Map<Bom, BomDto>(bom);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建物料清单失败，物料ID：{MaterialId}", input.MaterialId);
                throw new UserFriendlyException("创建物料清单失败", ex);
            }
        }

        /// <summary>
        /// 删除物料清单
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("开始删除物料清单，ID：{Id}", id);
                await _bomManager.DeleteAsync(id);
                _logger.LogInformation("物料清单删除成功，ID：{Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除物料清单失败，ID：{Id}", id);
                throw new UserFriendlyException("删除物料清单失败", ex);
            }
        }

        /// <summary>
        /// 获取物料清单
        /// </summary>
        public async Task<BomDto> GetAsync(Guid id)
        {
            var bom = await _bomManager.GetAsync(id);
            return ObjectMapper.Map<Bom, BomDto>(bom);
        }
    }
}