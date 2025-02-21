using System;

namespace My.ZhiCore.Equipment.Enums
{
    /// <summary>
    /// 设备维护类型枚举，用于区分不同类型的设备维护活动
    /// </summary>
    /// <remarks>
    /// 该枚举用于标识设备维护的性质和计划性，包括：
    /// - 计划内维护：按照预定计划进行的常规维护活动
    /// - 计划外维护：因设备故障或其他突发情况而进行的维护活动
    /// - 日常保养：日常的基础维护和保养工作
    /// - 预防性维护：预防性的检查和维护工作
    /// - 定期维护：按固定周期进行的维护工作
    /// - 大修：全面的设备检修和更新
    /// </remarks>
    public enum MaintenanceType
    {
        /// <summary>
        /// 计划内维护
        /// </summary>
        Planned = 0,

        /// <summary>
        /// 计划外维护
        /// </summary>
        Unplanned = 1,

        /// <summary>
        /// 日常保养
        /// </summary>
        DailyMaintenance = 2,

        /// <summary>
        /// 预防性维护
        /// </summary>
        PreventiveMaintenance = 3,

        /// <summary>
        /// 定期维护
        /// </summary>
        PeriodicMaintenance = 4,

        /// <summary>
        /// 大修
        /// </summary>
        Overhaul = 5
    }
}