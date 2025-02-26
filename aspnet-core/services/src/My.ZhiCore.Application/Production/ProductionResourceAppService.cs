using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产资源服务
    /// </summary>
    public class ProductionResourceAppService : ApplicationService
    {
        private readonly ProductionManager _productionManager;

        public ProductionResourceAppService(ProductionManager productionManager)
        {
            _productionManager = productionManager;
        }

        /// <summary>
        /// 分配生产资源
        /// </summary>
        public async Task<ProductionResource> AllocateResourceAsync(Guid productionId, ResourceAllocationRequest request)
        {
            return await _productionManager.AllocateProductionResourceAsync(productionId, request);
        }

        /// <summary>
        /// 释放生产资源
        /// </summary>
        public async Task ReleaseResourceAsync(Guid resourceId)
        {
            await _productionManager.ReleaseProductionResourceAsync(resourceId);
        }

        /// <summary>
        /// 更新资源状态
        /// </summary>
        public async Task<ProductionResource> UpdateResourceStatusAsync(Guid resourceId, ResourceStatus status)
        {
            return await _productionManager.UpdateResourceStatusAsync(resourceId, status);
        }

        /// <summary>
        /// 获取资源详情
        /// </summary>
        public async Task<ProductionResource> GetResourceAsync(Guid resourceId)
        {
            return await _productionManager.GetProductionResourceAsync(resourceId);
        }

        /// <summary>
        /// 获取可用资源列表
        /// </summary>
        public async Task<List<ProductionResource>> GetAvailableResourcesAsync(ResourceType type)
        {
            return await _productionManager.GetAvailableProductionResourcesAsync(type);
        }

        /// <summary>
        /// 获取资源使用记录
        /// </summary>
        public async Task<List<ResourceUsageRecord>> GetResourceUsageRecordsAsync(Guid resourceId, DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetResourceUsageRecordsAsync(resourceId, startTime, endTime);
        }

        /// <summary>
        /// 获取资源利用率
        /// </summary>
        public async Task<double> GetResourceUtilizationRateAsync(Guid resourceId, DateTime startTime, DateTime endTime)
        {
            return await _productionManager.GetResourceUtilizationRateAsync(resourceId, startTime, endTime);
        }
    }
}