using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序工艺管理器 - 负责处理工序工艺的领域逻辑和事件
    /// </summary>
    public class OperationProcessManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationProcess, Guid> _operationProcessRepository;
        private readonly IRepository<OperationProcessParameter, Guid> _processParameterRepository;

        public OperationProcessManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationProcess, Guid> operationProcessRepository,
            IRepository<OperationProcessParameter, Guid> processParameterRepository)
        {
            _operationRepository = operationRepository;
            _operationProcessRepository = operationProcessRepository;
            _processParameterRepository = processParameterRepository;
        }

        /// <summary>
        /// 创建工序工艺
        /// </summary>
        public async Task<OperationProcess> CreateOperationProcessAsync(
            Guid operationId,
            string processName,
            string processCode,
            string description)
        {
            var operation = await _operationRepository.GetAsync(operationId);

            var process = new OperationProcess
            {
                OperationId = operationId,
                ProcessName = processName,
                ProcessCode = processCode,
                Description = description,
                Status = OperationProcessStatus.Active
            };

            await _operationProcessRepository.InsertAsync(process);

            await LocalEventBus.PublishAsync(
                new OperationProcessCreatedEto
                {
                    Id = process.Id,
                    OperationId = process.OperationId,
                    ProcessName = process.ProcessName,
                    ProcessCode = process.ProcessCode
                });

            return process;
        }

        /// <summary>
        /// 更新工序工艺
        /// </summary>
        public async Task UpdateOperationProcessAsync(
            Guid processId,
            string processName = null,
            string description = null)
        {
            var process = await _operationProcessRepository.GetAsync(processId);

            if (!string.IsNullOrEmpty(processName))
                process.ProcessName = processName;

            if (!string.IsNullOrEmpty(description))
                process.Description = description;

            await _operationProcessRepository.UpdateAsync(process);

            await LocalEventBus.PublishAsync(
                new OperationProcessUpdatedEto
                {
                    Id = process.Id,
                    OperationId = process.OperationId,
                    ProcessName = process.ProcessName
                });
        }

        /// <summary>
        /// 添加工艺参数
        /// </summary>
        public async Task<OperationProcessParameter> AddProcessParameterAsync(
            Guid processId,
            string parameterName,
            string parameterCode,
            string defaultValue,
            string unit)
        {
            var process = await _operationProcessRepository.GetAsync(processId);

            var parameter = new OperationProcessParameter
            {
                ProcessId = processId,
                ParameterName = parameterName,
                ParameterCode = parameterCode,
                DefaultValue = defaultValue,
                Unit = unit
            };

            await _processParameterRepository.InsertAsync(parameter);

            await LocalEventBus.PublishAsync(
                new OperationProcessParameterAddedEto
                {
                    Id = parameter.Id,
                    ProcessId = parameter.ProcessId,
                    ParameterName = parameter.ParameterName,
                    ParameterCode = parameter.ParameterCode
                });

            return parameter;
        }

        /// <summary>
        /// 停用工序工艺
        /// </summary>
        public async Task DeactivateOperationProcessAsync(Guid processId)
        {
            var process = await _operationProcessRepository.GetAsync(processId);
            process.Status = OperationProcessStatus.Inactive;
            await _operationProcessRepository.UpdateAsync(process);

            await LocalEventBus.PublishAsync(
                new OperationProcessDeactivatedEto
                {
                    Id = process.Id,
                    OperationId = process.OperationId
                });
        }
    }
}