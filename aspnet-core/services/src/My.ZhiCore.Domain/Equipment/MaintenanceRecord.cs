using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备维护记录实体类，用于记录和追踪设备的维护历史
    /// </summary>
    /// <remarks>
    /// 该类提供以下功能：
    /// - 记录设备维护的基本信息（时间、人员、类型等）
    /// - 追踪维护结果和后续建议
    /// - 支持计划内和计划外维护的记录
    /// - 维护历史的查询和统计
    /// </remarks>
    public class MaintenanceRecord : Entity<Guid>
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; private set; }

        /// <summary>
        /// 维护时间
        /// </summary>
        public DateTime MaintenanceTime { get; private set; }

        /// <summary>
        /// 维护描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 维护人员
        /// </summary>
        public string Maintainer { get; private set; }

        /// <summary>
        /// 维护类型（计划内/计划外）
        /// </summary>
        public MaintenanceType MaintenanceType { get; private set; }

        /// <summary>
        /// 维护结果
        /// </summary>
        public string Result { get; private set; }

        /// <summary>
        /// 下次维护建议时间
        /// </summary>
        public DateTime? NextMaintenanceTime { get; private set; }

        protected MaintenanceRecord()
        {
        }

        public MaintenanceRecord(
            Guid id,
            Guid equipmentId,
            string description,
            string maintainer,
            MaintenanceType maintenanceType,
            string result,
            DateTime? nextMaintenanceTime = null)
        {
            Id = id;
            EquipmentId = equipmentId;
            Description = description;
            Maintainer = maintainer;
            MaintenanceType = maintenanceType;
            Result = result;
            MaintenanceTime = DateTime.Now;
            NextMaintenanceTime = nextMaintenanceTime;

            ValidateMaintenanceRecord();
        }

        private void ValidateMaintenanceRecord()
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                throw new ArgumentException("维护描述不能为空", nameof(Description));
            }

            if (string.IsNullOrWhiteSpace(Maintainer))
            {
                throw new ArgumentException("维护人员不能为空", nameof(Maintainer));
            }

            if (string.IsNullOrWhiteSpace(Result))
            {
                throw new ArgumentException("维护结果不能为空", nameof(Result));
            }

            if (NextMaintenanceTime.HasValue && NextMaintenanceTime.Value <= MaintenanceTime)
            {
                throw new ArgumentException("下次维护时间必须晚于当前维护时间", nameof(NextMaintenanceTime));
            }
        }
    }
}