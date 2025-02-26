using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Process.Managers
{
    /// <summary>
    /// 工艺路线管理器
    /// </summary>
    public class RouteManager : DomainService
    {
        private readonly IRepository<Route, Guid> _routeRepository;
        private readonly IRepository<RouteVersion, Guid> _routeVersionRepository;
        private readonly IRepository<RouteStep, Guid> _routeStepRepository;

        public RouteManager(
            IRepository<Route, Guid> routeRepository,
            IRepository<RouteVersion, Guid> routeVersionRepository,
            IRepository<RouteStep, Guid> routeStepRepository)
        {
            _routeRepository = routeRepository;
            _routeVersionRepository = routeVersionRepository;
            _routeStepRepository = routeStepRepository;
        }

        /// <summary>
        /// 创建工艺路线
        /// </summary>
        public async Task<Route> CreateRouteAsync(string code, string name, string description = null)
        {
            var route = new Route(GuidGenerator.Create(), code, name, description);
            await _routeRepository.InsertAsync(route);
            return route;
        }

        /// <summary>
        /// 创建工艺路线版本
        /// </summary>
        public async Task<RouteVersion> CreateRouteVersionAsync(Guid routeId, string version, string description = null)
        {
            var routeVersion = new RouteVersion(GuidGenerator.Create(), routeId, version, description);
            await _routeVersionRepository.InsertAsync(routeVersion);
            return routeVersion;
        }

        /// <summary>
        /// 设置当前版本
        /// </summary>
        public async Task SetCurrentVersionAsync(Guid routeId, Guid versionId)
        {
            var route = await _routeRepository.GetAsync(routeId);
            var versions = await _routeVersionRepository.GetListAsync(v => v.RouteId == routeId);

            foreach (var version in versions)
            {
                version.IsCurrent = version.Id == versionId;
                await _routeVersionRepository.UpdateAsync(version);
            }

            route.CurrentVersion = await _routeVersionRepository.GetAsync(versionId);
            await _routeRepository.UpdateAsync(route);
        }

        /// <summary>
        /// 添加工艺步骤
        /// </summary>
        public async Task<RouteStep> AddStepAsync(Guid routeVersionId, Guid operationId, int sequence, string name, string description = null)
        {
            var step = new RouteStep(GuidGenerator.Create(), routeVersionId, operationId, sequence, name, description);
            await _routeStepRepository.InsertAsync(step);
            return step;
        }
    }
}