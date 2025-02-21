using System;

namespace My.ZhiCore.Equipment.Enums
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
    }
}