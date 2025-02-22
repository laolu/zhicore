using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工艺路线版本应用服务
    /// </summary>
    public class RouteVersionAppService : ApplicationService
    {
        private readonly RouteManager _routeManager;

        public RouteVersionAppService(RouteManager routeManager)
        {
            _routeManager = routeManager;
        }

        /// <summary>
        /// 创建工艺路线版本
        /// </summary>
        public async Task<RouteVersionDto> CreateVersionAsync(Guid routeId, CreateRouteVersionDto input)
        {
            var version = new RouteVersion(Guid.NewGuid(), routeId, input.Version, input.Description);
            var createdVersion = await _routeManager.CreateVersionAsync(routeId, version);
            return ObjectMapper.Map<RouteVersion, RouteVersionDto>(createdVersion);
        }

        /// <summary>
        /// 更新工艺路线版本
        /// </summary>
        public async Task<RouteVersionDto> UpdateVersionAsync(Guid versionId, UpdateRouteVersionDto input)
        {
            var version = await _routeManager.GetVersionAsync(versionId);
            ObjectMapper.Map(input, version);
            var updatedVersion = await _routeManager.UpdateVersionAsync(versionId, version);
            return ObjectMapper.Map<RouteVersion, RouteVersionDto>(updatedVersion);
        }

        /// <summary>
        /// 删除工艺路线版本
        /// </summary>
        public async Task DeleteVersionAsync(Guid versionId)
        {
            await _routeManager.DeleteVersionAsync(versionId);
        }

        /// <summary>
        /// 获取工艺路线版本列表
        /// </summary>
        public async Task<List<RouteVersionDto>> GetVersionsAsync(Guid routeId)
        {
            var versions = await _routeManager.GetVersionsAsync(routeId);
            return ObjectMapper.Map<List<RouteVersion>, List<RouteVersionDto>>(versions);
        }

        /// <summary>
        /// 获取工艺路线版本详情
        /// </summary>
        public async Task<RouteVersionDto> GetVersionAsync(Guid versionId)
        {
            var version = await _routeManager.GetVersionAsync(versionId);
            return ObjectMapper.Map<RouteVersion, RouteVersionDto>(version);
        }
    }
}