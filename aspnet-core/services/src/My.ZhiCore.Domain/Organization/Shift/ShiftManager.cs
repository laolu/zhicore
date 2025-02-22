using System;
using System.Threading.Tasks;
using My.ZhiCore.Organization.Shift.Events;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Guids;
using System.Collections.Generic;

namespace My.ZhiCore.Organization.Shift
{
    /// <summary>
    /// 班次管理器
    /// </summary>
    public class ShiftManager : DomainService
    {
        private readonly ILocalEventBus _localEventBus;

        public ShiftManager(ILocalEventBus localEventBus)
        {
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 更新班次基本信息
        /// </summary>
        public virtual async Task UpdateShiftAsync(
            Shift shift,
            string name,
            TimeSpan startTime,
            TimeSpan endTime,
            string changeReason)
        {
            // 更新班次信息
            shift.SetName(name);
            shift.SetWorkTime(startTime, endTime);
            shift.SetChangeReason(changeReason);

            // 发布班次变更事件
            await _localEventBus.PublishAsync(new ShiftChangedEto
            {
                Id = shift.Id,
                Code = shift.Code,
                Name = shift.Name,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                ChangeTime = DateTime.Now,
                ChangeReason = changeReason
            });
        }

        /// <summary>
        /// 更改班次状态
        /// </summary>
        public virtual async Task ChangeStatusAsync(
            Shift shift,
            bool isActive,
            string changeReason)
        {
            // 更新状态
            shift.SetActive(isActive);
            shift.SetChangeReason(changeReason);

            // 发布状态变更事件
            await _localEventBus.PublishAsync(new ShiftStatusChangedEto
            {
                Id = shift.Id,
                Code = shift.Code,
                Name = shift.Name,
                IsActive = isActive,
                StatusChangedTime = DateTime.Now,
                ChangeReason = changeReason
            });
        }

        /// <summary>
        /// 分配人员到班次
        /// </summary>
        public virtual async Task AssignPersonnelAsync(
            Shift shift,
            Guid personnelId,
            DateTime effectiveDate,
            string changeReason)
        {
            // 添加人员
            shift.AddPersonnel(personnelId, effectiveDate);
            shift.SetChangeReason(changeReason);

            // 发布人员变更事件
            await _localEventBus.PublishAsync(new ShiftPersonnelChangedEto
            {
                ShiftId = shift.Id,
                PersonnelId = personnelId,
                ChangeType = ShiftPersonnelChangeType.Added,
                EffectiveDate = effectiveDate,
                ChangeTime = DateTime.Now,
                ChangeReason = changeReason
            });
        }

        /// <summary>
        /// 从班次中移除人员
        /// </summary>
        public virtual async Task RemovePersonnelAsync(
            Shift shift,
            Guid personnelId,
            string changeReason)
        {
            // 移除人员
            shift.RemovePersonnel(personnelId);
            shift.SetChangeReason(changeReason);

            // 发布人员变更事件
            await _localEventBus.PublishAsync(new ShiftPersonnelChangedEto
            {
                ShiftId = shift.Id,
                PersonnelId = personnelId,
                ChangeType = ShiftPersonnelChangeType.Removed,
                EffectiveDate = DateTime.Now,
                ChangeTime = DateTime.Now,
                ChangeReason = changeReason
            });
        }

        /// <summary>
        /// 创建考勤记录
        /// </summary>
        public virtual async Task CreateAttendanceRecordAsync(
            Shift shift,
            Guid personnelId,
            DateTime recordTime,
            AttendanceType attendanceType,
            string remarks)
        {
            // 创建考勤记录
            shift.AddAttendanceRecord(personnelId, recordTime, attendanceType);

            // 发布考勤记录事件
            await _localEventBus.PublishAsync(new ShiftAttendanceRecordCreatedEto
            {
                ShiftId = shift.Id,
                PersonnelId = personnelId,
                RecordTime = recordTime,
                AttendanceType = attendanceType,
                Remarks = remarks
            });
        }

        /// <summary>
        /// 处理考勤异常
        /// </summary>
        public virtual async Task HandleAttendanceExceptionAsync(
            Shift shift,
            Guid attendanceRecordId,
            string handlingResult,
            string remarks)
        {
            // 处理考勤异常
            shift.HandleAttendanceException(attendanceRecordId, handlingResult);

            // 发布考勤异常处理事件
            await _localEventBus.PublishAsync(new ShiftAttendanceExceptionHandledEto
            {
                ShiftId = shift.Id,
                AttendanceRecordId = attendanceRecordId,
                HandlingResult = handlingResult,
                HandlingTime = DateTime.Now,
                Remarks = remarks
            });
        }

        /// <summary>
        /// 创建交接班记录
        /// </summary>
        public virtual async Task CreateHandoverRecordAsync(
            Shift shift,
            Guid handoverUserId,
            Guid takeoverUserId,
            string remarks)
        {
            // 创建交接班记录
            var handoverId = shift.CreateHandoverRecord(handoverUserId, takeoverUserId);

            // 发布交接班记录创建事件
            await _localEventBus.PublishAsync(new ShiftHandoverRecordCreatedEto
            {
                Id = handoverId,
                ShiftId = shift.Id,
                HandoverUserId = handoverUserId,
                TakeoverUserId = takeoverUserId,
                HandoverTime = DateTime.Now,
                Remarks = remarks
            });
        }

        /// <summary>
        /// 确认交接班
        /// </summary>
        public virtual async Task ConfirmHandoverAsync(
            Shift shift,
            Guid handoverId,
            string remarks)
        {
            // 确认交接班
            shift.ConfirmHandover(handoverId);

            // 发布交接班确认事件
            await _localEventBus.PublishAsync(new ShiftHandoverConfirmedEto
            {
                Id = handoverId,
                ShiftId = shift.Id,
                ConfirmTime = DateTime.Now,
                Remarks = remarks
            });
        }

        /// <summary>
        /// 记录异常情况
        /// </summary>
        public virtual async Task CreateExceptionRecordAsync(
            Shift shift,
            string exceptionType,
            string description,
            Dictionary<string, string> extraProperties)
        {
            // 创建异常记录
            var exceptionId = shift.CreateExceptionRecord(exceptionType, description, extraProperties);

            // 发布异常记录事件
            await _localEventBus.PublishAsync(new ShiftExceptionRecordCreatedEto
            {
                Id = exceptionId,
                ShiftId = shift.Id,
                ExceptionType = exceptionType,
                Description = description,
                ExtraProperties = extraProperties,
                RecordTime = DateTime.Now
            });
        }

        /// <summary>
        /// 处理异常记录
        /// </summary>
        public virtual async Task HandleExceptionAsync(
            Shift shift,
            Guid exceptionId,
            string handlingResult,
            string remarks)
        {
            // 处理异常
            shift.HandleException(exceptionId, handlingResult);

            // 发布异常处理事件
            await _localEventBus.PublishAsync(new ShiftExceptionHandledEto
            {
                Id = exceptionId,
                ShiftId = shift.Id,
                HandlingResult = handlingResult,
                HandlingTime = DateTime.Now,
                Remarks = remarks
            });
        }

        /// <summary>
        /// 记录绩效指标
        /// </summary>
        public virtual async Task RecordPerformanceMetricAsync(
            Shift shift,
            string metricType,
            decimal value,
            Dictionary<string, string> extraProperties)
        {
            // 记录绩效指标
            var metricId = shift.RecordPerformanceMetric(metricType, value, extraProperties);

            // 发布绩效指标记录事件
            await _localEventBus.PublishAsync(new ShiftPerformanceMetricRecordedEto
            {
                Id = metricId,
                ShiftId = shift.Id,
                MetricType = metricType,
                Value = value,
                ExtraProperties = extraProperties,
                RecordTime = DateTime.Now
            });
        }

        /// <summary>
        /// 统计绩效指标
        /// </summary>
        public virtual async Task<Dictionary<string, decimal>> CalculatePerformanceMetricsAsync(
            Shift shift,
            DateTime startTime,
            DateTime endTime)
        {
            // 统计绩效指标
            var metrics = shift.CalculatePerformanceMetrics(startTime, endTime);

            // 发布绩效统计事件
            await _localEventBus.PublishAsync(new ShiftPerformanceMetricsCalculatedEto
            {
                ShiftId = shift.Id,
                StartTime = startTime,
                EndTime = endTime,
                Metrics = metrics,
                CalculateTime = DateTime.Now
            });

            return metrics;
        }
    }
}