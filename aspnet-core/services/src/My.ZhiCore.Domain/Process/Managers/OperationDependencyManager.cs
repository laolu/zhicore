using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序依赖管理器 - 负责管理工序之间的依赖关系
    /// </summary>
    public class OperationDependencyManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationDependency, Guid> _dependencyRepository;

        public OperationDependencyManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationDependency, Guid> dependencyRepository)
        {
            _operationRepository = operationRepository;
            _dependencyRepository = dependencyRepository;
        }

        /// <summary>
        /// 添加工序依赖关系
        /// </summary>
        public async Task<OperationDependency> AddDependencyAsync(
            Guid sourceOperationId,
            Guid targetOperationId,
            OperationDependencyType dependencyType,
            string description)
        {
            var sourceOperation = await _operationRepository.GetAsync(sourceOperationId);
            var targetOperation = await _operationRepository.GetAsync(targetOperationId);

            var dependency = new OperationDependency
            {
                SourceOperationId = sourceOperationId,
                TargetOperationId = targetOperationId,
                DependencyType = dependencyType,
                Description = description
            };

            await _dependencyRepository.InsertAsync(dependency);

            await LocalEventBus.PublishAsync(
                new OperationDependencyAddedEto
                {
                    Id = dependency.Id,
                    SourceOperationId = dependency.SourceOperationId,
                    TargetOperationId = dependency.TargetOperationId,
                    DependencyType = dependency.DependencyType
                });

            return dependency;
        }

        /// <summary>
        /// 移除工序依赖关系
        /// </summary>
        public async Task RemoveDependencyAsync(Guid dependencyId)
        {
            var dependency = await _dependencyRepository.GetAsync(dependencyId);
            await _dependencyRepository.DeleteAsync(dependency);

            await LocalEventBus.PublishAsync(
                new OperationDependencyRemovedEto
                {
                    Id = dependency.Id,
                    SourceOperationId = dependency.SourceOperationId,
                    TargetOperationId = dependency.TargetOperationId
                });
        }

        /// <summary>
        /// 获取工序的所有依赖工序
        /// </summary>
        public async Task<List<Operation>> GetDependenciesAsync(Guid operationId)
        {
            var dependencies = await _dependencyRepository
                .GetListAsync(x => x.SourceOperationId == operationId);

            var dependencyIds = dependencies.Select(x => x.TargetOperationId).ToList();
            return await _operationRepository.GetListAsync(x => dependencyIds.Contains(x.Id));
        }

        /// <summary>
        /// 获取依赖于指定工序的所有工序
        /// </summary>
        public async Task<List<Operation>> GetDependentOperationsAsync(Guid operationId)
        {
            var dependencies = await _dependencyRepository
                .GetListAsync(x => x.TargetOperationId == operationId);

            var dependentIds = dependencies.Select(x => x.SourceOperationId).ToList();
            return await _operationRepository.GetListAsync(x => dependentIds.Contains(x.Id));
        }
    }
}