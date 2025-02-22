using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序监控管理器 - 负责实时监控工序执行状态和性能指标
    /// </summary>
    public class OperationMonitorManager : ZhiCoreDomainService
    {   
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationExecution, Guid> _operationExecutionRepository;

        public OperationMonitorManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationExecution, Guid> operationExecutionRepository)
        {
            _operationRepository = operationRepository;
            _operationExecutionRepository = operationExecutionRepository;
        }

        /// <summary>
        /// 记录工序性能指标
        /// </summary>
        public async Task RecordPerformanceMetricsAsync(
            Guid operationId,
            double cycleTime,
            double outputRate,
            double qualityRate,
            double equipmentEfficiency)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            
            var metrics = new OperationPerformanceMetrics
            {
                OperationId = operationId,
                CycleTime = cycleTime,
                OutputRate = outputRate,
                QualityRate = qualityRate,
                EquipmentEfficiency = equipmentEfficiency,
                RecordTime = Clock.Now
            };

            await LocalEventBus.PublishAsync(
                new OperationMetricsRecordedEto
                {
                    OperationId = operationId,
                    Metrics = metrics
                });
        }

        /// <summary>
        /// 获取工序实时状态
        /// </summary>
        public async Task<OperationStatus> GetOperationStatusAsync(Guid operationId)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            var currentExecution = await _operationExecutionRepository
                .FirstOrDefaultAsync(e => e.OperationId == operationId && e.Status == OperationExecutionStatus.Running);

            return new OperationStatus
            {
                OperationId = operationId,
                IsRunning = currentExecution != null,
                CurrentExecutionId = currentExecution?.Id,
                LastUpdateTime = Clock.Now
            };
        }

        /// <summary>
        /// 设置工序告警
        /// </summary>
        public async Task SetOperationAlertAsync(
            Guid operationId,
            string alertType,
            string alertMessage,
            AlertSeverity severity)
        {
            var operation = await _operationRepository.GetAsync(operationId);
            
            var alert = new OperationAlert
            {
                OperationId = operationId,
                AlertType = alertType,
                Message = alertMessage,
                Severity = severity,
                AlertTime = Clock.Now
            };

            await LocalEventBus.PublishAsync(
                new OperationAlertRaisedEto
                {
                    OperationId = operationId,
                    Alert = alert
                });
        }
    }
}