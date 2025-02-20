using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量成本实体
    /// </summary>
    public class QualityCost : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 成本编号
        /// </summary>
        public string CostNumber { get; private set; }

        /// <summary>
        /// 成本类型
        /// </summary>
        public QualityCostType Type { get; private set; }

        /// <summary>
        /// 成本项目
        /// </summary>
        public string CostItem { get; private set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime OccurrenceTime { get; private set; }

        /// <summary>
        /// 金额（元）
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// 相关工单ID
        /// </summary>
        public Guid? WorkOrderId { get; private set; }

        /// <summary>
        /// 相关质量问题ID
        /// </summary>
        public Guid? QualityIssueId { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; private set; }

        protected QualityCost()
        {
        }

        public QualityCost(
            Guid id,
            string costNumber,
            QualityCostType type,
            string costItem,
            decimal amount,
            Guid? workOrderId = null,
            Guid? qualityIssueId = null,
            string remark = null)
        {
            Id = id;
            CostNumber = costNumber;
            Type = type;
            CostItem = costItem;
            Amount = amount;
            WorkOrderId = workOrderId;
            QualityIssueId = qualityIssueId;
            OccurrenceTime = DateTime.Now;
            Remark = remark;
        }
    }

    public enum QualityCostType
    {
        /// <summary>
        /// 预防成本
        /// </summary>
        Prevention,

        /// <summary>
        /// 评估成本
        /// </summary>
        Appraisal,

        /// <summary>
        /// 内部失效成本
        /// </summary>
        InternalFailure,

        /// <summary>
        /// 外部失效成本
        /// </summary>
        ExternalFailure
    }
}