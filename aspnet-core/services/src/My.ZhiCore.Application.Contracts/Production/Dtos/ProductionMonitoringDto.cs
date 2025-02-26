using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Production.Dtos
{
    /// <summary>
    /// 生产监控DTO
    /// </summary>
    public class ProductionMonitoringDto : EntityDto<Guid>
    {
        /// <summary>
        /// 生产线ID
        /// </summary>
        public Guid ProductionLineId { get; set; }

        /// <summary>
        /// 工单ID
        /// </summary>
        public Guid WorkOrderId { get; set; }

        /// <summary>
        /// 监控时间
        /// </summary>
        public DateTime MonitoringTime { get; set; }

        /// <summary>
        /// 生产状态
        /// </summary>
        public ProductionStatus Status { get; set; }

        /// <summary>
        /// 当前产量
        /// </summary>
        public int CurrentQuantity { get; set; }

        /// <summary>
        /// 目标产量
        /// </summary>
        public int TargetQuantity { get; set; }

        /// <summary>
        /// 生产效率
        /// </summary>
        public decimal Efficiency { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        public EquipmentStatus EquipmentStatus { get; set; }

        /// <summary>
        /// 质量合格率
        /// </summary>
        public decimal QualityRate { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public List<ProductionAbnormalDto> Abnormals { get; set; }
    }

    /// <summary>
    /// 生产异常DTO
    /// </summary>
    public class ProductionAbnormalDto
    {
        /// <summary>
        /// 异常类型
        /// </summary>
        public string AbnormalType { get; set; }

        /// <summary>
        /// 异常描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime OccurTime { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public AbnormalStatus Status { get; set; }
    }

    /// <summary>
    /// 生产状态枚举
    /// </summary>
    public enum ProductionStatus
    {
        /// <summary>
        /// 停机
        /// </summary>
        Stopped = 0,

        /// <summary>
        /// 运行中
        /// </summary>
        Running = 1,

        /// <summary>
        /// 待机
        /// </summary>
        Standby = 2,

        /// <summary>
        /// 故障
        /// </summary>
        Fault = 3
    }

    /// <summary>
    /// 设备状态枚举
    /// </summary>
    public enum EquipmentStatus
    {
        /// <summary>
        /// 离线
        /// </summary>
        Offline = 0,

        /// <summary>
        /// 在线
        /// </summary>
        Online = 1,

        /// <summary>
        /// 故障
        /// </summary>
        Fault = 2,

        /// <summary>
        /// 维护中
        /// </summary>
        Maintenance = 3
    }

    /// <summary>
    /// 异常状态枚举
    /// </summary>
    public enum AbnormalStatus
    {
        /// <summary>
        /// 未处理
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 处理中
        /// </summary>
        Processing = 1,

        /// <summary>
        /// 已解决
        /// </summary>
        Resolved = 2
    }
}