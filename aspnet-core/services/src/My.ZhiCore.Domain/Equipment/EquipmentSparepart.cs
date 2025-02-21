using System;
using My.ZhiCore.Equipment.Enums;
using My.ZhiCore.Equipment.Events;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Domain.Events;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备备件 - 用于管理设备的备件信息
    /// </summary>
    public class EquipmentSparepart : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 备件编码
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// 备件名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; private set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public string Manufacturer { get; private set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockQuantity { get; private set; }

        /// <summary>
        /// 最小库存警告值
        /// </summary>
        public int MinStockWarning { get; private set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; private set; }

        protected EquipmentSparepart()
        {
        }

        public EquipmentSparepart(
            Guid id,
            string code,
            string name,
            string specification,
            string manufacturer,
            int stockQuantity,
            int minStockWarning,
            string unit,
            string remark = null) : base(id)
        {
            Code = code;
            Name = name;
            Specification = specification;
            Manufacturer = manufacturer;
            StockQuantity = stockQuantity;
            MinStockWarning = minStockWarning;
            Unit = unit;
            Remark = remark;
        }

        /// <summary>
        /// 更新备件信息
        /// </summary>
        public void Update(
            string name,
            string specification,
            string manufacturer,
            int minStockWarning,
            string unit,
            string remark = null)
        {
            Name = name;
            Specification = specification;
            Manufacturer = manufacturer;
            MinStockWarning = minStockWarning;
            Unit = unit;
            Remark = remark;
        }

        /// <summary>
        /// 入库
        /// </summary>
        public void StockIn(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("入库数量必须大于0", nameof(quantity));
            }
            var oldQuantity = StockQuantity;
            StockQuantity += quantity;

            AddLocalEvent(new EquipmentSparepartStockChangedEvent
            {
                SparepartId = Id,
                OldQuantity = oldQuantity,
                NewQuantity = StockQuantity,
                ChangeType = StockChangeType.StockIn,
                ChangeQuantity = quantity
            });

            CheckAndRaiseLowStockWarning();
        }

        /// <summary>
        /// 出库
        /// </summary>
        public void StockOut(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("出库数量必须大于0", nameof(quantity));
            }
            if (StockQuantity < quantity)
            {
                throw new InvalidOperationException("库存不足");
            }
            var oldQuantity = StockQuantity;
            StockQuantity -= quantity;

            AddLocalEvent(new EquipmentSparepartStockChangedEvent
            {
                SparepartId = Id,
                OldQuantity = oldQuantity,
                NewQuantity = StockQuantity,
                ChangeType = StockChangeType.StockOut,
                ChangeQuantity = quantity
            });

            CheckAndRaiseLowStockWarning();
        }

        /// <summary>
        /// 检查是否库存不足
        /// </summary>
        public bool IsLowStock()
        {
            return StockQuantity <= MinStockWarning;
        }

        private void CheckAndRaiseLowStockWarning()
        {
            if (IsLowStock())
            {
                AddLocalEvent(new EquipmentSparepartLowStockWarningEvent
                {
                    SparepartId = Id,
                    CurrentQuantity = StockQuantity,
                    MinStockWarning = MinStockWarning
                });
            }
        }
    }


}