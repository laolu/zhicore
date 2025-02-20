using System;

namespace My.ZhiCore.Organization.WorkCenter
{
    /// <summary>
    /// 工作中心状态枚举
    /// </summary>
    public enum WorkCenterStatus
    {
        /// <summary>
        /// 空闲
        /// </summary>
        Idle = 0,

        /// <summary>
        /// 运行中
        /// </summary>
        Running = 1,

        /// <summary>
        /// 维护中
        /// </summary>
        Maintenance = 2,

        /// <summary>
        /// 故障
        /// </summary>
        Fault = 3
    }
}