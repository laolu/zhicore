using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace My.ZhiCore.Material.BOM
{
    /// <summary>
    /// BOM项管理器，负责处理BOM项的业务逻辑和生命周期管理
    /// </summary>
    public class BomItemManager : DomainService
    {
        /// <summary>
        /// 创建新的BOM项
        /// </summary>
        /// <param name="billOfMaterialId">所属物料清单ID</param>
        /// <param name="materialId">物料ID</param>
        /// <param name="quantity">物料数量</param>
        /// <param name="unit">计量单位</param>
        /// <param name="position">安装位置</param>
        /// <param name="remark">备注说明</param>
        /// <param name="itemNo">项目序号</param>
        /// <returns>创建的BOM项实体</returns>
        public virtual async Task<BomItem> CreateAsync(
            Guid billOfMaterialId,
            Guid materialId,
            decimal quantity,
            string unit,
            string position,
            string remark,
            int itemNo)
        {
            // 验证数量必须大于0
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            // 创建新的BOM项实例
            var bomItem = new BomItem(
                GuidGenerator.Create(),
                billOfMaterialId,
                materialId,
                quantity,
                unit,
                position,
                remark,
                itemNo
            );

            return bomItem;
        }

        /// <summary>
        /// 更新BOM项的数量
        /// </summary>
        /// <param name="bomItem">BOM项实体</param>
        /// <param name="newQuantity">新的数量</param>
        /// <returns>更新后的BOM项实体</returns>
        public virtual async Task<BomItem> UpdateQuantityAsync(
            BomItem bomItem,
            decimal newQuantity)
        {
            var oldQuantity = bomItem.Quantity;
            bomItem.UpdateQuantity(newQuantity);

            // 发布BOM项变更事件
            await _localEventBus.PublishAsync(new BomItemChangedEto
            {
                Id = bomItem.Id,
                BillOfMaterialId = bomItem.BillOfMaterialId,
                MaterialId = bomItem.MaterialId,
                ChangeType = BomItemChangeType.QuantityChanged,
                OldQuantity = oldQuantity,
                NewQuantity = newQuantity
            });

            return bomItem;
        }

        /// <summary>
        /// 更新BOM项的安装位置
        /// </summary>
        /// <param name="bomItem">BOM项实体</param>
        /// <param name="newPosition">新的安装位置</param>
        /// <returns>更新后的BOM项实体</returns>
        public virtual async Task<BomItem> UpdatePositionAsync(
            BomItem bomItem,
            string newPosition)
        {
            var oldPosition = bomItem.Position;
            bomItem.UpdatePosition(newPosition);

            // 发布BOM项变更事件
            await _localEventBus.PublishAsync(new BomItemChangedEto
            {
                Id = bomItem.Id,
                BillOfMaterialId = bomItem.BillOfMaterialId,
                MaterialId = bomItem.MaterialId,
                ChangeType = BomItemChangeType.PositionChanged,
                OldPosition = oldPosition,
                NewPosition = newPosition
            });

            return bomItem;
        }

        /// <summary>
        /// 更新BOM项的备注说明
        /// </summary>
        /// <param name="bomItem">BOM项实体</param>
        /// <param name="newRemark">新的备注说明</param>
        /// <returns>更新后的BOM项实体</returns>
        public virtual async Task<BomItem> UpdateRemarkAsync(
            BomItem bomItem,
            string newRemark)
        {
            bomItem.UpdateRemark(newRemark);
            return bomItem;
        }

        /// <summary>
        /// 启用BOM项
        /// </summary>
        /// <param name="bomItem">BOM项实体</param>
        /// <returns>更新后的BOM项实体</returns>
        public virtual async Task<BomItem> ActivateAsync(BomItem bomItem)
        {
            bomItem.Activate();

            // 发布BOM项状态变更事件
            await _localEventBus.PublishAsync(new BomItemChangedEto
            {
                Id = bomItem.Id,
                BillOfMaterialId = bomItem.BillOfMaterialId,
                MaterialId = bomItem.MaterialId,
                ChangeType = BomItemChangeType.StatusChanged
            });

            return bomItem;
        }

        /// <summary>
        /// 禁用BOM项
        /// </summary>
        /// <param name="bomItem">BOM项实体</param>
        /// <returns>更新后的BOM项实体</returns>
        public virtual async Task<BomItem> DeactivateAsync(BomItem bomItem)
        {
            bomItem.Deactivate();

            // 发布BOM项状态变更事件
            await _localEventBus.PublishAsync(new BomItemChangedEto
            {
                Id = bomItem.Id,
                BillOfMaterialId = bomItem.BillOfMaterialId,
                MaterialId = bomItem.MaterialId,
                ChangeType = BomItemChangeType.StatusChanged
            });

            return bomItem;
        }
    }
}