using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序资源管理器 - 负责处理工序资源相关的领域逻辑和事件
    /// </summary>
    public class OperationResourceManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationResource, Guid> _operationResourceRepository;
        private readonly IRepository<OperationResourceExecution, Guid> _resourceExecutionRepository;

        public OperationResourceManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationResource, Guid> operationResourceRepository,
            IRepository<OperationResourceExecution, Guid> resourceExecutionRepository)
        {
            _operationRepository = operationRepository;
            _operationResourceRepository = operationResourceRepository;
            _resourceExecutionRepository = resourceExecutionRepository;
        }

        /// <summary>
        /// 分配工序资源
        /// </summary>
        public async Task AssignResourcesAsync(Guid operationId, List<OperationResource> resources)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            
            foreach (var resource in resources)
            {
                resource.OperationId = operationId;
                await _operationResourceRepository.InsertAsync(resource);
            }

            await LocalEventBus.PublishAsync(
                new OperationResourcesAssignedEto
                {
                    OperationId = operationId,
                    ResourceIds = resources.Select(r => r.Id).ToList()
                });
        }

        /// <summary>
        /// 记录资源执行情况
        /// </summary>
        public async Task RecordResourceExecutionAsync(
            Guid resourceId,
            Guid executionId,
            OperationResourceExecutionStatus status)
        {
            var resourceExecution = new OperationResourceExecution
            {
                ResourceId = resourceId,
                ExecutionId = executionId,
                Status = status,
                ExecutionTime = Clock.Now
            };

            await _resourceExecutionRepository.InsertAsync(resourceExecution);

            await LocalEventBus.PublishAsync(
                new OperationResourceExecutionRecordedEto
                {
                    ResourceId = resourceId,
                    ExecutionId = executionId,
                    Status = status,
                    ExecutionTime = resourceExecution.ExecutionTime
                });
        }

        /// <summary>
        /// 获取工序资源列表
        /// </summary>
        public async Task<List<OperationResource>> GetOperationResourcesAsync(Guid operationId)
        {
            return await _operationResourceRepository.GetListAsync(r => r.OperationId == operationId);
        }

        /// <summary>
        /// 移除工序资源
        /// </summary>
        public async Task RemoveResourceAsync(Guid resourceId)
        {
            var resource = await _operationResourceRepository.GetAsync(resourceId);
            await _operationResourceRepository.DeleteAsync(resource);

            await LocalEventBus.PublishAsync(
                new OperationResourceRemovedEto
                {
                    ResourceId = resourceId,
                    OperationId = resource.OperationId
                });
        }
    }
}