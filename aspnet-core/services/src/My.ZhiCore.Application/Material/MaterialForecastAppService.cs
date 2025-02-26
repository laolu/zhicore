using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料预测应用服务
    /// </summary>
    public class MaterialForecastAppService : ApplicationService
    {
        private readonly MaterialInventoryManager _inventoryManager;
        private readonly ILogger<MaterialForecastAppService> _logger;

        public MaterialForecastAppService(
            MaterialInventoryManager inventoryManager,
            ILogger<MaterialForecastAppService> logger)
        {
            _inventoryManager = inventoryManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建需求预测
        /// </summary>
        public async Task<MaterialDemandForecastDto> CreateDemandForecastAsync(CreateDemandForecastDto input)
        {
            try
            {
                _logger.LogInformation("开始创建物料需求预测，物料ID：{MaterialId}", input.MaterialId);
                var forecast = await _inventoryManager.CreateDemandForecastAsync(
                    input.MaterialId,
                    input.WarehouseId,
                    input.StartDate,
                    input.EndDate,
                    input.ForecastQuantity,
                    input.Confidence);
                _logger.LogInformation("物料需求预测创建成功，预测ID：{Id}", forecast.Id);
                return ObjectMapper.Map<MaterialDemandForecast, MaterialDemandForecastDto>(forecast);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建物料需求预测失败，物料ID：{MaterialId}", input.MaterialId);
                throw new UserFriendlyException("创建物料需求预测失败", ex);
            }
        }

        /// <summary>
        /// 更新需求预测
        /// </summary>
        public async Task<MaterialDemandForecastDto> UpdateDemandForecastAsync(UpdateDemandForecastDto input)
        {
            try
            {
                _logger.LogInformation("开始更新物料需求预测，预测ID：{ForecastId}", input.Id);
                var forecast = await _inventoryManager.UpdateDemandForecastAsync(
                    input.Id,
                    input.ForecastQuantity,
                    input.Confidence);
                _logger.LogInformation("物料需求预测更新成功，预测ID：{Id}", forecast.Id);
                return ObjectMapper.Map<MaterialDemandForecast, MaterialDemandForecastDto>(forecast);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新物料需求预测失败，预测ID：{ForecastId}", input.Id);
                throw new UserFriendlyException("更新物料需求预测失败", ex);
            }
        }

        /// <summary>
        /// 获取需求预测
        /// </summary>
        public async Task<MaterialDemandForecastDto> GetDemandForecastAsync(Guid forecastId)
        {
            try
            {
                var forecast = await _inventoryManager.GetDemandForecastAsync(forecastId);
                return ObjectMapper.Map<MaterialDemandForecast, MaterialDemandForecastDto>(forecast);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取物料需求预测失败，预测ID：{ForecastId}", forecastId);
                throw new UserFriendlyException("获取物料需求预测失败", ex);
            }
        }
    }
}