using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序执行管理器 - 负责处理工序执行相关的领域逻辑和事件
    /// </summary>
    public class OperationExecutionManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationExecution, Guid> _operationExecutionRepository;

        public OperationExecutionManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationExecution, Guid> operationExecutionRepository)
        {
            _operationRepository = operationRepository;
            _operationExecutionRepository = operationExecutionRepository;
        }

        /// <summary>
        /// 开始执行工序
        /// </summary>
        public async Task<OperationExecution> StartOperationExecutionAsync(
            Guid operationId,
            string executionCode)
        {
            var operation = await _operationRepository.GetAsync(operationId);

            var execution = new OperationExecution
            {
                OperationId = operationId,
                ExecutionCode = executionCode,
                Status = OperationExecutionStatus.Running,
                StartTime = Clock.Now
            };

            await _operationExecutionRepository.InsertAsync(execution);

            await LocalEventBus.PublishAsync(
                new OperationExecutionStartedEto
                {
                    Id = execution.Id,
                    OperationId = execution.OperationId,
                    ExecutionCode = execution.ExecutionCode,
                    StartTime = execution.StartTime
                });

            return execution;
        }

        /// <summary>
        /// 完成工序执行
        /// </summary>
        public async Task CompleteOperationExecutionAsync(
            Guid executionId,
            bool isSuccess = true)
        {
            var execution = await _operationExecutionRepository.GetAsync(executionId);
            execution.Status = isSuccess ? OperationExecutionStatus.Completed : OperationExecutionStatus.Failed;
            execution.EndTime = Clock.Now;

            await _operationExecutionRepository.UpdateAsync(execution);

            await LocalEventBus.PublishAsync(
                new OperationExecutionCompletedEto
                {
                    Id = execution.Id,
                    OperationId = execution.OperationId,
                    ExecutionCode = execution.ExecutionCode,
                    IsSuccess = isSuccess,
                    StartTime = execution.StartTime,
                    EndTime = execution.EndTime
                });
        }

        /// <summary>
        /// 获取工序执行记录
        /// </summary>
        public async Task<OperationExecution> GetOperationExecutionAsync(Guid executionId)
        {
            return await _operationExecutionRepository.GetAsync(executionId);
        }

        /// <summary>
        /// 监控工序执行状态
        /// </summary>
        public async Task<OperationExecutionStatus> MonitorExecutionStatusAsync(Guid executionId)
        {
            var execution = await _operationExecutionRepository.GetAsync(executionId);
            
            await LocalEventBus.PublishAsync(
                new OperationExecutionMonitoredEto
                {
                    ExecutionId = executionId,
                    OperationId = execution.OperationId,
                    Status = execution.Status
                });

            return execution.Status;
        }
    }
}