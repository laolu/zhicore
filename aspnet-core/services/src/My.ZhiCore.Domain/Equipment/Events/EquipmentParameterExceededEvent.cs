using System;

namespace My.ZhiCore.Equipment.Events
{
    /// <summary>
    /// 设备参数超限事件
    /// </summary>
    public class EquipmentParameterExceededEvent : EquipmentEventData
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; }

        /// <summary>
        /// 参数编码
        /// </summary>
        public string ParameterCode { get; }

        /// <summary>
        /// 当前值
        /// </summary>
        public decimal CurrentValue { get; }

        /// <summary>
        /// 上限值
        /// </summary>
        public decimal? UpperLimit { get; }

        /// <summary>
        /// 下限值
        /// </summary>
        public decimal? LowerLimit { get; }

        /// <summary>
        /// 超限类型（1：超上限，-1：超下限）
        /// </summary>
        public int ExceededType { get; }

        /// <summary>
        /// 超限持续时间（秒）
        /// </summary>
        public int DurationSeconds { get; }

        public EquipmentParameterExceededEvent(
            Guid equipmentId,
            string equipmentCode,
            string equipmentName,
            string parameterName,
            string parameterCode,
            decimal currentValue,
            decimal? upperLimit,
            decimal? lowerLimit,
            int exceededType,
            int durationSeconds,
            Guid? operatorId = null,
            string operatorName = null,
            string reason = null,
            string remark = null) 
            : base(equipmentId, equipmentCode, equipmentName, operatorId, operatorName, reason, remark)
        {
            ParameterName = parameterName;
            ParameterCode = parameterCode;
            CurrentValue = currentValue;
            UpperLimit = upperLimit;
            LowerLimit = lowerLimit;
            ExceededType = exceededType;
            DurationSeconds = durationSeconds;
        }
    }
}