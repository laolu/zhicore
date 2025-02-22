using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序知识库管理器 - 负责管理工序相关的知识和经验
    /// </summary>
    public class OperationKnowledgeManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationKnowledge, Guid> _knowledgeRepository;

        public OperationKnowledgeManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationKnowledge, Guid> knowledgeRepository)
        {
            _operationRepository = operationRepository;
            _knowledgeRepository = knowledgeRepository;
        }

        /// <summary>
        /// 创建工序知识条目
        /// </summary>
        public async Task<OperationKnowledge> CreateKnowledgeAsync(
            Guid operationId,
            string title,
            string content,
            KnowledgeCategory category,
            string author)
        {
            var operation = await _operationRepository.GetAsync(operationId);

            var knowledge = new OperationKnowledge
            {
                OperationId = operationId,
                Title = title,
                Content = content,
                Category = category,
                Author = author,
                CreationTime = Clock.Now
            };

            await _knowledgeRepository.InsertAsync(knowledge);

            await LocalEventBus.PublishAsync(
                new OperationKnowledgeCreatedEto
                {
                    Id = knowledge.Id,
                    OperationId = knowledge.OperationId,
                    Title = knowledge.Title,
                    Category = knowledge.Category
                });

            return knowledge;
        }

        /// <summary>
        /// 更新工序知识条目
        /// </summary>
        public async Task<OperationKnowledge> UpdateKnowledgeAsync(
            Guid knowledgeId,
            string title,
            string content,
            KnowledgeCategory category)
        {
            var knowledge = await _knowledgeRepository.GetAsync(knowledgeId);

            knowledge.Title = title;
            knowledge.Content = content;
            knowledge.Category = category;
            knowledge.LastModificationTime = Clock.Now;

            await _knowledgeRepository.UpdateAsync(knowledge);

            await LocalEventBus.PublishAsync(
                new OperationKnowledgeUpdatedEto
                {
                    Id = knowledge.Id,
                    OperationId = knowledge.OperationId,
                    Title = knowledge.Title,
                    Category = knowledge.Category
                });

            return knowledge;
        }

        /// <summary>
        /// 验证工序知识条目
        /// </summary>
        public async Task ValidateKnowledgeAsync(
            Guid knowledgeId,
            string validatorName,
            string validationComments)
        {
            var knowledge = await _knowledgeRepository.GetAsync(knowledgeId);

            knowledge.IsValidated = true;
            knowledge.ValidatorName = validatorName;
            knowledge.ValidationComments = validationComments;
            knowledge.ValidationTime = Clock.Now;

            await _knowledgeRepository.UpdateAsync(knowledge);

            await LocalEventBus.PublishAsync(
                new OperationKnowledgeValidatedEto
                {
                    Id = knowledge.Id,
                    OperationId = knowledge.OperationId,
                    ValidatorName = knowledge.ValidatorName
                });
        }

        /// <summary>
        /// 归档工序知识条目
        /// </summary>
        public async Task ArchiveKnowledgeAsync(
            Guid knowledgeId,
            string archiveReason)
        {
            var knowledge = await _knowledgeRepository.GetAsync(knowledgeId);

            knowledge.IsArchived = true;
            knowledge.ArchiveReason = archiveReason;
            knowledge.ArchiveTime = Clock.Now;

            await _knowledgeRepository.UpdateAsync(knowledge);

            await LocalEventBus.PublishAsync(
                new OperationKnowledgeArchivedEto
                {
                    Id = knowledge.Id,
                    OperationId = knowledge.OperationId,
                    ArchiveReason = knowledge.ArchiveReason
                });
        }

        /// <summary>
        /// 恢复已归档的工序知识条目
        /// </summary>
        public async Task RestoreKnowledgeAsync(
            Guid knowledgeId,
            string restoreReason)
        {
            var knowledge = await _knowledgeRepository.GetAsync(knowledgeId);

            knowledge.IsArchived = false;
            knowledge.RestoreReason = restoreReason;
            knowledge.RestoreTime = Clock.Now;

            await _knowledgeRepository.UpdateAsync(knowledge);

            await LocalEventBus.PublishAsync(
                new OperationKnowledgeRestoredEto
                {
                    Id = knowledge.Id,
                    OperationId = knowledge.OperationId,
                    RestoreReason = knowledge.RestoreReason
                });
        }
    }
}