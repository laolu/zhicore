using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料需求预测应用服务
    /// </summary>
    public class MaterialDemandForecastAppService : ApplicationService
    {
        private readonly MaterialDemandForecastManager _forecastManager;
        private readonly ILogger<MaterialDemandForecastAppService> _logger;

        public MaterialDemandForecastAppService(
            MaterialDemandForecastManager forecastManager,
            ILogger<MaterialDemandForecastAppService> logger)
        {
            _forecastManager = forecastManager;
            _logger = logger;
        }

        /// <summary>
        /// 创建需求预测
        /// </summary>
        public async Task<MaterialDemandForecastDto> CreateForecastAsync(CreateMaterialDemandForecastDto input)
        {
            try
            {
                _logger.LogInformation("开始创建物料需求预测，物料ID：{MaterialId}", input.MaterialId);
                var forecast = await _forecastManager.CreateForecastAsync(
                    input.MaterialId,
                    input.ForecastDate,
                    input.ForecastQuantity,
                    input.Confidence,
                    input.AnalysisMethod,
                    input.Remark);
                _logger.LogInformation("物料需求预测创建成功，ID：{Id}", forecast.Id);
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
        public async Task<MaterialDemandForecastDto> UpdateForecastAsync(UpdateMaterialDemandForecastDto input)
        {
            try
            {
                _logger.LogInformation("开始更新物料需求预测，ID：{Id}", input.Id);
                var forecast = await _forecastManager.UpdateForecastAsync(
                    input.Id,
                    input.ForecastQuantity,
                    input.Confidence,
                    input.AnalysisMethod,
                    input.Remark);
                _logger.LogInformation("物料需求预测更新成功，ID：{Id}", forecast.Id);
                return ObjectMapper.Map<MaterialDemandForecast, MaterialDemandForecastDto>(forecast);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新物料需求预测失败，ID：{Id}", input.Id);
                throw new UserFriendlyException("更新物料需求预测失败", ex);
            }
        }

        /// <summary>
        /// 获取物料需求预测列表
        /// </summary>
        public async Task<List<MaterialDemandForecastDto>> GetForecastListAsync(Guid materialId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var forecasts = await _forecastManager.GetForecastListAsync(materialId, startDate, endDate);
            return ObjectMapper.Map<List<MaterialDemandForecast>, List<MaterialDemandForecastDto>>(forecasts);
        }

        /// <summary>
        /// 删除需求预测
        /// </summary>
        public async Task DeleteForecastAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("开始删除物料需求预测，ID：{Id}", id);
                await _forecastManager.DeleteForecastAsync(id);
                _logger.LogInformation("物料需求预测删除成功，ID：{Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除物料需求预测失败，ID：{Id}", id);
                throw new UserFriendlyException("删除物料需求预测失败", ex);
            }
        }
    }
}