using Volo.Abp.Application.Services;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产执行应用服务
    /// </summary>
    public class ProductionExecutionAppService : ApplicationService
    {
        private readonly ProductionManager _productionManager;

        public ProductionExecutionAppService(ProductionManager productionManager)
        {
            _productionManager = productionManager;
        }

        /// <summary>
        /// 开始生产
        /// </summary>
        public async Task<Production> StartProductionAsync(Guid id)
        {
            return await _productionManager.StartProductionAsync(id);
        }

        /// <summary>
        /// 暂停生产
        /// </summary>
        public async Task<Production> PauseProductionAsync(Guid id)
        {
            return await _productionManager.PauseProductionAsync(id);
        }

        /// <summary>
        /// 恢复生产
        /// </summary>
        public async Task<Production> ResumeProductionAsync(Guid id)
        {
            return await _productionManager.ResumeProductionAsync(id);
        }

        /// <summary>
        /// 完成生产
        /// </summary>
        public async Task<Production> CompleteProductionAsync(Guid id)
        {
            return await _productionManager.CompleteProductionAsync(id);
        }

        /// <summary>
        /// 更新生产进度
        /// </summary>
        public async Task<Production> UpdateProgressAsync(Guid id, int progress)
        {
            return await _productionManager.UpdateProgressAsync(id, progress);
        }
    }
}