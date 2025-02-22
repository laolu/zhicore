using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料库存应用服务
    /// </summary>
    public class MaterialInventoryAppService : ApplicationService
    {
        private readonly MaterialInventoryManager _inventoryManager;
        private readonly ILogger<MaterialInventoryAppService> _logger;

        public MaterialInventoryAppService(
            MaterialInventoryManager inventoryManager,
            ILogger<MaterialInventoryAppService> logger)
        {
            _inventoryManager = inventoryManager;
            _logger = logger;
        }

        /// <summary>
        /// 入库
        /// </summary>
        public async Task<MaterialInventoryDto> StockInAsync(StockInMaterialDto input)
        {
            try
            {
                _logger.LogInformation("开始物料入库，物料ID：{MaterialId}，数量：{Quantity}", input.MaterialId, input.Quantity);
                var inventory = await _inventoryManager.StockInAsync(
                    input.MaterialId,
                    input.WarehouseId,
                    input.LocationId,
                    input.Quantity,
                    input.BatchNo);
                _logger.LogInformation("物料入库成功，库存记录ID：{Id}", inventory.Id);
                return ObjectMapper.Map<MaterialInventory, MaterialInventoryDto>(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "物料入库失败，物料ID：{MaterialId}", input.MaterialId);
                throw new UserFriendlyException("物料入库失败", ex);
            }
        }

        /// <summary>
        /// 出库
        /// </summary>
        public async Task<MaterialInventoryDto> StockOutAsync(StockOutMaterialDto input)
        {
            try
            {
                _logger.LogInformation("开始物料出库，物料ID：{MaterialId}，数量：{Quantity}", input.MaterialId, input.Quantity);
                var inventory = await _inventoryManager.StockOutAsync(
                    input.MaterialId,
                    input.WarehouseId,
                    input.LocationId,
                    input.Quantity,
                    input.BatchNo);
                _logger.LogInformation("物料出库成功，库存记录ID：{Id}", inventory.Id);
                return ObjectMapper.Map<MaterialInventory, MaterialInventoryDto>(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "物料出库失败，物料ID：{MaterialId}", input.MaterialId);
                throw new UserFriendlyException("物料出库失败", ex);
            }
        }

        /// <summary>
        /// 库存调拨
        /// </summary>
        public async Task<MaterialInventoryDto> TransferAsync(TransferMaterialDto input)
        {
            try
            {
                _logger.LogInformation("开始物料调拨，物料ID：{MaterialId}", input.MaterialId);
                var inventory = await _inventoryManager.TransferAsync(
                    input.MaterialId,
                    input.FromWarehouseId,
                    input.ToWarehouseId,
                    input.FromLocationId,
                    input.ToLocationId,
                    input.Quantity,
                    input.BatchNo);
                _logger.LogInformation("物料调拨成功，库存记录ID：{Id}", inventory.Id);
                return ObjectMapper.Map<MaterialInventory, MaterialInventoryDto>(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "物料调拨失败，物料ID：{MaterialId}", input.MaterialId);
                throw new UserFriendlyException("物料调拨失败", ex);
            }
        }

        /// <summary>
        /// 获取物料库存
        /// </summary>
        public async Task<MaterialInventoryDto> GetInventoryAsync(Guid materialId, Guid warehouseId, Guid? locationId = null)
        {
            var inventory = await _inventoryManager.GetInventoryAsync(materialId, warehouseId, locationId);
            return ObjectMapper.Map<MaterialInventory, MaterialInventoryDto>(inventory);
        }

        /// <summary>
        /// 获取物料库存列表
        /// </summary>
        public async Task<List<MaterialInventoryDto>> GetInventoryListAsync(Guid materialId)
        {
            var inventories = await _inventoryManager.GetInventoryListAsync(materialId);
            return ObjectMapper.Map<List<MaterialInventory>, List<MaterialInventoryDto>>(inventories);
        }
    }