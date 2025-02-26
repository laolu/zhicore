using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Production.Dtos
{
    /// <summary>
    /// 生产效率指标DTO
    /// </summary>
    public class EfficiencyIndicatorDto : EntityDto<Guid>
    {
        /// <summary>
        /// 指标名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 指标值
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 统计时间
        /// </summary>
        public DateTime StatisticsTime { get; set; }
    }

    /// <summary>
    /// 设备利用率统计DTO
    /// </summary>
    public class EquipmentUtilizationDto
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName { get; set; }

        /// <summary>
        /// 利用率
        /// </summary>
        public double UtilizationRate { get; set; }

        /// <summary>
        /// 运行时间（小时）
        /// </summary>
        public double RunningHours { get; set; }

        /// <summary>
        /// 停机时间（小时）
        /// </summary>
        public double DowntimeHours { get; set; }
    }

    /// <summary>
    /// 生产异常统计DTO
    /// </summary>
    public class ProductionAbnormalStatisticsDto
    {
        /// <summary>
        /// 异常类型
        /// </summary>
        public string AbnormalType { get; set; }

        /// <summary>
        /// 发生次数
        /// </summary>
        public int OccurrenceCount { get; set; }

        /// <summary>
        /// 影响时间（小时）
        /// </summary>
        public double ImpactHours { get; set; }

        /// <summary>
        /// 影响产量
        /// </summary>
        public int ImpactOutput { get; set; }
    }

    /// <summary>
    /// 生产质量统计DTO
    /// </summary>
    public class ProductionQualityStatisticsDto
    {
        /// <summary>
        /// 质量等级
        /// </summary>
        public string QualityLevel { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public double Percentage { get; set; }
    }
}