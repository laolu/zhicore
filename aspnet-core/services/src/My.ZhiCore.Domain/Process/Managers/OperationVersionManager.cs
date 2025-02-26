using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序版本管理器 - 负责管理工序的版本控制和变更历史
    /// </summary>
    public class OperationVersionManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationVersion, Guid> _operationVersionRepository;

        public OperationVersionManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationVersion, Guid> operationVersionRepository)
        {
            _operationRepository = operationRepository;
            _operationVersionRepository = operationVersionRepository;
        }

        /// <summary>
        /// 创建工序版本
        /// </summary>
        public async Task<OperationVersion> CreateVersionAsync(
            Guid operationId,
            string versionNumber,
            string description)
        {
            var operation = await _operationRepository.GetAsync(operationId);

            var version = new OperationVersion
            {
                OperationId = operationId,
                VersionNumber = versionNumber,
                Description = description,
                CreationTime = Clock.Now
            };

            await _operationVersionRepository.InsertAsync(version);

            await LocalEventBus.PublishAsync(
                new OperationVersionCreatedEto
                {
                    Id = version.Id,
                    OperationId = version.OperationId,
                    VersionNumber = version.VersionNumber
                });

            return version;
        }

        /// <summary>
        /// 更新工序版本信息
        /// </summary>
        public async Task<OperationVersion> UpdateVersionAsync(
            Guid versionId,
            string description)
        {
            var version = await _operationVersionRepository.GetAsync(versionId);
            
            version.Description = description;

            await _operationVersionRepository.UpdateAsync(version);

            await LocalEventBus.PublishAsync(
                new OperationVersionUpdatedEto
                {
                    Id = version.Id,
                    OperationId = version.OperationId,
                    VersionNumber = version.VersionNumber
                });

            return version;
        }

        /// <summary>
        /// 设置工序当前版本
        /// </summary>
        public async Task SetCurrentVersionAsync(Guid operationId, Guid versionId)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            var version = await _operationVersionRepository.GetAsync(versionId);

            operation.CurrentVersionId = versionId;
            await _operationRepository.UpdateAsync(operation);

            await LocalEventBus.PublishAsync(
                new OperationCurrentVersionChangedEto
                {
                    OperationId = operationId,
                    VersionId = versionId,
                    VersionNumber = version.VersionNumber
                });
        }
    }
}