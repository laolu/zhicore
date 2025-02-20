using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量检验实体 - 用于管理生产过程中的质量检测
    /// </summary>
    public class QualityInspection : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 检验单号
        /// </summary>
        public string InspectionNumber { get; private set; }

        /// <summary>
        /// 工单ID
        /// </summary>
        public Guid WorkOrderId { get; private set; }

        /// <summary>
        /// 检验类型
        /// </summary>
        public InspectionType Type { get; private set; }

        /// <summary>
        /// 检验状态
        /// </summary>
        public InspectionStatus Status { get; private set; }

        /// <summary>
        /// 检验数量
        /// </summary>
        public int InspectionQuantity { get; private set; }

        /// <summary>
        /// 合格数量
        /// </summary>
        public int QualifiedQuantity { get; private set; }

        /// <summary>
        /// 不合格数量
        /// </summary>
        public int UnqualifiedQuantity { get; private set; }

        /// <summary>
        /// 检验项目
        /// </summary>
        private readonly List<InspectionItem> _items;
        public IReadOnlyList<InspectionItem> Items => _items.AsReadOnly();

        protected QualityInspection()
        {
            _items = new List<InspectionItem>();
        }

        public QualityInspection(
            Guid id,
            string inspectionNumber,
            Guid workOrderId,
            InspectionType type,
            int inspectionQuantity)
        {
            Id = id;
            InspectionNumber = inspectionNumber;
            WorkOrderId = workOrderId;
            Type = type;
            InspectionQuantity = inspectionQuantity;
            Status = InspectionStatus.Created;
            QualifiedQuantity = 0;
            UnqualifiedQuantity = 0;
            _items = new List<InspectionItem>();
        }

        public void AddInspectionItem(string itemName, string standard, string method)
        {
            var item = new InspectionItem(
                Guid.NewGuid(),
                Id,
                itemName,
                standard,
                method);

            _items.Add(item);
        }

        public void RecordItemResult(Guid itemId, bool isQualified, string result, string remark = null)
        {
            var item = _items.Find(x => x.Id == itemId);
            if (item == null)
                throw new InvalidOperationException("检验项目不存在");

            item.RecordResult(isQualified, result, remark);
        }

        public void Complete(int qualifiedQuantity, int unqualifiedQuantity)
        {
            if (Status != InspectionStatus.Created)
                throw new InvalidOperationException("只有新建状态的检验单才能完成检验");

            if (qualifiedQuantity + unqualifiedQuantity != InspectionQuantity)
                throw new ArgumentException("合格数量与不合格数量之和必须等于检验数量");

            QualifiedQuantity = qualifiedQuantity;
            UnqualifiedQuantity = unqualifiedQuantity;
            Status = InspectionStatus.Completed;
        }

        public bool IsAllItemsCompleted()
        {
            return _items.TrueForAll(x => x.IsCompleted);
        }
    }

    /// <summary>
    /// 检验项目
    /// </summary>
    public class InspectionItem : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 检验单ID
        /// </summary>
        public Guid InspectionId { get; private set; }

        /// <summary>
        /// 检验项目名称
        /// </summary>
        public string ItemName { get; private set; }

        /// <summary>
        /// 检验标准
        /// </summary>
        public string Standard { get; private set; }

        /// <summary>
        /// 检验方法
        /// </summary>
        public string Method { get; private set; }

        /// <summary>
        /// 是否合格
        /// </summary>
        public bool? IsQualified { get; private set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        public string Result { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; private set; }

        /// <summary>
        /// 是否已完成检验
        /// </summary>
        public bool IsCompleted { get; private set; }

        protected InspectionItem() { }

        public InspectionItem(
            Guid id,
            Guid inspectionId,
            string itemName,
            string standard,
            string method)
        {
            Id = id;
            InspectionId = inspectionId;
            ItemName = itemName;
            Standard = standard;
            Method = method;
            IsCompleted = false;
        }

        public void RecordResult(bool isQualified, string result, string remark = null)
        {
            if (IsCompleted)
                throw new InvalidOperationException("检验项目已完成，不能重复记录结果");

            IsQualified = isQualified;
            Result = result;
            Remark = remark;
            IsCompleted = true;
        }
    }
}