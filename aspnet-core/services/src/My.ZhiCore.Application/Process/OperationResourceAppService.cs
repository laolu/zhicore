using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using My.ZhiCore.Process.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序资源应用服务
    /// </summary>
    public class OperationResourceAppService : ApplicationService, IOperationResourceAppService
    {
        private readonly OperationResourceManager _resourceManager;
        private readonly ILogger<OperationResourceAppService> _logger;

        public OperationResourceAppService(
            OperationResourceManager resourceManager,
            ILogger<OperationResourceAppService> logger)
        {
            _resourceManager = resourceManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取工序资源列表
        /// </summary>
        public async Task<List<OperationResourceDto>> GetOperationResourcesAsync(Guid operationId)
        {
            if (operationId == Guid.Empty)
            {
                throw new UserFriendlyException("工序ID不能为空");
            }

            try
            {
                _logger.LogInformation("获取工序资源列表，工序ID：{OperationId}", operationId);
                var resources = await _resourceManager.GetOperationResourcesAsync(operationId);
                return ObjectMapper.Map<List<OperationResource>, List<OperationResourceDto>>(resources);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取工序资源列表失败，工序ID：{OperationId}", operationId);
                throw new UserFriendlyException("获取工序资源列表失败", ex);
            }
        }
    }
}