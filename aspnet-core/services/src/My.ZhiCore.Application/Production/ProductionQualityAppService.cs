using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产质量服务
    /// </summary>
    public class ProductionQualityAppService : ApplicationService
    {
        private readonly ProductionManager _productionManager;

        public ProductionQualityAppService(ProductionManager productionManager)
        {
            _productionManager = productionManager;
        }

        /// <summary>
        /// 创建质量检查记录
        /// </summary>
        public async Task<QualityCheck> CreateQualityCheckAsync(QualityCheck qualityCheck)
        {
            return await _productionManager.CreateQualityCheckAsync(qualityCheck);
        }

        /// <summary>
        /// 更新质量检查记录
        /// </summary>
        public async Task<QualityCheck> UpdateQualityCheckAsync(Guid id, QualityCheck qualityCheck)
        {
            return await _productionManager.UpdateQualityCheckAsync(id, qualityCheck);
        }

        /// <summary>
        /// 获取质量检查记录
        /// </summary>
        public async Task<QualityCheck> GetQualityCheckAsync(Guid id)
        {
            return await _productionManager.GetQualityCheckAsync(id);
        }

        /// <summary>
        /// 获取质量检查记录列表
        /// </summary>
        public async Task<List<QualityCheck>> GetQualityCheckListAsync(Guid productionId)
        {
            return await _productionManager.GetQualityCheckListAsync(productionId);
        }

        /// <summary>
        /// 记录不合格品
        /// </summary>
        public async Task<DefectiveProduct> RecordDefectiveProductAsync(DefectiveProduct defectiveProduct)
        {
            return await _productionManager.RecordDefectiveProductAsync(defectiveProduct);
        }

        /// <summary>
        /// 获取不合格品列表
        /// </summary>
        public async Task<List<DefectiveProduct>> GetDefectiveProductListAsync(Guid productionId)
        {
            return await _productionManager.GetDefectiveProductListAsync(productionId);
        }

        /// <summary>
        /// 获取质量统计数据
        /// </summary>
        public async Task<QualityStatistics> GetQualityStatisticsAsync(Guid productionId)
        {
            return await _productionManager.GetQualityStatisticsAsync(productionId);
        }
    }
}