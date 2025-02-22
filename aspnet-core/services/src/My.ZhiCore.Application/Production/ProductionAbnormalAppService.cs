using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产异常服务
    /// </summary>
    public class ProductionAbnormalAppService : ApplicationService
    {
        private readonly ProductionManager _productionManager;

        public ProductionAbnormalAppService(ProductionManager productionManager)
        {
            _productionManager = productionManager;
        }

        /// <summary>
        /// 记录生产异常
        /// </summary>
        public async Task<ProductionAbnormal> RecordAbnormalAsync(ProductionAbnormal abnormal)
        {
            return await _productionManager.RecordProductionAbnormalAsync(abnormal);
        }

        /// <summary>
        /// 更新异常处理状态
        /// </summary>
        public async Task<ProductionAbnormal> UpdateAbnormalStatusAsync(Guid id, AbnormalStatus status)
        {
            return await _productionManager.UpdateAbnormalStatusAsync(id, status);
        }

        /// <summary>
        /// 添加异常处理记录
        /// </summary>
        public async Task<ProductionAbnormal> AddHandlingRecordAsync(Guid id, AbnormalHandlingRecord record)
        {
            return await _productionManager.AddAbnormalHandlingRecordAsync(id, record);
        }

        /// <summary>
        /// 获取异常详情
        /// </summary>
        public async Task<ProductionAbnormal> GetAbnormalAsync(Guid id)
        {
            return await _productionManager.GetProductionAbnormalAsync(id);
        }

        /// <summary>
        /// 获取异常列表
        /// </summary>
        public async Task<List<ProductionAbnormal>> GetAbnormalListAsync(DateTime? startTime = null, DateTime? endTime = null)
        {
            return await _productionManager.GetProductionAbnormalListAsync(startTime, endTime);
        }

        /// <summary>
        /// 获取未处理的异常列表
        /// </summary>
        public async Task<List<ProductionAbnormal>> GetPendingAbnormalListAsync()
        {
            return await _productionManager.GetPendingProductionAbnormalListAsync();
        }
    }
}