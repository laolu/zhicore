using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序生命周期管理器 - 负责管理工序的生命周期状态和转换
    /// </summary>
    public class OperationLifecycleManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;

        public OperationLifecycleManager(
            IRepository<Operation, Guid> operationRepository)
        {
            _operationRepository = operationRepository;
        }

        /// <summary>
        /// 初始化工序
        /// </summary>
        public async Task InitializeOperationAsync(Guid operationId)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            operation.Initialize();
            await _operationRepository.UpdateAsync(operation);

            await LocalEventBus.PublishAsync(
                new OperationInitializedEto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Code = operation.Code
                });
        }

        /// <summary>
        /// 激活工序
        /// </summary>
        public async Task ActivateOperationAsync(Guid operationId)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            operation.Activate();
            await _operationRepository.UpdateAsync(operation);

            await LocalEventBus.PublishAsync(
                new OperationActivatedEto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Code = operation.Code
                });
        }

        /// <summary>
        /// 完成工序
        /// </summary>
        public async Task CompleteOperationAsync(Guid operationId)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            operation.Complete();
            await _operationRepository.UpdateAsync(operation);

            await LocalEventBus.PublishAsync(
                new OperationCompletedEto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Code = operation.Code
                });
        }

        /// <summary>
        /// 归档工序
        /// </summary>
        public async Task ArchiveOperationAsync(Guid operationId)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            operation.Archive();
            await _operationRepository.UpdateAsync(operation);

            await LocalEventBus.PublishAsync(
                new OperationArchivedEto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Code = operation.Code
                });
        }

        /// <summary>
        /// 废弃工序
        /// </summary>
        public async Task DiscardOperationAsync(Guid operationId)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            operation.Discard();
            await _operationRepository.UpdateAsync(operation);

            await LocalEventBus.PublishAsync(
                new OperationDiscardedEto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Code = operation.Code
                });
        }
    }
}