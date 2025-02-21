using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备备件使用记录 - 用于记录备件的使用情况
    /// </summary>
    public class EquipmentSparepartUsageRecord : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 备件ID
        /// </summary>
        public Guid SparepartId { get; private set; }

        /// <summary>
        /// 使用数量
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime UsageTime { get; private set; }

        /// <summary>
        /// 使用设备ID
        /// </summary>
        public Guid? EquipmentId { get; private set; }

        /// <summary>
        /// 用途描述
        /// </summary>
        public string UsageDescription { get; private set; }

        /// <summary>
        /// 维修工单ID（如果是用于维修）
        /// </summary>
        public Guid? MaintenanceWorkOrderId { get; private set; }

        protected EquipmentSparepartUsageRecord()
        {
        }

        public EquipmentSparepartUsageRecord(
            Guid id,
            Guid sparepartId,
            int quantity,
            DateTime usageTime,
            string usageDescription,
            Guid? equipmentId = null,
            Guid? maintenanceWorkOrderId = null) : base(id)
        {
            SparepartId = sparepartId;
            Quantity = quantity;
            UsageTime = usageTime;
            UsageDescription = usageDescription;
            EquipmentId = equipmentId;
            MaintenanceWorkOrderId = maintenanceWorkOrderId;
        }

        /// <summary>
        /// 更新使用记录信息
        /// </summary>
        public void Update(
            int quantity,
            DateTime usageTime,
            string usageDescription,
            Guid? equipmentId = null,
            Guid? maintenanceWorkOrderId = null)
        {
            Quantity = quantity;
            UsageTime = usageTime;
            UsageDescription = usageDescription;
            EquipmentId = equipmentId;
            MaintenanceWorkOrderId = maintenanceWorkOrderId;
        }
    }
}