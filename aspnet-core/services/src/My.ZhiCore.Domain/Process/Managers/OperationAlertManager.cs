using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序预警管理器 - 负责处理工序执行过程中的预警信息
    /// </summary>
    public class OperationAlertManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationExecution, Guid> _operationExecutionRepository;

        public OperationAlertManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationExecution, Guid> operationExecutionRepository)
        {
            _operationRepository = operationRepository;
            _operationExecutionRepository = operationExecutionRepository;
        }

        /// <summary>
        /// 创建工序预警
        /// </summary>
        public async Task CreateOperationAlertAsync(
            Guid operationId,
            string alertType,
            string alertMessage,
            AlertSeverity severity)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            var execution = await _operationExecutionRepository.FirstOrDefaultAsync(e => 
                e.OperationId == operationId && 
                e.Status == OperationExecutionStatus.Running);

            var alert = new OperationAlert
            {
                OperationId = operationId,
                ExecutionId = execution?.Id,
                AlertType = alertType,
                AlertMessage = alertMessage,
                Severity = severity,
                CreationTime = Clock.Now,
                Status = AlertStatus.Active
            };

            await LocalEventBus.PublishAsync(
                new OperationAlertCreatedEto
                {
                    Id = alert.Id,
                    OperationId = alert.OperationId,
                    ExecutionId = alert.ExecutionId,
                    AlertType = alert.AlertType,
                    AlertMessage = alert.AlertMessage,
                    Severity = alert.Severity
                });
        }

        /// <summary>
        /// 处理工序预警
        /// </summary>
        public async Task HandleOperationAlertAsync(
            Guid alertId,
            string handlingNote)
        {
            var alert = await _operationAlertRepository.GetAsync(alertId);
            
            alert.Status = AlertStatus.Handled;
            alert.HandlingNote = handlingNote;
            alert.HandlingTime = Clock.Now;

            await _operationAlertRepository.UpdateAsync(alert);

            await LocalEventBus.PublishAsync(
                new OperationAlertHandledEto
                {
                    Id = alert.Id,
                    OperationId = alert.OperationId,
                    HandlingNote = handlingNote,
                    HandlingTime = alert.HandlingTime
                });
        }

        /// <summary>
        /// 获取活跃的工序预警
        /// </summary>
        public async Task<List<OperationAlert>> GetActiveAlertsAsync(
            Guid? operationId = null,
            AlertSeverity? minSeverity = null)
        {
            var query = await _operationAlertRepository.GetQueryableAsync();
            query = query.Where(a => a.Status == AlertStatus.Active);

            if (operationId.HasValue)
            {
                query = query.Where(a => a.OperationId == operationId.Value);
            }

            if (minSeverity.HasValue)
            {
                query = query.Where(a => a.Severity >= minSeverity.Value);
            }

            return await AsyncExecuter.ToListAsync(query);
        }
    }
}