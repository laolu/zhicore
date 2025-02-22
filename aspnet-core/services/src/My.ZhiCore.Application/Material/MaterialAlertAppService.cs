using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料预警服务，用于管理物料的库存预警和质量预警
    /// </summary>
    public class MaterialAlertAppService : ApplicationService
    {
        private readonly IRepository<Material, Guid> _materialRepository;
        private readonly MaterialInventoryManager _materialInventoryManager;
        private readonly MaterialQualityStandardManager _materialQualityManager;
        private readonly ILogger<MaterialAlertAppService> _logger;

        public MaterialAlertAppService(
            IRepository<Material, Guid> materialRepository,
            MaterialInventoryManager materialInventoryManager,
            MaterialQualityStandardManager materialQualityManager,
            ILogger<MaterialAlertAppService> logger)
        {
            _materialRepository = materialRepository;
            _materialInventoryManager = materialInventoryManager;
            _materialQualityManager = materialQualityManager;
            _logger = logger;
        }

        /// <summary>
        /// 检查物料库存预警
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <returns>库存预警信息</returns>
        public async Task<MaterialInventoryAlert> CheckInventoryAlertAsync(Guid materialId)
        {
            try
            {
                _logger.LogInformation($"开始检查物料 {materialId} 的库存预警");
                var material = await _materialRepository.GetAsync(materialId);
                var inventory = await _materialInventoryManager.GetCurrentInventoryAsync(materialId);

                var alert = new MaterialInventoryAlert
                {
                    MaterialId = materialId,
                    MaterialName = material.Name,
                    CurrentQuantity = inventory.Quantity,
                    MinimumQuantity = material.MinimumQuantity,
                    MaximumQuantity = material.MaximumQuantity,
                    AlertLevel = GetInventoryAlertLevel(inventory.Quantity, material)
                };

                _logger.LogInformation($"物料 {material.Name} 的库存预警检查完成");
                return alert;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"检查物料 {materialId} 库存预警失败");
                throw;
            }
        }

        /// <summary>
        /// 检查物料质量预警
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <returns>质量预警信息</returns>
        public async Task<MaterialQualityAlert> CheckQualityAlertAsync(Guid materialId)
        {
            try
            {
                _logger.LogInformation($"开始检查物料 {materialId} 的质量预警");
                var material = await _materialRepository.GetAsync(materialId);
                var qualityData = await _materialQualityManager.GetLatestQualityDataAsync(materialId);

                var alert = new MaterialQualityAlert
                {
                    MaterialId = materialId,
                    MaterialName = material.Name,
                    QualityIndicators = qualityData.Indicators,
                    AlertLevel = GetQualityAlertLevel(qualityData)
                };

                _logger.LogInformation($"物料 {material.Name} 的质量预警检查完成");
                return alert;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"检查物料 {materialId} 质量预警失败");
                throw;
            }
        }

        /// <summary>
        /// 获取所有预警物料
        /// </summary>
        /// <returns>预警物料列表</returns>
        public async Task<List<MaterialAlertSummary>> GetAllAlertsAsync()
        {
            try
            {
                _logger.LogInformation("开始获取所有预警物料");
                var materials = await _materialRepository.GetListAsync();
                var alertSummaries = new List<MaterialAlertSummary>();

                foreach (var material in materials)
                {
                    var inventoryAlert = await CheckInventoryAlertAsync(material.Id);
                    var qualityAlert = await CheckQualityAlertAsync(material.Id);

                    if (inventoryAlert.AlertLevel != AlertLevel.Normal ||
                        qualityAlert.AlertLevel != AlertLevel.Normal)
                    {
                        alertSummaries.Add(new MaterialAlertSummary
                        {
                            MaterialId = material.Id,
                            MaterialName = material.Name,
                            InventoryAlertLevel = inventoryAlert.AlertLevel,
                            QualityAlertLevel = qualityAlert.AlertLevel,
                            LastCheckTime = DateTime.Now
                        });
                    }
                }

                _logger.LogInformation($"获取预警物料完成，共 {alertSummaries.Count} 条预警");
                return alertSummaries;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取预警物料失败");
                throw;
            }
        }

        private AlertLevel GetInventoryAlertLevel(int currentQuantity, Material material)
        {
            if (currentQuantity <= material.MinimumQuantity)
            {
                return AlertLevel.Critical;
            }
            else if (currentQuantity <= material.MinimumQuantity * 1.2)
            {
                return AlertLevel.Warning;
            }
            else if (currentQuantity >= material.MaximumQuantity)
            {
                return AlertLevel.Warning;
            }
            return AlertLevel.Normal;
        }

        private AlertLevel GetQualityAlertLevel(MaterialQualityData qualityData)
        {
            var maxAlertLevel = AlertLevel.Normal;

            foreach (var indicator in qualityData.Indicators)
            {
                if (indicator.Value < indicator.MinValue || indicator.Value > indicator.MaxValue)
                {
                    maxAlertLevel = AlertLevel.Critical;
                    break;
                }
                else if (indicator.Value < indicator.MinValue * 1.1 || 
                         indicator.Value > indicator.MaxValue * 0.9)
                {
                    maxAlertLevel = AlertLevel.Warning;
                }
            }

            return maxAlertLevel;
        }
    }
}