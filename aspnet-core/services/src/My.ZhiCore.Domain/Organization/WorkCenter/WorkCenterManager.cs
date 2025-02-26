using System;
using System.Threading.Tasks;
using My.ZhiCore.Organization.WorkCenter.Events;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;
using System.Collections.Generic;

namespace My.ZhiCore.Organization.WorkCenter
{
    /// <summary>
    /// 工作中心管理器
    /// </summary>
    public class WorkCenterManager : DomainService
    {
        private readonly ILocalEventBus _localEventBus;

        public WorkCenterManager(ILocalEventBus localEventBus)
        {
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 更新工作中心基本信息
        /// </summary>
        public virtual async Task UpdateWorkCenterAsync(
            WorkCenter workCenter,
            string name,
            string description,
            string changeReason)
        {
            // 更新工作中心信息
            workCenter.SetName(name);
            workCenter.SetDescription(description);
            workCenter.SetChangeReason(changeReason);

            // 发布工作中心变更事件
            await _localEventBus.PublishAsync(new WorkCenterChangedEto
            {
                Id = workCenter.Id,
                Code = workCenter.Code,
                Name = workCenter.Name,
                Description = workCenter.Description,
                ChangeTime = DateTime.Now,
                ChangeReason = changeReason
            });
        }

        /// <summary>
        /// 更改工作中心状态
        /// </summary>
        public virtual async Task ChangeStatusAsync(
            WorkCenter workCenter,
            bool isActive,
            string changeReason)
        {
            // 更新状态
            workCenter.SetActive(isActive);
            workCenter.SetChangeReason(changeReason);

            // 发布状态变更事件
            await _localEventBus.PublishAsync(new WorkCenterStatusChangedEto
            {
                Id = workCenter.Id,
                Code = workCenter.Code,
                Name = workCenter.Name,
                IsActive = isActive,
                StatusChangedTime = DateTime.Now,
                ChangeReason = changeReason
            });
        }

        /// <summary>
        /// 分配设备到工作中心
        /// </summary>
        public virtual async Task AssignDeviceAsync(
            WorkCenter workCenter,
            Guid deviceId,
            DateTime effectiveDate,
            string changeReason)
        {
            // 添加设备
            workCenter.AddDevice(deviceId, effectiveDate);
            workCenter.SetChangeReason(changeReason);

            // 发布设备变更事件
            await _localEventBus.PublishAsync(new WorkCenterDeviceChangedEto
            {
                WorkCenterId = workCenter.Id,
                DeviceId = deviceId,
                ChangeType = WorkCenterDeviceChangeType.Added,
                EffectiveDate = effectiveDate,
                ChangeTime = DateTime.Now,
                ChangeReason = changeReason
            });
        }

        /// <summary>
        /// 从工作中心移除设备
        /// </summary>
        public virtual async Task RemoveDeviceAsync(
            WorkCenter workCenter,
            Guid deviceId,
            string changeReason)
        {
            // 移除设备
            workCenter.RemoveDevice(deviceId);
            workCenter.SetChangeReason(changeReason);

            // 发布设备变更事件
            await _localEventBus.PublishAsync(new WorkCenterDeviceChangedEto
            {
                WorkCenterId = workCenter.Id,
                DeviceId = deviceId,
                ChangeType = WorkCenterDeviceChangeType.Removed,
                EffectiveDate = DateTime.Now,
                ChangeTime = DateTime.Now,
                ChangeReason = changeReason
            });
        }

        /// <summary>
        /// 记录工作中心异常情况
        /// </summary>
        public virtual async Task CreateExceptionRecordAsync(
            WorkCenter workCenter,
            string exceptionType,
            string description,
            Dictionary<string, string> extraProperties)
        {
            // 创建异常记录
            var exceptionId = workCenter.CreateExceptionRecord(exceptionType, description, extraProperties);

            // 发布异常记录事件
            await _localEventBus.PublishAsync(new WorkCenterExceptionRecordCreatedEto
            {
                Id = exceptionId,
                WorkCenterId = workCenter.Id,
                ExceptionType = exceptionType,
                Description = description,
                ExtraProperties = extraProperties,
                RecordTime = DateTime.Now
            });
        }

        /// <summary>
        /// 处理工作中心异常
        /// </summary>
        public virtual async Task HandleExceptionAsync(
            WorkCenter workCenter,
            Guid exceptionId,
            string handlingResult,
            string remarks)
        {
            // 处理异常
            workCenter.HandleException(exceptionId, handlingResult);

            // 发布异常处理事件
            await _localEventBus.PublishAsync(new WorkCenterExceptionHandledEto
            {
                Id = exceptionId,
                WorkCenterId = workCenter.Id,
                HandlingResult = handlingResult,
                HandlingTime = DateTime.Now,
                Remarks = remarks
            });
        }

        /// <summary>
        /// 记录工作中心绩效指标
        /// </summary>
        public virtual async Task RecordPerformanceMetricAsync(
            WorkCenter workCenter,
            string metricType,
            decimal value,
            Dictionary<string, string> extraProperties)
        {
            // 记录绩效指标
            var metricId = workCenter.RecordPerformanceMetric(metricType, value, extraProperties);

            // 发布绩效指标记录事件
            await _localEventBus.PublishAsync(new WorkCenterPerformanceMetricRecordedEto
            {
                Id = metricId,
                WorkCenterId = workCenter.Id,
                MetricType = metricType,
                Value = value,
                ExtraProperties = extraProperties,
                RecordTime = DateTime.Now
            });
        }

        /// <summary>
        /// 统计工作中心绩效指标
        /// </summary>
        public virtual async Task<Dictionary<string, decimal>> CalculatePerformanceMetricsAsync(
            WorkCenter workCenter,
            DateTime startTime,
            DateTime endTime)
        {
            // 统计绩效指标
            var metrics = workCenter.CalculatePerformanceMetrics(startTime, endTime);

            // 发布绩效统计事件
            await _localEventBus.PublishAsync(new WorkCenterPerformanceMetricsCalculatedEto
            {
                WorkCenterId = workCenter.Id,
                StartTime = startTime,
                EndTime = endTime,
                Metrics = metrics,
                CalculateTime = DateTime.Now
            });

            return metrics;
        }
    }
}