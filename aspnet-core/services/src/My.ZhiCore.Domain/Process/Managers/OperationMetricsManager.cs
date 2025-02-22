using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序指标管理器 - 负责管理工序的关键指标和目标值
    /// </summary>
    public class OperationMetricsManager : ZhiCoreDomainService
    {
        private readonly IRepository<Operation, Guid> _operationRepository;
        private readonly IRepository<OperationMetric, Guid> _metricRepository;
        private readonly IRepository<OperationMetricValue, Guid> _metricValueRepository;

        public OperationMetricsManager(
            IRepository<Operation, Guid> operationRepository,
            IRepository<OperationMetric, Guid> metricRepository,
            IRepository<OperationMetricValue, Guid> metricValueRepository)
        {
            _operationRepository = operationRepository;
            _metricRepository = metricRepository;
            _metricValueRepository = metricValueRepository;
        }

        /// <summary>
        /// 创建工序指标
        /// </summary>
        public async Task<OperationMetric> CreateMetricAsync(
            Guid operationId,
            string name,
            string code,
            string unit,
            decimal targetValue,
            decimal warningThreshold)
        {
            var operation = await _operationRepository.GetAsync(operationId);

            var metric = new OperationMetric
            {
                OperationId = operationId,
                Name = name,
                Code = code,
                Unit = unit,
                TargetValue = targetValue,
                WarningThreshold = warningThreshold
            };

            await _metricRepository.InsertAsync(metric);

            await LocalEventBus.PublishAsync(
                new OperationMetricCreatedEto
                {
                    Id = metric.Id,
                    OperationId = metric.OperationId,
                    Name = metric.Name,
                    Code = metric.Code
                });

            return metric;
        }

        /// <summary>
        /// 更新工序指标
        /// </summary>
        public async Task<OperationMetric> UpdateMetricAsync(
            Guid metricId,
            string name,
            string code,
            string unit,
            decimal targetValue,
            decimal warningThreshold)
        {
            var metric = await _metricRepository.GetAsync(metricId);

            metric.Name = name;
            metric.Code = code;
            metric.Unit = unit;
            metric.TargetValue = targetValue;
            metric.WarningThreshold = warningThreshold;

            await _metricRepository.UpdateAsync(metric);

            await LocalEventBus.PublishAsync(
                new OperationMetricUpdatedEto
                {
                    Id = metric.Id,
                    OperationId = metric.OperationId,
                    Name = metric.Name,
                    Code = metric.Code
                });

            return metric;
        }

        /// <summary>
        /// 记录指标值
        /// </summary>
        public async Task<OperationMetricValue> RecordMetricValueAsync(
            Guid metricId,
            decimal value,
            DateTime recordTime)
        {
            var metric = await _metricRepository.GetAsync(metricId);

            var metricValue = new OperationMetricValue
            {
                MetricId = metricId,
                Value = value,
                RecordTime = recordTime
            };

            await _metricValueRepository.InsertAsync(metricValue);

            if (value > metric.WarningThreshold)
            {
                await LocalEventBus.PublishAsync(
                    new OperationMetricWarningEto
                    {
                        MetricId = metric.Id,
                        OperationId = metric.OperationId,
                        Value = value,
                        Threshold = metric.WarningThreshold
                    });
            }

            return metricValue;
        }

        /// <summary>
        /// 获取指标历史值
        /// </summary>
        public async Task<List<OperationMetricValue>> GetMetricHistoryAsync(
            Guid metricId,
            DateTime startTime,
            DateTime endTime)
        {
            return await _metricValueRepository.GetListAsync(
                x => x.MetricId == metricId &&
                     x.RecordTime >= startTime &&
                     x.RecordTime <= endTime);
        }

        /// <summary>
        /// 获取工序的所有指标
        /// </summary>
        public async Task<List<OperationMetric>> GetOperationMetricsAsync(Guid operationId)
        {
            return await _metricRepository.GetListAsync(x => x.OperationId == operationId);
        }
    }
}