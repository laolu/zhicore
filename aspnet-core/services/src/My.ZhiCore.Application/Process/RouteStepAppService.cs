using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工艺路线步骤管理应用服务
    /// </summary>
    public class RouteStepAppService : ApplicationService
    {
        private readonly RouteManager _routeManager;

        public RouteStepAppService(RouteManager routeManager)
        {
            _routeManager = routeManager;
        }

        /// <summary>
        /// 添加工艺步骤
        /// </summary>
        public async Task<RouteStepDto> AddStepAsync(Guid routeId, CreateRouteStepDto input)
        {
            var step = ObjectMapper.Map<CreateRouteStepDto, RouteStep>(input);
            var createdStep = await _routeManager.AddStepAsync(routeId, step);
            return ObjectMapper.Map<RouteStep, RouteStepDto>(createdStep);
        }

        /// <summary>
        /// 更新工艺步骤
        /// </summary>
        public async Task<RouteStepDto> UpdateStepAsync(Guid stepId, UpdateRouteStepDto input)
        {
            var step = await _routeManager.GetStepAsync(stepId);
            ObjectMapper.Map(input, step);
            var updatedStep = await _routeManager.UpdateStepAsync(stepId, step);
            return ObjectMapper.Map<RouteStep, RouteStepDto>(updatedStep);
        }

        /// <summary>
        /// 删除工艺步骤
        /// </summary>
        public async Task DeleteStepAsync(Guid stepId)
        {
            await _routeManager.DeleteStepAsync(stepId);
        }

        /// <summary>
        /// 获取工艺步骤列表
        /// </summary>
        public async Task<List<RouteStepDto>> GetStepsAsync(Guid routeId)
        {
            var steps = await _routeManager.GetStepsAsync(routeId);
            return ObjectMapper.Map<List<RouteStep>, List<RouteStepDto>>(steps);
        }

        /// <summary>
        /// 获取工艺步骤详情
        /// </summary>
        public async Task<RouteStepDto> GetStepAsync(Guid stepId)
        {
            var step = await _routeManager.GetStepAsync(stepId);
            return ObjectMapper.Map<RouteStep, RouteStepDto>(step);
        }

        /// <summary>
        /// 设置步骤顺序
        /// </summary>
        public async Task SetStepOrderAsync(Guid stepId, int order)
        {
            await _routeManager.SetStepOrderAsync(stepId, order);
        }
    }
}