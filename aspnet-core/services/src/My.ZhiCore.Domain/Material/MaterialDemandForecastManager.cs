using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Material
{
    /// <summary>
    /// 物料需求预测管理器，负责处理需求预测的业务逻辑和生命周期管理
    /// </summary>
    public class MaterialDemandForecastManager : DomainService
    {
        private readonly IRepository<MaterialDemandForecast, Guid> _forecastRepository;
        private readonly IRepository<Material, Guid> _materialRepository;
        private readonly ILocalEventBus _localEventBus;

        public MaterialDemandForecastManager(
            IRepository<MaterialDemandForecast, Guid> forecastRepository,
            IRepository<Material, Guid> materialRepository,
            ILocalEventBus localEventBus)
        {
            _forecastRepository = forecastRepository;
            _materialRepository = materialRepository;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建新的需求预测
        /// </summary>
        public async Task<MaterialDemandForecast> CreateForecastAsync(
            Guid materialId,
            int forecastPeriod,
            DateTime forecastStartDate,
            string forecastMethod)
        {
            await ValidateMaterialExistsAsync(materialId);
            ValidateForecastPeriod(forecastPeriod);
            ValidateForecastMethod(forecastMethod);
            ValidateForecastStartDate(forecastStartDate);

            var forecast = new MaterialDemandForecast(
                GuidGenerator.Create(),
                materialId,
                forecastPeriod,
                forecastStartDate,
                forecastMethod);

            await _forecastRepository.InsertAsync(forecast);
            return forecast;
        }

        /// <summary>
        /// 更新预测结果
        /// </summary>
        public async Task UpdateForecastResultAsync(
            Guid forecastId,
            decimal forecastQuantity,
            decimal seasonalityFactor,
            decimal suggestedReplenishmentQuantity,
            DateTime? suggestedReplenishmentDate)
        {
            var forecast = await _forecastRepository.GetAsync(forecastId);
            ValidateForecastQuantity(forecastQuantity);
            ValidateSeasonalityFactor(seasonalityFactor);
            ValidateSuggestedReplenishmentQuantity(suggestedReplenishmentQuantity);

            forecast.UpdateForecastResult(
                forecastQuantity,
                seasonalityFactor,
                suggestedReplenishmentQuantity,
                suggestedReplenishmentDate);

            await _forecastRepository.UpdateAsync(forecast);
            await _localEventBus.PublishAsync(new ForecastCompletedEto
            {
                ForecastId = forecastId,
                MaterialId = forecast.MaterialId,
                ForecastQuantity = forecastQuantity
            });
        }

        /// <summary>
        /// 验证预测结果
        /// </summary>
        public async Task ValidateForecastAsync(Guid forecastId, decimal actualQuantity)
        {
            var forecast = await _forecastRepository.GetAsync(forecastId);
            ValidateActualQuantity(actualQuantity);

            forecast.ValidateForecast(actualQuantity);
            await _forecastRepository.UpdateAsync(forecast);
            await _localEventBus.PublishAsync(new ForecastValidatedEto
            {
                ForecastId = forecastId,
                MaterialId = forecast.MaterialId,
                ActualQuantity = actualQuantity,
                ForecastAccuracy = forecast.ForecastAccuracy
            });
        }

        #region Validation Methods

        private async Task ValidateMaterialExistsAsync(Guid materialId)
        {
            if (!await _materialRepository.AnyAsync(m => m.Id == materialId))
            {
                throw new ArgumentException("指定的物料不存在", nameof(materialId));
            }
        }

        private void ValidateForecastPeriod(int forecastPeriod)
        {
            if (forecastPeriod <= 0 || forecastPeriod > 365)
            {
                throw new ArgumentException("预测周期必须在1到365天之间", nameof(forecastPeriod));
            }
        }

        private void ValidateForecastMethod(string forecastMethod)
        {
            if (string.IsNullOrEmpty(forecastMethod))
            {
                throw new ArgumentException("预测方法不能为空", nameof(forecastMethod));
            }

            // 可以添加更多预测方法的验证逻辑
        }

        private void ValidateForecastStartDate(DateTime forecastStartDate)
        {
            if (forecastStartDate.Date < DateTime.Now.Date)
            {
                throw new ArgumentException("预测开始日期不能早于当前日期", nameof(forecastStartDate));
            }
        }

        private void ValidateForecastQuantity(decimal forecastQuantity)
        {
            if (forecastQuantity < 0)
            {
                throw new ArgumentException("预测数量不能为负数", nameof(forecastQuantity));
            }
        }

        private void ValidateSeasonalityFactor(decimal seasonalityFactor)
        {
            if (seasonalityFactor <= 0)
            {
                throw new ArgumentException("季节性因子必须大于0", nameof(seasonalityFactor));
            }
        }

        private void ValidateSuggestedReplenishmentQuantity(decimal suggestedReplenishmentQuantity)
        {
            if (suggestedReplenishmentQuantity < 0)
            {
                throw new ArgumentException("建议补货量不能为负数", nameof(suggestedReplenishmentQuantity));
            }
        }

        private void ValidateActualQuantity(decimal actualQuantity)
        {
            if (actualQuantity < 0)
            {
                throw new ArgumentException("实际数量不能为负数", nameof(actualQuantity));
            }
        }

        #endregion
    }

    /// <summary>
    /// 预测完成事件数据传输对象
    /// </summary>
    public class ForecastCompletedEto
    {
        public Guid ForecastId { get; set; }
        public Guid MaterialId { get; set; }
        public decimal ForecastQuantity { get; set; }
    }

    /// <summary>
    /// 预测验证事件数据传输对象
    /// </summary>
    public class ForecastValidatedEto
    {
        public Guid ForecastId { get; set; }
        public Guid MaterialId { get; set; }
        public decimal ActualQuantity { get; set; }
        public decimal ForecastAccuracy { get; set; }
    }
}