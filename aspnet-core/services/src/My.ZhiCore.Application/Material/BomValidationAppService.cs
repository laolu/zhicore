using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料清单验证服务
    /// </summary>
    public class BomValidationAppService : ApplicationService
    {
        private readonly BomManager _bomManager;
        private readonly ILogger<BomValidationAppService> _logger;

        public BomValidationAppService(
            BomManager bomManager,
            ILogger<BomValidationAppService> logger)
        {
            _bomManager = bomManager;
            _logger = logger;
        }

        /// <summary>
        /// 验证物料清单的完整性
        /// </summary>
        public async Task<BomValidationResultDto> ValidateCompletenessAsync(Guid bomId)
        {
            try
            {
                _logger.LogInformation("开始验证物料清单完整性，物料清单ID：{BomId}", bomId);
                var result = await _bomManager.ValidateCompletenessAsync(bomId);
                _logger.LogInformation("物料清单完整性验证完成");
                return ObjectMapper.Map<BomValidationResult, BomValidationResultDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "验证物料清单完整性失败");
                throw new UserFriendlyException("验证物料清单完整性失败", ex);
            }
        }

        /// <summary>
        /// 验证物料清单的循环引用
        /// </summary>
        public async Task<BomCircularReferenceResultDto> ValidateCircularReferenceAsync(Guid bomId)
        {
            try
            {
                _logger.LogInformation("开始验证物料清单循环引用，物料清单ID：{BomId}", bomId);
                var result = await _bomManager.ValidateCircularReferenceAsync(bomId);
                _logger.LogInformation("物料清单循环引用验证完成");
                return ObjectMapper.Map<BomCircularReferenceResult, BomCircularReferenceResultDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "验证物料清单循环引用失败");
                throw new UserFriendlyException("验证物料清单循环引用失败", ex);
            }
        }

        /// <summary>
        /// 验证物料清单中的物料有效性
        /// </summary>
        public async Task<BomMaterialValidationResultDto> ValidateMaterialsAsync(Guid bomId)
        {
            try
            {
                _logger.LogInformation("开始验证物料清单中的物料有效性，物料清单ID：{BomId}", bomId);
                var result = await _bomManager.ValidateMaterialsAsync(bomId);
                _logger.LogInformation("物料清单中的物料有效性验证完成");
                return ObjectMapper.Map<BomMaterialValidationResult, BomMaterialValidationResultDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "验证物料清单中的物料有效性失败");
                throw new UserFriendlyException("验证物料清单中的物料有效性失败", ex);
            }
        }
    }
}