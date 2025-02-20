using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Organization.Workshop
{
    /// <summary>
    /// 车间实体
    /// </summary>
    public class Workshop : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 车间名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 车间描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 车间类型
        /// </summary>
        public WorkshopType Type { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 生产能力/容量
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// 车间位置
        /// </summary>
        public string Location { get; set; }
    }
}