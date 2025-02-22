using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工艺路线基础应用服务
    /// </summary>
    public class RouteBaseAppService : ApplicationService
    {
        private readonly RouteManager _routeManager;

        public RouteBaseAppService(RouteManager routeManager)
        {
            _routeManager = routeManager;
        }

        /// <summary>
        /// 创建工艺路线
        /// </summary>
        public async Task<RouteDto> CreateRouteAsync(CreateRouteDto input)
        {
            var route = await _routeManager.CreateRouteAsync(input.Code, input.Name, input.Description);
            return ObjectMapper.Map<Route, RouteDto>(route);
        }

        /// <summary>
        /// 更新工艺路线
        /// </summary>
        public async Task<RouteDto> UpdateRouteAsync(Guid id, UpdateRouteDto input)
        {
            var route = await _routeManager.GetRouteAsync(id);
            ObjectMapper.Map(input, route);
            await _routeManager.UpdateRouteAsync(id, route);
            return ObjectMapper.Map<Route, RouteDto>(route);
        }

        /// <summary>
        /// 删除工艺路线
        /// </summary>
        public async Task DeleteRouteAsync(Guid id)
        {
            await _routeManager.DeleteRouteAsync(id);
        }

        /// <summary>
        /// 获取工艺路线列表
        /// </summary>
        public async Task<List<RouteDto>> GetRoutesAsync()
        {
            var routes = await _routeManager.GetRoutesAsync();
            return ObjectMapper.Map<List<Route>, List<RouteDto>>(routes);
        }

        /// <summary>
        /// 获取工艺路线详情
        /// </summary>
        public async Task<RouteDto> GetRouteAsync(Guid id)
        {
            var route = await _routeManager.GetRouteAsync(id);
            return ObjectMapper.Map<Route, RouteDto>(route);
        }

        /// <summary>
        /// 启用工艺路线
        /// </summary>
        public async Task<bool> EnableRouteAsync(Guid id)
        {
            return await _routeManager.EnableRouteAsync(id);
        }

        /// <summary>
        /// 禁用工艺路线
        /// </summary>
        public async Task<bool> DisableRouteAsync(Guid id)
        {
            return await _routeManager.DisableRouteAsync(id);
        }
    }
}