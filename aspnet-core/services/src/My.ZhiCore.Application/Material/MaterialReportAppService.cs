using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Logging;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料报表应用服务
    /// </summary>
    public class MaterialReportAppService : ApplicationService
    {
        private readonly MaterialInventoryManager _inventoryManager;
        private readonly MaterialChangeManager _changeManager;
        private readonly ILogger<MaterialReportAppService> _logger;

        public MaterialReportAppService(
            MaterialInventoryManager inventoryManager,
            MaterialChangeManager changeManager,
            ILogger<MaterialReportAppService> logger)
        {
            _inventoryManager = inventoryManager;
            _changeManager = changeManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取物料库存报表
        /// </summary>
        public async Task<MaterialInventoryReportDto> GetInventoryReportAsync(MaterialInventoryReportQueryDto input)
        {
            try
            {
                _logger.LogInformation("开始生成物料库存报表，仓库ID：{WarehouseId}", input.WarehouseId);
                var report = new MaterialInventoryReportDto
                {
                    WarehouseId = input.WarehouseId,
                    ReportDate = DateTime.Now,
                    Items = new List<MaterialInventoryReportItemDto>()
                };

                var inventories = await _inventoryManager.GetInventoryListByWarehouseAsync(
                    input.WarehouseId,
                    input.LocationId,
                    input.CategoryId);

                foreach (var inventory in inventories)
                {
                    report.Items.Add(new MaterialInventoryReportItemDto
                    {
                        MaterialId = inventory.MaterialId,
                        LocationId = inventory.LocationId,
                        Quantity = inventory.Quantity,
                        BatchNo = inventory.BatchNo
                    });
                }

                _logger.LogInformation("物料库存报表生成成功，仓库ID：{WarehouseId}", input.WarehouseId);
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "生成物料库存报表失败，仓库ID：{WarehouseId}", input.WarehouseId);
                throw new UserFriendlyException("生成物料库存报表失败", ex);
            }
        }

        /// <summary>
        /// 获取物料变更报表
        /// </summary>
        public async Task<MaterialChangeReportDto> GetChangeReportAsync(MaterialChangeReportQueryDto input)
        {
            try
            {
                _logger.LogInformation("开始生成物料变更报表，开始日期：{StartDate}，结束日期：{EndDate}",
                    input.StartDate, input.EndDate);

                var report = new MaterialChangeReportDto
                {
                    StartDate = input.StartDate,
                    EndDate = input.EndDate,
                    Items = new List<MaterialChangeReportItemDto>()
                };

                var changes = await _changeManager.GetChangeHistoryByDateRangeAsync(
                    input.StartDate,
                    input.EndDate,
                    input.MaterialId,
                    input.ChangeType);

                foreach (var change in changes)
                {
                    report.Items.Add(new MaterialChangeReportItemDto
                    {
                        MaterialId = change.MaterialId,
                        ChangeType = change.ChangeType,
                        ChangeReason = change.ChangeReason,
                        ChangeContent = change.ChangeContent,
                        ChangerId = change.ChangerId,
                        ChangeTime = change.CreationTime
                    });
                }

                _logger.LogInformation("物料变更报表生成成功");
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "生成物料变更报表失败");
                throw new UserFriendlyException("生成物料变更报表失败", ex);
            }
        }
    }
}