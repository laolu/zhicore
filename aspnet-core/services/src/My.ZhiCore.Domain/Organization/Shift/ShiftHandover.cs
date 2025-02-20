using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Production.Shift
{
    /// <summary>
    /// 班次交接记录实体 - 用于记录班次交接的设备状态和生产任务等信息
    /// </summary>
    public class ShiftHandover : Entity<Guid>
    {
        /// <summary>
        /// 班次ID
        /// </summary>
        public Guid ShiftId { get; private set; }

        /// <summary>
        /// 交接时间
        /// </summary>
        public DateTime HandoverTime { get; private set; }

        /// <summary>
        /// 设备状态描述
        /// </summary>
        public string EquipmentStatus { get; private set; }

        /// <summary>
        /// 生产任务完成情况描述
        /// </summary>
        public string TaskCompletion { get; private set; }

        /// <summary>
        /// 计划任务数量
        /// </summary>
        public int PlannedTaskCount { get; private set; }

        /// <summary>
        /// 完成任务数量
        /// </summary>
        public int CompletedTaskCount { get; private set; }

        /// <summary>
        /// 交接人ID
        /// </summary>
        public Guid HandoverEmployeeId { get; private set; }

        /// <summary>
        /// 接班人ID
        /// </summary>
        public Guid TakeoverEmployeeId { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; private set; }

        protected ShiftHandover() { }

        public ShiftHandover(
            Guid id,
            Guid shiftId,
            DateTime handoverTime,
            string equipmentStatus,
            string taskCompletion,
            int plannedTaskCount,
            int completedTaskCount,
            Guid handoverEmployeeId,
            Guid takeoverEmployeeId,
            string remarks = null)
        {
            Id = id;
            ShiftId = shiftId;
            HandoverTime = handoverTime;
            EquipmentStatus = equipmentStatus;
            TaskCompletion = taskCompletion;
            PlannedTaskCount = plannedTaskCount;
            CompletedTaskCount = completedTaskCount;
            HandoverEmployeeId = handoverEmployeeId;
            TakeoverEmployeeId = takeoverEmployeeId;
            Remarks = remarks;
        }
    }
}