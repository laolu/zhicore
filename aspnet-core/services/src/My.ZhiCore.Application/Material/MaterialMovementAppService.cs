using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料移动应用服务
    /// </summary>
    public class MaterialMovementAppService : ApplicationService
    {
        private readonly MaterialInventoryManager _inventoryManager;
        private readonly ILogger<MaterialMovementAppService> _logger;

        public MaterialMovementAppService(
            MaterialInventoryManager inventoryManager,
            ILogger<MaterialMovementAppService> logger)
        {
            _inventoryManager = inventoryManager;
            _logger = logger;
        }

        /// <summary>
        /// 记录物料移动
        /// </summary>
        public async Task<MaterialMovementRecordDto> RecordMovementAsync(CreateMaterialMovementDto input)
        {
            try
            {
                _logger.LogInformation("开始记录物料移动，物料ID：{MaterialId}", input.MaterialId);
                var record = await _inventoryManager.RecordMaterialMovementAsync(
                    input.MaterialId,
                    input.FromWarehouseId,
                    input.ToWarehouseId,
                    input.FromLocationId,
                    input.ToLocationId,
                    input.Quantity,
                    input.MovementType,
                    input.Remark);
                _logger.LogInformation("物料移动记录成功，记录ID：{Id}", record.Id);
                return ObjectMapper.Map<MaterialMovementRecord, MaterialMovementRecordDto>(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "记录物料移动失败，物料ID：{MaterialId}", input.MaterialId);
                throw new UserFriendlyException("记录物料移动失败", ex);
            }
        }

        /// <summary>
        /// 获取物料移动记录列表
        /// </summary>
        public async Task<List<MaterialMovementRecordDto>> GetMovementRecordsAsync(
            Guid materialId,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            try
            {
                _logger.LogInformation("开始获取物料移动记录，物料ID：{MaterialId}", materialId);
                var records = await _inventoryManager.GetMaterialMovementRecordsAsync(
                    materialId,
                    startDate,
                    endDate);
                _logger.LogInformation("获取物料移动记录成功");
                return ObjectMapper.Map<List<MaterialMovementRecord>, List<MaterialMovementRecordDto>>(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取物料移动记录失败，物料ID：{MaterialId}", materialId);
                throw new UserFriendlyException("获取物料移动记录失败", ex);
            }
        }

        /// <summary>
        /// 获取仓库物料移动记录列表
        /// </summary>
        public async Task<List<MaterialMovementRecordDto>> GetWarehouseMovementRecordsAsync(
            Guid warehouseId,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            try
            {
                _logger.LogInformation("开始获取仓库物料移动记录，仓库ID：{WarehouseId}", warehouseId);
                var records = await _inventoryManager.GetWarehouseMovementRecordsAsync(
                    warehouseId,
                    startDate,
                    endDate);
                _logger.LogInformation("获取仓库物料移动记录成功");
                return ObjectMapper.Map<List<MaterialMovementRecord>, List<MaterialMovementRecordDto>>(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取仓库物料移动记录失败，仓库ID：{WarehouseId}", warehouseId);
                throw new UserFriendlyException("获取仓库物料移动记录失败", ex);
            }
        }
    }
}