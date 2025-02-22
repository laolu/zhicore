using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using My.ZhiCore.Process.Events;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序关联管理器 - 负责管理工序之间的关联关系和依赖关系
    /// </summary>
    public class OperationRelationManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationRelation, Guid> _operationRelationRepository;

        public OperationRelationManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationRelation, Guid> operationRelationRepository)
        {
            _operationRepository = operationRepository;
            _operationRelationRepository = operationRelationRepository;
        }

        /// <summary>
        /// 创建工序关联关系
        /// </summary>
        public async Task<OperationRelation> CreateRelationAsync(
            Guid sourceOperationId,
            Guid targetOperationId,
            OperationDependencyType dependencyType,
            string description)
        {
            var sourceOperation = await _operationRepository.GetAsync(sourceOperationId);
            var targetOperation = await _operationRepository.GetAsync(targetOperationId);

            var relation = new OperationRelation
            {
                SourceOperationId = sourceOperationId,
                TargetOperationId = targetOperationId,
                DependencyType = dependencyType,
                Description = description
            };

            await _operationRelationRepository.InsertAsync(relation);

            await LocalEventBus.PublishAsync(
                new OperationRelationCreatedEto
                {
                    Id = relation.Id,
                    SourceOperationId = relation.SourceOperationId,
                    TargetOperationId = relation.TargetOperationId,
                    DependencyType = relation.DependencyType
                });

            return relation;
        }

        /// <summary>
        /// 更新工序关联关系
        /// </summary>
        public async Task<OperationRelation> UpdateRelationAsync(
            Guid id,
            OperationDependencyType dependencyType,
            string description)
        {
            var relation = await _operationRelationRepository.GetAsync(id);

            relation.DependencyType = dependencyType;
            relation.Description = description;

            await _operationRelationRepository.UpdateAsync(relation);

            await LocalEventBus.PublishAsync(
                new OperationRelationUpdatedEto
                {
                    Id = relation.Id,
                    SourceOperationId = relation.SourceOperationId,
                    TargetOperationId = relation.TargetOperationId,
                    DependencyType = relation.DependencyType
                });

            return relation;
        }

        /// <summary>
        /// 删除工序关联关系
        /// </summary>
        public async Task DeleteRelationAsync(Guid relationId)
        {
            var relation = await _operationRelationRepository.GetAsync(relationId);
            await _operationRelationRepository.DeleteAsync(relation);

            await LocalEventBus.PublishAsync(
                new OperationRelationDeletedEto
                {
                    Id = relation.Id,
                    SourceOperationId = relation.SourceOperationId,
                    TargetOperationId = relation.TargetOperationId
                });
        }

        /// <summary>
        /// 获取工序的所有前置依赖工序
        /// </summary>
        public async Task<List<Operation>> GetPrecedingOperationsAsync(Guid operationId)
        {
            var relations = await _operationRelationRepository.GetListAsync(
                r => r.TargetOperationId == operationId);

            var operations = new List<Operation>();
            foreach (var relation in relations)
            {
                var operation = await _operationRepository.GetAsync(relation.SourceOperationId);
                operations.Add(operation);
            }

            return operations;
        }

        /// <summary>
        /// 获取工序的所有后续依赖工序
        /// </summary>
        public async Task<List<Operation>> GetSucceedingOperationsAsync(Guid operationId)
        {
            var relations = await _operationRelationRepository.GetListAsync(
                r => r.SourceOperationId == operationId);

            var operations = new List<Operation>();
            foreach (var relation in relations)
            {
                var operation = await _operationRepository.GetAsync(relation.TargetOperationId);
                operations.Add(operation);
            }

            return operations;
        }
    }
}