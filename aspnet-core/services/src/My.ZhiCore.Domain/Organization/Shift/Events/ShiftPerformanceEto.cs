using System;
using System.Collections.Generic;
using Volo.Abp.EventBus;

namespace My.ZhiCore.Organization.Shift.Events
{
    /// <summary>
    /// 绩效指标记录事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftPerformanceMetricRecorded")]
    public class ShiftPerformanceMetricRecordedEto
    {
        /// <summary>指标记录ID</summary>
        public Guid Id { get; set; }

        /// <summary>班次ID</summary>
        public Guid ShiftId { get; set; }

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
    /// 绩效指标统计事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftPerformanceMetricsCalculated")]
    public class ShiftPerformanceMetricsCalculatedEto
    {
        /// <summary>班次ID</summary>
        public Guid ShiftId { get; set; }

        /// <summary>开始时间</summary>
        public DateTime StartTime { get; set; }

        /// <summary>结束时间</summary>
        public DateTime EndTime { get; set; }

        /// <summary>统计指标</summary>
        public Dictionary<string, decimal> Metrics { get; set; }

        /// <summary>统计时间</summary>
        public DateTime CalculateTime { get; set; }
    }

    /// <summary>
    /// 班次绩效事件
    /// </summary>
    [EventName("My.ZhiCore.Organization.Shift.ShiftPerformance")]
    public class ShiftPerformanceEto
    {
        /// <summary>班次ID</summary>
        public Guid ShiftId { get; set; }

        /// <summary>班次编码</summary>
        public string ShiftCode { get; set; }

        /// <summary>班次名称</summary>
        public string ShiftName { get; set; }

        /// <summary>统计日期</summary>
        public DateTime StatisticsDate { get; set; }

        /// <summary>生产计划完成率</summary>
        public decimal ProductionPlanCompletionRate { get; set; }

        /// <summary>设备利用率</summary>
        public decimal EquipmentUtilizationRate { get; set; }

        /// <summary>产品合格率</summary>
        public decimal ProductQualityRate { get; set; }

        /// <summary>人员出勤率</summary>
        public decimal AttendanceRate { get; set; }

        /// <summary>安全事故数</summary>
        public int SafetyIncidents { get; set; }

        /// <summary>能源消耗指标</summary>
        public decimal EnergyConsumptionIndex { get; set; }

        /// <summary>备注</summary>
        public string Remarks { get; set; }
    }
}