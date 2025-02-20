using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量检验计划
    /// </summary>
    public class QualityInspectionPlan : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        public string PlanNumber { get; private set; }

        /// <summary>
        /// 计划名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 检验类型
        /// </summary>
        public InspectionType Type { get; private set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime EndTime { get; private set; }

        /// <summary>
        /// 计划状态
        /// </summary>
        public PlanStatus Status { get; private set; }

        /// <summary>
        /// 检验项目列表
        /// </summary>
        private readonly List<InspectionItem> _inspectionItems;
        public IReadOnlyList<InspectionItem> InspectionItems => _inspectionItems.AsReadOnly();

        protected QualityInspectionPlan()
        {
            _inspectionItems = new List<InspectionItem>();
        }

        public QualityInspectionPlan(
            Guid id,
            string planNumber,
            string name,
            InspectionType type,
            DateTime startTime,
            DateTime endTime)
        {
            Id = id;
            PlanNumber = planNumber;
            Name = name;
            Type = type;
            StartTime = startTime;
            EndTime = endTime;
            Status = PlanStatus.Created;
            _inspectionItems = new List<InspectionItem>();
        }

        public void AddInspectionItem(string itemName, string standard, string method)
        {
            var item = new InspectionItem(
                Guid.NewGuid(),
                Id,
                itemName,
                standard,
                method);

            _inspectionItems.Add(item);
        }
    }
}