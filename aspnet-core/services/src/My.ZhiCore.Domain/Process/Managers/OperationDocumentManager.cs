using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序文档管理器 - 负责管理工序相关的技术文档和操作指导
    /// </summary>
    public class OperationDocumentManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationDocument, Guid> _operationDocumentRepository;

        public OperationDocumentManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationDocument, Guid> operationDocumentRepository)
        {
            _operationRepository = operationRepository;
            _operationDocumentRepository = operationDocumentRepository;
        }

        /// <summary>
        /// 添加工序文档
        /// </summary>
        public async Task<OperationDocument> AddDocumentAsync(
            Guid operationId,
            string title,
            string content,
            string documentType,
            string fileUrl = null)
        {
            var operation = await _operationRepository.GetAsync(operationId);

            var document = new OperationDocument
            {
                OperationId = operationId,
                Title = title,
                Content = content,
                DocumentType = documentType,
                FileUrl = fileUrl,
                CreationTime = Clock.Now
            };

            await _operationDocumentRepository.InsertAsync(document);

            await LocalEventBus.PublishAsync(
                new OperationDocumentCreatedEto
                {
                    Id = document.Id,
                    OperationId = document.OperationId,
                    Title = document.Title,
                    DocumentType = document.DocumentType
                });

            return document;
        }

        /// <summary>
        /// 更新工序文档
        /// </summary>
        public async Task<OperationDocument> UpdateDocumentAsync(
            Guid documentId,
            string title,
            string content,
            string fileUrl = null)
        {
            var document = await _operationDocumentRepository.GetAsync(documentId);
            
            document.Title = title;
            document.Content = content;
            document.FileUrl = fileUrl;
            document.LastModificationTime = Clock.Now;

            await _operationDocumentRepository.UpdateAsync(document);

            await LocalEventBus.PublishAsync(
                new OperationDocumentUpdatedEto
                {
                    Id = document.Id,
                    OperationId = document.OperationId,
                    Title = document.Title,
                    DocumentType = document.DocumentType
                });

            return document;
        }

        /// <summary>
        /// 删除工序文档
        /// </summary>
        public async Task DeleteDocumentAsync(Guid documentId)
        {
            var document = await _operationDocumentRepository.GetAsync(documentId);
            await _operationDocumentRepository.DeleteAsync(document);

            await LocalEventBus.PublishAsync(
                new OperationDocumentDeletedEto
                {
                    Id = document.Id,
                    OperationId = document.OperationId
                });
        }

        /// <summary>
        /// 归档工序文档
        /// </summary>
        public async Task ArchiveDocumentAsync(Guid documentId)
        {
            var document = await _operationDocumentRepository.GetAsync(documentId);
            
            document.IsArchived = true;
            document.ArchiveTime = Clock.Now;

            await _operationDocumentRepository.UpdateAsync(document);

            await LocalEventBus.PublishAsync(
                new OperationDocumentArchivedEto
                {
                    Id = document.Id,
                    OperationId = document.OperationId,
                    Title = document.Title
                });
        }
    }
}