using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量问题实体 - 用于记录和跟踪生产过程中发现的质量问题
    /// </summary>
    public class QualityIssue : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 问题编号
        /// </summary>
        public string IssueNumber { get; private set; }

        /// <summary>
        /// 问题标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 问题描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 问题严重程度
        /// </summary>
        public IssueSeverity Severity { get; private set; }

        /// <summary>
        /// 问题状态
        /// </summary>
        public IssueStatus Status { get; private set; }

        /// <summary>
        /// 关联工单ID
        /// </summary>
        public Guid? WorkOrderId { get; private set; }

        /// <summary>
        /// 关联工序ID
        /// </summary>
        public Guid? OperationId { get; private set; }

        /// <summary>
        /// 发现时间
        /// </summary>
        public DateTime DiscoveryTime { get; private set; }

        /// <summary>
        /// 预计解决时间
        /// </summary>
        public DateTime? ExpectedResolutionTime { get; private set; }

        /// <summary>
        /// 实际解决时间
        /// </summary>
        public DateTime? ActualResolutionTime { get; private set; }

        /// <summary>
        /// 解决方案
        /// </summary>
        public string Solution { get; private set; }

        /// <summary>
        /// 预防措施
        /// </summary>
        public string PreventiveMeasures { get; private set; }

        /// <summary>
        /// 处理记录集合
        /// </summary>
        public ICollection<QualityIssueHandlingRecord> HandlingRecords { get; private set; }

        protected QualityIssue()
        {
            HandlingRecords = new List<QualityIssueHandlingRecord>();
        }

        public QualityIssue(
            Guid id,
            string issueNumber,
            string title,
            string description,
            IssueSeverity severity,
            DateTime discoveryTime,
            Guid? workOrderId = null,
            Guid? operationId = null,
            DateTime? expectedResolutionTime = null)
        {
            Id = id;
            IssueNumber = issueNumber;
            Title = title;
            Description = description;
            Severity = severity;
            Status = IssueStatus.Open;
            DiscoveryTime = discoveryTime;
            WorkOrderId = workOrderId;
            OperationId = operationId;
            ExpectedResolutionTime = expectedResolutionTime;
            HandlingRecords = new List<QualityIssueHandlingRecord>();
        }

        /// <summary>
        /// 添加处理记录
        /// </summary>
        public void AddHandlingRecord(QualityIssueHandlingRecord record)
        {
            HandlingRecords.Add(record);
        }

        /// <summary>
        /// 更新问题状态
        /// </summary>
        public void UpdateStatus(IssueStatus newStatus, string remarks = null)
        {
            if (Status == IssueStatus.Closed)
            {
                throw new InvalidOperationException("已关闭的问题不能更改状态");
            }

            Status = newStatus;

            if (newStatus == IssueStatus.Resolved || newStatus == IssueStatus.Closed)
            {
                ActualResolutionTime = DateTime.Now;
            }

            var record = new QualityIssueHandlingRecord(
                Guid.NewGuid(),
                Id,
                newStatus,
                remarks
            );

            AddHandlingRecord(record);
        }

        /// <summary>
        /// 更新解决方案
        /// </summary>
        public void UpdateSolution(string solution)
        {
            if (Status == IssueStatus.Closed)
            {
                throw new InvalidOperationException("已关闭的问题不能更新解决方案");
            }

            Solution = solution;
        }

        /// <summary>
        /// 更新预防措施
        /// </summary>
        public void UpdatePreventiveMeasures(string measures)
        {
            if (Status == IssueStatus.Closed)
            {
                throw new InvalidOperationException("已关闭的问题不能更新预防措施");
            }

            PreventiveMeasures = measures;
        }
    }

    /// <summary>
    /// 问题严重程度
    /// </summary>
    public enum IssueSeverity
    {
        /// <summary>
        /// 轻微
        /// </summary>
        Minor = 0,

        /// <summary>
        /// 一般
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 严重
        /// </summary>
        Severe = 2,

        /// <summary>
        /// 致命
        /// </summary>
        Critical = 3
    }

    /// <summary>
    /// 问题状态
    /// </summary>
    public enum IssueStatus
    {
        /// <summary>
        /// 待处理
        /// </summary>
        Open = 0,

        /// <summary>
        /// 处理中
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// 已解决
        /// </summary>
        Resolved = 2,

        /// <summary>
        /// 已关闭
        /// </summary>
        Closed = 3,

        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled = 4
    }
}