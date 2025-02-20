using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Material.BOM
{
    /// <summary>
    /// 物料清单项实体，用于描述BOM中的具体物料组成项
    /// </summary>
    public class BomItem : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>所属物料清单ID</summary>
        public Guid BillOfMaterialId { get; private set; }
        /// <summary>物料ID</summary>
        public Guid MaterialId { get; private set; }

        /// <summary>物料数量</summary>
        public decimal Quantity { get; private set; }

        /// <summary>计量单位</summary>
        public string Unit { get; private set; }

        /// <summary>安装位置</summary>
        public string Position { get; private set; }

        /// <summary>备注说明</summary>
        public string Remark { get; private set; }

        /// <summary>项目序号</summary>
        public int ItemNo { get; private set; }

        /// <summary>是否启用</summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// 保护的构造函数，供ORM使用
        /// </summary>
        protected BomItem() { }

        /// <summary>
        /// 创建物料清单项
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <param name="billOfMaterialId">所属物料清单ID</param>
        /// <param name="materialId">物料ID</param>
        /// <param name="quantity">物料数量</param>
        /// <param name="unit">计量单位</param>
        /// <param name="position">安装位置</param>
        /// <param name="remark">备注说明</param>
        /// <param name="itemNo">项目序号</param>
        public BomItem(
            Guid id,
            Guid billOfMaterialId,
            Guid materialId,
            decimal quantity,
            string unit,
            string position,
            string remark,
            int itemNo) : base(id)
        {
            BillOfMaterialId = billOfMaterialId;
            MaterialId = materialId;
            Quantity = quantity;
            Unit = unit;
            Position = position;
            Remark = remark;
            ItemNo = itemNo;
            IsActive = true;
        }

        /// <summary>
        /// 更新物料数量
        /// </summary>
        /// <param name="newQuantity">新的物料数量（必须大于0）</param>
        /// <exception cref="ArgumentException">当数量小于或等于0时抛出</exception>
        public void UpdateQuantity(decimal newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(newQuantity));

            Quantity = newQuantity;
        }

        /// <summary>
        /// 更新安装位置
        /// </summary>
        /// <param name="newPosition">新的安装位置</param>
        public void UpdatePosition(string newPosition)
        {
            Position = newPosition;
        }

        /// <summary>
        /// 更新备注说明
        /// </summary>
        /// <param name="newRemark">新的备注说明</param>
        public void UpdateRemark(string newRemark)
        {
            Remark = newRemark;
        }

        /// <summary>
        /// 启用物料清单项
        /// </summary>
        public void Activate()
        {
            IsActive = true;
        }

        /// <summary>
        /// 禁用物料清单项
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}