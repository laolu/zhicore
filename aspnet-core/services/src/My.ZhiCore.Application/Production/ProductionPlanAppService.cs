using Volo.Abp.Application.Services;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产计划应用服务
    /// </summary>
    public class ProductionPlanAppService : ApplicationService
    {
        private readonly ProductionManager _productionManager;

        public ProductionPlanAppService(ProductionManager productionManager)
        {
            _productionManager = productionManager;
        }

        /// <summary>
        /// 创建生产计划
        /// </summary>
        public async Task<Production> CreatePlanAsync(Production production)
        {
            return await _productionManager.CreateProductionAsync(production);
        }

        /// <summary>
        /// 更新生产计划
        /// </summary>
        public async Task<Production> UpdatePlanAsync(Guid id, Production production)
        {
            return await _productionManager.UpdateProductionAsync(id, production);
        }

        /// <summary>
        /// 删除生产计划
        /// </summary>
        public async Task DeletePlanAsync(Guid id)
        {
            await _productionManager.DeleteProductionAsync(id);
        }

        /// <summary>
        /// 获取生产计划
        /// </summary>
        public async Task<Production> GetPlanAsync(Guid id)
        {
            return await _productionManager.GetProductionAsync(id);
        }

        /// <summary>
        /// 获取生产计划列表
        /// </summary>
        public async Task<List<Production>> GetPlanListAsync()
        {
            return await _productionManager.GetProductionListAsync();
        }
    }
}