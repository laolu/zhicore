using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序关系管理器 - 负责管理工序之间的关联和依赖关系
    /// </summary>
    public class OperationRelationshipManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationRelationship, Guid> _relationshipRepository;

        public OperationRelationshipManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationRelationship, Guid> relationshipRepository)
        {
            _operationRepository = operationRepository;
            _relationshipRepository = relationshipRepository;
        }

        /// <summary>
        /// 创建工序关系
        /// </summary>
        public async Task CreateRelationshipAsync(
            Guid sourceOperationId,
            Guid targetOperationId,
            OperationRelationshipType relationshipType)
        {
            var sourceOperation = await _operationRepository.GetAsync(sourceOperationId);
            var targetOperation = await _operationRepository.GetAsync(targetOperationId);

            var relationship = new OperationRelationship
            {
                SourceOperationId = sourceOperationId,
                TargetOperationId = targetOperationId,
                RelationshipType = relationshipType,
                CreationTime = Clock.Now
            };

            await _relationshipRepository.InsertAsync(relationship);

            await LocalEventBus.PublishAsync(
                new OperationRelationshipCreatedEto
                {
                    Id = relationship.Id,
                    SourceOperationId = relationship.SourceOperationId,
                    TargetOperationId = relationship.TargetOperationId,
                    RelationshipType = relationship.RelationshipType
                });
        }

        /// <summary>
        /// 删除工序关系
        /// </summary>
        public async Task DeleteRelationshipAsync(
            Guid sourceOperationId,
            Guid targetOperationId,
            OperationRelationshipType relationshipType)
        {
            var relationship = await _relationshipRepository.FirstOrDefaultAsync(
                r => r.SourceOperationId == sourceOperationId &&
                      r.TargetOperationId == targetOperationId &&
                      r.RelationshipType == relationshipType);

            if (relationship != null)
            {
                await _relationshipRepository.DeleteAsync(relationship);

                await LocalEventBus.PublishAsync(
                    new OperationRelationshipDeletedEto
                    {
                        Id = relationship.Id,
                        SourceOperationId = relationship.SourceOperationId,
                        TargetOperationId = relationship.TargetOperationId,
                        RelationshipType = relationship.RelationshipType
                    });
            }
        }

        /// <summary>
        /// 获取工序的所有前置工序
        /// </summary>
        public async Task<List<Operation>> GetPrecedingOperationsAsync(Guid operationId)
        {
            var relationships = await _relationshipRepository.GetListAsync(
                r => r.TargetOperationId == operationId &&
                      r.RelationshipType == OperationRelationshipType.Precedence);

            var operations = new List<Operation>();
            foreach (var relationship in relationships)
            {
                var operation = await _operationRepository.GetAsync(relationship.SourceOperationId);
                operations.Add(operation);
            }

            return operations;
        }

        /// <summary>
        /// 获取工序的所有后续工序
        /// </summary>
        public async Task<List<Operation>> GetSucceedingOperationsAsync(Guid operationId)
        {
            var relationships = await _relationshipRepository.GetListAsync(
                r => r.SourceOperationId == operationId &&
                      r.RelationshipType == OperationRelationshipType.Precedence);

            var operations = new List<Operation>();
            foreach (var relationship in relationships)
            {
                var operation = await _operationRepository.GetAsync(relationship.TargetOperationId);
                operations.Add(operation);
            }

            return operations;
        }

        /// <summary>
        /// 检查两个工序之间是否存在指定类型的关系
        /// </summary>
        public async Task<bool> HasRelationshipAsync(
            Guid sourceOperationId,
            Guid targetOperationId,
            OperationRelationshipType relationshipType)
        {
            return await _relationshipRepository.AnyAsync(
                r => r.SourceOperationId == sourceOperationId &&
                      r.TargetOperationId == targetOperationId &&
                      r.RelationshipType == relationshipType);
        }
    }
}