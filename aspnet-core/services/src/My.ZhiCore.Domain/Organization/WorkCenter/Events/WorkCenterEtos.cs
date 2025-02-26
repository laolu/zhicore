using System;
using Volo.Abp.EventBus;
using System.Collections.Generic;

namespace My.ZhiCore.Organization.WorkCenter.Events
{
    /// <summary>
    /// 工作中心变更事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.WorkCenter.WorkCenterChanged")]
    public class WorkCenterChangedEto
    {
        /// <summary>工作中心ID</summary>
        public Guid Id { get; set; }

        /// <summary>工作中心编码</summary>
        public string Code { get; set; }

        /// <summary>工作中心名称</summary>
        public string Name { get; set; }

        /// <summary>工作中心描述</summary>
        public string Description { get; set; }

        /// <summary>变更时间</summary>
        public DateTime ChangeTime { get; set; }

        /// <summary>变更原因</summary>
        public string ChangeReason { get; set; }
    }

    /// <summary>
    /// 工作中心状态变更事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.WorkCenter.WorkCenterStatusChanged")]
    public class WorkCenterStatusChangedEto
    {
        /// <summary>工作中心ID</summary>
        public Guid Id { get; set; }

        /// <summary>工作中心编码</summary>
        public string Code { get; set; }

        /// <summary>工作中心名称</summary>
        public string Name { get; set; }

        /// <summary>是否启用</summary>
        public bool IsActive { get; set; }

        /// <summary>状态变更时间</summary>
        public DateTime StatusChangedTime { get; set; }

        /// <summary>变更原因</summary>
        public string ChangeReason { get; set; }
    }

    /// <summary>
    /// 工作中心设备变更事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.WorkCenter.WorkCenterDeviceChanged")]
    public class WorkCenterDeviceChangedEto
    {
        /// <summary>工作中心ID</summary>
        public Guid WorkCenterId { get; set; }

        /// <summary>设备ID</summary>
        public Guid DeviceId { get; set; }

        /// <summary>变更类型（添加/移除）</summary>
        public WorkCenterDeviceChangeType ChangeType { get; set; }

        /// <summary>生效日期</summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>变更原因</summary>
        public string ChangeReason { get; set; }

        /// <summary>变更时间</summary>
        public DateTime ChangeTime { get; set; }
    }

    /// <summary>
    /// 工作中心设备变更类型
    /// </summary>
    public enum WorkCenterDeviceChangeType
    {
        /// <summary>添加设备</summary>
        Added = 1,

        /// <summary>移除设备</summary>
        Removed = 2
    }

    /// <summary>
    /// 工作中心异常记录创建事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.WorkCenter.WorkCenterExceptionRecordCreated")]
    public class WorkCenterExceptionRecordCreatedEto
    {
        /// <summary>异常记录ID</summary>
        public Guid Id { get; set; }

        /// <summary>工作中心ID</summary>
        public Guid WorkCenterId { get; set; }

        /// <summary>异常类型</summary>
        public string ExceptionType { get; set; }

        /// <summary>异常描述</summary>
        public string Description { get; set; }

        /// <summary>额外属性</summary>
        public Dictionary<string, string> ExtraProperties { get; set; }

        /// <summary>记录时间</summary>
        public DateTime RecordTime { get; set; }
    }

    /// <summary>
    /// 工作中心异常处理事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.WorkCenter.WorkCenterExceptionHandled")]
    public class WorkCenterExceptionHandledEto
    {
        /// <summary>异常记录ID</summary>
        public Guid Id { get; set; }

        /// <summary>工作中心ID</summary>
        public Guid WorkCenterId { get; set; }

        /// <summary>处理结果</summary>
        public string HandlingResult { get; set; }

        /// <summary>处理时间</summary>
        public DateTime HandlingTime { get; set; }

        /// <summary>备注</summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 工作中心绩效指标记录事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.WorkCenter.WorkCenterPerformanceMetricRecorded")]
    public class WorkCenterPerformanceMetricRecordedEto
    {
        /// <summary>指标记录ID</summary>
        public Guid Id { get; set; }

        /// <summary>工作中心ID</summary>
        public Guid WorkCenterId { get; set; }

        /// <summary>指标类型</summary>
        public string MetricType { get; set; }

        /// <summary>指标值</summary>
        public decimal Value { get; set; }

        /// <summary>额外属性</summary>
        public Dictionary<string, string> ExtraProperties { get; set; }

        /// <summary>记录时间</summary>
        public DateTime RecordTime { get; set; }
    }

    /// <summary>
    /// 工作中心绩效指标统计事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.WorkCenter.WorkCenterPerformanceMetricsCalculated")]
    public class WorkCenterPerformanceMetricsCalculatedEto
    {
        /// <summary>工作中心ID</summary>
        public Guid WorkCenterId { get; set; }

        /// <summary>开始时间</summary>
        public DateTime StartTime { get; set; }

        /// <summary>结束时间</summary>
        public DateTime EndTime { get; set; }

        /// <summary>统计指标</summary>
        public Dictionary<string, decimal> Metrics { get; set; }

        /// <summary>统计时间</summary>
        public DateTime CalculateTime { get; set; }
    }
}