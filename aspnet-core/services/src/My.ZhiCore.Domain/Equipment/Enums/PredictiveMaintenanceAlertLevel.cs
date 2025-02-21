using System;

namespace My.ZhiCore.Equipment.Enums
{
    /// <summary>
    /// 预测性维护预警级别枚举
    /// </summary>
    /// <remarks>
    /// 用于标识设备运行状态的严重程度，帮助维护人员快速识别和响应潜在问题
    /// </remarks>
    public enum PredictiveMaintenanceAlertLevel
    {
        /// <summary>
        /// 正常：设备运行状态良好，无需特别关注
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 注意：设备出现轻微异常，需要关注
        /// </summary>
        Attention = 1,

        /// <summary>
        /// 警告：设备存在明显异常，需要及时处理
        /// </summary>
        Warning = 2,

        /// <summary>
        /// 严重：设备存在严重问题，需要立即处理
        /// </summary>
        Critical = 3
    }
}