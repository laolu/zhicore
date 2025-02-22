using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产工艺应用服务
    /// </summary>
    public class ProductionProcessAppService : ApplicationService
    {
        private readonly ProductionManager _productionManager;

        public ProductionProcessAppService(ProductionManager productionManager)
        {
            _productionManager = productionManager;
        }

        /// <summary>
        /// 创建工艺路线
        /// </summary>
        public async Task<ProcessRoute> CreateProcessRouteAsync(ProcessRoute processRoute)
        {
            return await _productionManager.CreateProcessRouteAsync(processRoute);
        }

        /// <summary>
        /// 更新工艺路线
        /// </summary>
        public async Task<ProcessRoute> UpdateProcessRouteAsync(Guid id, ProcessRoute processRoute)
        {
            return await _productionManager.UpdateProcessRouteAsync(id, processRoute);
        }

        /// <summary>
        /// 删除工艺路线
        /// </summary>
        public async Task DeleteProcessRouteAsync(Guid id)
        {
            await _productionManager.DeleteProcessRouteAsync(id);
        }

        /// <summary>
        /// 设置工艺参数
        /// </summary>
        public async Task<ProcessParameters> SetProcessParametersAsync(Guid processId, Dictionary<string, object> parameters)
        {
            return await _productionManager.SetProcessParametersAsync(processId, parameters);
        }

        /// <summary>
        /// 获取工艺参数
        /// </summary>
        public async Task<ProcessParameters> GetProcessParametersAsync(Guid processId)
        {
            return await _productionManager.GetProcessParametersAsync(processId);
        }

        /// <summary>
        /// 创建工艺版本
        /// </summary>
        public async Task<ProcessVersion> CreateProcessVersionAsync(Guid processId, ProcessVersion version)
        {
            return await _productionManager.CreateProcessVersionAsync(processId, version);
        }

        /// <summary>
        /// 获取工艺版本列表
        /// </summary>
        public async Task<List<ProcessVersion>> GetProcessVersionsAsync(Guid processId)
        {
            return await _productionManager.GetProcessVersionsAsync(processId);
        }

        /// <summary>
        /// 设置当前工艺版本
        /// </summary>
        public async Task<bool> SetCurrentProcessVersionAsync(Guid processId, Guid versionId)
        {
            return await _productionManager.SetCurrentProcessVersionAsync(processId, versionId);
        }
    }
}