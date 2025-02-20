using System;

namespace My.ZhiCore.Organization.WorkCenter
{
    /// <summary>
    /// 工作中心类型
    /// </summary>
    public enum WorkCenterType
    {
        /// <summary>
        /// 装配工作中心
        /// </summary>
        Assembly = 1,

        /// <summary>
        /// 加工工作中心
        /// </summary>
        Processing = 2,

        /// <summary>
        /// 测试工作中心
        /// </summary>
        Testing = 3,

        /// <summary>
        /// 包装工作中心
        /// </summary>
        Packaging = 4,

        /// <summary>
        /// 维护工作中心
        /// </summary>
        Maintenance = 5,

        /// <summary>
        /// 质检工作中心
        /// </summary>
        Quality = 6,

        /// <summary>
        /// 仓储工作中心
        /// </summary>
        Storage = 7,

        /// <summary>
        /// 其他类型工作中心
        /// </summary>
        Other = 99
    }
}