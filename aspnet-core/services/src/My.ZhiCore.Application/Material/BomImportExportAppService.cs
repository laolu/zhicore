using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料清单导入导出服务
    /// </summary>
    public class BomImportExportAppService : ApplicationService
    {
        private readonly BomManager _bomManager;
        private readonly ILogger<BomImportExportAppService> _logger;

        public BomImportExportAppService(
            BomManager bomManager,
            ILogger<BomImportExportAppService> logger)
        {
            _bomManager = bomManager;
            _logger = logger;
        }

        /// <summary>
        /// 导出物料清单
        /// </summary>
        public async Task<BomExportResultDto> ExportAsync(ExportBomDto input)
        {
            try
            {
                _logger.LogInformation("开始导出物料清单，物料清单ID：{BomId}", input.BomId);
                var result = await _bomManager.ExportAsync(
                    input.BomId,
                    input.IncludeSubItems,
                    input.ExportFormat);
                _logger.LogInformation("物料清单导出完成");
                return ObjectMapper.Map<BomExportResult, BomExportResultDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "导出物料清单失败");
                throw new UserFriendlyException("导出物料清单失败", ex);
            }
        }

        /// <summary>
        /// 导入物料清单
        /// </summary>
        public async Task<BomImportResultDto> ImportAsync(ImportBomDto input)
        {
            try
            {
                _logger.LogInformation("开始导入物料清单");
                var result = await _bomManager.ImportAsync(
                    input.FileContent,
                    input.ImportFormat,
                    input.UpdateExisting);
                _logger.LogInformation("物料清单导入完成");
                return ObjectMapper.Map<BomImportResult, BomImportResultDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "导入物料清单失败");
                throw new UserFriendlyException("导入物料清单失败", ex);
            }
        }

        /// <summary>
        /// 验证导入文件
        /// </summary>
        public async Task<BomImportValidationResultDto> ValidateImportFileAsync(ValidateBomImportFileDto input)
        {
            try
            {
                _logger.LogInformation("开始验证物料清单导入文件");
                var result = await _bomManager.ValidateImportFileAsync(
                    input.FileContent,
                    input.ImportFormat);
                _logger.LogInformation("物料清单导入文件验证完成");
                return ObjectMapper.Map<BomImportValidationResult, BomImportValidationResultDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "验证物料清单导入文件失败");
                throw new UserFriendlyException("验证物料清单导入文件失败", ex);
            }
        }
    }
}