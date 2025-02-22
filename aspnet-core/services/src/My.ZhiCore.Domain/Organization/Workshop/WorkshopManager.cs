using System;
using System.Threading.Tasks;
using My.ZhiCore.Organization.Workshop.Events;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;
using System.Collections.Generic;

namespace My.ZhiCore.Organization.Workshop
{
    /// <summary>
    /// 车间管理器
    /// </summary>
    public class WorkshopManager : DomainService
    {
        private readonly ILocalEventBus _localEventBus;

        public WorkshopManager(ILocalEventBus localEventBus)
        {
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 更新车间基本信息
        /// </summary>
        public virtual async Task UpdateWorkshopAsync(
            Workshop workshop,
            string name,
            string description,
            string changeReason)
        {
            // 更新车间信息
            workshop.SetName(name);
            workshop.SetDescription(description);
            workshop.SetChangeReason(changeReason);

            // 发布车间变更事件
            await _localEventBus.PublishAsync(new WorkshopChangedEto
            {
                Id = workshop.Id,
                Code = workshop.Code,
                Name = workshop.Name,
                Description = workshop.Description,
                ChangeTime = DateTime.Now,
                ChangeReason = changeReason
            });
        }

        /// <summary>
        /// 更改车间状态
        /// </summary>
        public virtual async Task ChangeStatusAsync(
            Workshop workshop,
            bool isActive,
            string changeReason)
        {
            // 更新状态
            workshop.SetActive(isActive);
            workshop.SetChangeReason(changeReason);

            // 发布状态变更事件
            await _localEventBus.PublishAsync(new WorkshopStatusChangedEto
            {
                Id = workshop.Id,
                Code = workshop.Code,
                Name = workshop.Name,
                IsActive = isActive,
                StatusChangedTime = DateTime.Now,
                ChangeReason = changeReason
            });
        }

        /// <summary>
        /// 更新车间产能
        /// </summary>
        public virtual async Task UpdateCapacityAsync(
            Workshop workshop,
            decimal dailyCapacity,
            string unit,
            string changeReason)
        {
            // 更新产能信息
            workshop.SetCapacity(dailyCapacity, unit);
            workshop.SetChangeReason(changeReason);

            // 发布产能变更事件
            await _localEventBus.PublishAsync(new WorkshopCapacityChangedEto
            {
                Id = workshop.Id,
                Code = workshop.Code,
                Name = workshop.Name,
                DailyCapacity = dailyCapacity,
                Unit = unit,
                ChangeTime = DateTime.Now,
                ChangeReason = changeReason
            });
        }

        /// <summary>
        /// 记录车间异常情况
        /// </summary>
        public virtual async Task CreateExceptionRecordAsync(
            Workshop workshop,
            string exceptionType,
            string description,
            Dictionary<string, string> extraProperties)
        {
            // 创建异常记录
            var exceptionId = workshop.CreateExceptionRecord(exceptionType, description, extraProperties);

            // 发布异常记录事件
            await _localEventBus.PublishAsync(new WorkshopExceptionRecordCreatedEto
            {
                Id = exceptionId,
                WorkshopId = workshop.Id,
                ExceptionType = exceptionType,
                Description = description,
                ExtraProperties = extraProperties,
                RecordTime = DateTime.Now
            });
        }

        /// <summary>
        /// 处理车间异常
        /// </summary>
        public virtual async Task HandleExceptionAsync(
            Workshop workshop,
            Guid exceptionId,
            string handlingResult,
            string remarks)
        {
            // 处理异常
            workshop.HandleException(exceptionId, handlingResult);

            // 发布异常处理事件
            await _localEventBus.PublishAsync(new WorkshopExceptionHandledEto
            {
                Id = exceptionId,
                WorkshopId = workshop.Id,
                HandlingResult = handlingResult,
                HandlingTime = DateTime.Now,
                Remarks = remarks
            });
        }

        /// <summary>
        /// 记录车间绩效指标
        /// </summary>
        public virtual async Task RecordPerformanceMetricAsync(
            Workshop workshop,
            string metricType,
            decimal value,
            Dictionary<string, string> extraProperties)
        {
            // 记录绩效指标
            var metricId = workshop.RecordPerformanceMetric(metricType, value, extraProperties);

            // 发布绩效指标记录事件
            await _localEventBus.PublishAsync(new WorkshopPerformanceMetricRecordedEto
            {
                Id = metricId,
                WorkshopId = workshop.Id,
                MetricType = metricType,
                Value = value,
                ExtraProperties = extraProperties,
                RecordTime = DateTime.Now
            });
        }

        /// <summary>
        /// 统计车间绩效指标
        /// </summary>
        public virtual async Task<Dictionary<string, decimal>> CalculatePerformanceMetricsAsync(
            Workshop workshop,
            DateTime startTime,
            DateTime endTime)
        {
            // 统计绩效指标
            var metrics = workshop.CalculatePerformanceMetrics(startTime, endTime);

            // 发布绩效统计事件
            await _localEventBus.PublishAsync(new WorkshopPerformanceMetricsCalculatedEto
            {
                WorkshopId = workshop.Id,
                StartTime = startTime,
                EndTime = endTime,
                Metrics = metrics,
                CalculateTime = DateTime.Now
            });

            return metrics;
        }
    }
}