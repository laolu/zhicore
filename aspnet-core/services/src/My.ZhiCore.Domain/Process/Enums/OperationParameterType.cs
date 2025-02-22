using System;

namespace My.ZhiCore.Production.Process
{
    /// <summary>
    /// 工艺参数类型
    /// </summary>
    public enum ParameterType
    {
        /// <summary>
        /// 数值型
        /// </summary>
        Numeric = 0,

        /// <summary>
        /// 文本型
        /// </summary>
        Text = 1,

        /// <summary>
        /// 布尔型
        /// </summary>
        Boolean = 2,

        /// <summary>
        /// 日期时间型
        /// </summary>
        DateTime = 3,

        /// <summary>
        /// 选项型
        /// </summary>
        Option = 4
    }
}