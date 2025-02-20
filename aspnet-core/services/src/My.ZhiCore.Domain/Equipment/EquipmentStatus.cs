using System;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备状态枚举，用于表示设备在不同时期的运行状态
    /// </summary>
    public enum EquipmentStatus
    {
        /// <summary>
        /// 待机状态：设备已开启但未执行任务，随时可以投入使用
        /// </summary>
        Standby = 0,
    
        /// <summary>
        /// 运行状态：设备正在执行任务或生产作业
        /// </summary>
        Running = 1,
    
        /// <summary>
        /// 维护状态：设备正在进行维护、保养或检修
        /// </summary>
        Maintenance = 2,
    
        /// <summary>
        /// 故障状态：设备发生故障，需要维修
        /// </summary>
        Fault = 3,
    
        /// <summary>
        /// 空闲状态：设备处于关机或未使用状态
        /// </summary>
        Idle = 4,
    
        /// <summary>
        /// 设置状态：设备正在进行参数设置或调试
        /// </summary>
        Setup = 5,
    
        /// <summary>
        /// 关机状态：设备已完全关闭
        /// </summary>
        Shutdown = 6
    }
}