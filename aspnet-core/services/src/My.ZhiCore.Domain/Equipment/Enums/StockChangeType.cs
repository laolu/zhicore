using System;

namespace My.ZhiCore.Equipment.Enums
{
    /// <summary>
    /// 库存变更类型枚举
    /// </summary>
    /// <remarks>
    /// 用于标识备件库存的变更类型：
    /// - 入库：增加库存数量
    /// - 出库：减少库存数量
    /// </remarks>
    public enum StockChangeType
    {
        /// <summary>
        /// 入库
        /// </summary>
        StockIn = 0,

        /// <summary>
        /// 出库
        /// </summary>
        StockOut = 1
    }
}