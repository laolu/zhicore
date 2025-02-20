using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Organization.Workshop
{
    /// <summary>
    /// 车间审计日志实体
    /// </summary>
    public class WorkshopAuditLog : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 车间ID
        /// </summary>
        public Guid WorkshopId { get; set; }

        /// <summary>
        /// 操作行为
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 操作详情
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }
}
{
    /// <summary>
    /// 车间审核日志
    /// </summary>
    public class WorkshopAuditLog : Entity<Guid>
    {
        /// <summary>
        /// 车间ID
        /// </summary>
        public Guid WorkshopId { get; private set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public WorkshopAuditOperationType OperationType { get; private set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; private set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; private set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public WorkshopAuditStatus Status { get; private set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Auditor { get; private set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditTime { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; private set; }

        protected WorkshopAuditLog() { }

        public WorkshopAuditLog(
            Guid id,
            Guid workshopId,
            WorkshopAuditOperationType operationType,
            string @operator,
            string remarks = null)
        {
            Id = id;
            WorkshopId = workshopId;
            OperationType = operationType;
            Operator = @operator ?? throw new ArgumentNullException(nameof(@operator));
            OperationTime = DateTime.Now;
            Status = WorkshopAuditStatus.Pending;
            Remarks = remarks;
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        public void Approve(string auditor)
        {
            if (Status != WorkshopAuditStatus.Pending)
            {
                throw new InvalidOperationException("只有待审核状态才能进行审核");
            }

            if (string.IsNullOrWhiteSpace(auditor))
            {
                throw new ArgumentException("审核人不能为空", nameof(auditor));
            }

            Status = WorkshopAuditStatus.Approved;
            Auditor = auditor;
            AuditTime = DateTime.Now;
        }

        /// <summary>
        /// 审核拒绝
        /// </summary>
        public void Reject(string auditor, string remarks)
        {
            if (Status != WorkshopAuditStatus.Pending)
            {
                throw new InvalidOperationException("只有待审核状态才能进行审核");
            }

            if (string.IsNullOrWhiteSpace(auditor))
            {
                throw new ArgumentException("审核人不能为空", nameof(auditor));
            }

            Status = WorkshopAuditStatus.Rejected;
            Auditor = auditor;
            AuditTime = DateTime.Now;
            Remarks = remarks;
        }
    }

    /// <summary>
    /// 车间审核操作类型
    /// </summary>
    public enum WorkshopAuditOperationType
    {
        /// <summary>
        /// 启用
        /// </summary>
        Activate = 1,

        /// <summary>
        /// 停用
        /// </summary>
        Deactivate = 2,

        /// <summary>
        /// 信息更新
        /// </summary>
        Update = 3
    }

    /// <summary>
    /// 车间审核状态
    /// </summary>
    public enum WorkshopAuditStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        Pending = 1,

        /// <summary>
        /// 已通过
        /// </summary>
        Approved = 2,

        /// <summary>
        /// 已拒绝
        /// </summary>
        Rejected = 3
    }
}