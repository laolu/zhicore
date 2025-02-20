namespace My.ZhiCore.Equipment
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
        /// <remarks>
        /// 包括以下情况：
        /// - 定期保养和检查
        /// - 预防性维护
        /// - 按设备使用手册要求的例行维护
        /// - 根据维护计划安排的升级和改造
        /// </remarks>
        Planned = 0,

        /// <summary>
        /// 计划外维护
        /// </summary>
        /// <remarks>
        /// 包括以下情况：
        /// - 设备突发故障维修
        /// - 紧急维护和抢修
        /// - 因生产需要的临时维护
        /// - 设备意外损坏的修复
        /// </remarks>
        Unplanned = 1,

        /// <summary>
        /// 日常保养
        /// </summary>
        /// <remarks>
        /// 包括以下情况：
        /// - 日常清洁和保养
        /// - 基础润滑和紧固件检查
        /// - 设备运行状态日常检查
        /// - 基本的维护记录
        /// </remarks>
        DailyMaintenance = 2,

        /// <summary>
        /// 预防性维护
        /// </summary>
        /// <remarks>
        /// 包括以下情况：
        /// - 定期检测和诊断
        /// - 零部件寿命预测
        /// - 预防性更换和调整
        /// - 设备性能优化
        /// </remarks>
        PreventiveMaintenance = 3,

        /// <summary>
        /// 定期维护
        /// </summary>
        /// <remarks>
        /// 包括以下情况：
        /// - 按周期进行的全面检查
        /// - 定期的零部件更换
        /// - 设备校准和调试
        /// - 性能测试和评估
        /// </remarks>
        RegularMaintenance = 4,

        /// <summary>
        /// 大修
        /// </summary>
        /// <remarks>
        /// 包括以下情况：
        /// - 设备全面拆检
        /// - 主要部件更换或修复
        /// - 设备升级改造
        /// - 全面性能恢复
        /// </remarks>
        Overhaul = 5
    }
}