using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序管理器 - 负责处理工序基本信息的领域逻辑和事件
    /// </summary>
    public class OperationManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;

        public OperationManager(
            IRepository<Operation, Guid> operationRepository)
        {
            _operationRepository = operationRepository;
        }

        /// <summary>
        /// 创建工序
        /// </summary>
        public async Task<Operation> CreateOperationAsync(string name, string code)
        {
            var operation = new Operation
            {
                Name = name,
                Code = code
            };

            await _operationRepository.InsertAsync(operation);

            await LocalEventBus.PublishAsync(
                new OperationCreatedEto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Code = operation.Code
                });

            return operation;
        }

        /// <summary>
        /// 更新工序信息
        /// </summary>
        public async Task<Operation> UpdateOperationAsync(Guid id, string name, string code)
        {
            var operation = await _operationRepository.GetAsync(id);
            
            operation.Name = name;
            operation.Code = code;

            await _operationRepository.UpdateAsync(operation);

            await LocalEventBus.PublishAsync(
                new OperationUpdatedEto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Code = operation.Code
                });

            return operation;
        }

        /// <summary>
        /// 启用工序
        /// </summary>
        public async Task EnableOperationAsync(Guid operationId)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            operation.Enable();
            await _operationRepository.UpdateAsync(operation);

            await LocalEventBus.PublishAsync(
                new OperationEnabledEto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Code = operation.Code
                });
        }

        /// <summary>
        /// 禁用工序
        /// </summary>
        public async Task DisableOperationAsync(Guid operationId)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            operation.Disable();
            await _operationRepository.UpdateAsync(operation);

            await LocalEventBus.PublishAsync(
                new OperationDisabledEto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Code = operation.Code
                });
        }

        /// <summary>
        /// 暂停工序
        /// </summary>
        public async Task PauseOperationAsync(Guid operationId)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            operation.Pause();
            await _operationRepository.UpdateAsync(operation);

            await LocalEventBus.PublishAsync(
                new OperationPausedEto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Code = operation.Code
                });
        }

        /// <summary>
        /// 恢复工序
        /// </summary>
        public async Task ResumeOperationAsync(Guid operationId)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            operation.Resume();
            await _operationRepository.UpdateAsync(operation);

            await LocalEventBus.PublishAsync(
                new OperationResumedEto
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    Code = operation.Code
                });
        }
    }
}