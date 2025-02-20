using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Organization.WorkCenter
{
    /// <summary>
    /// 工作中心实体
    /// </summary>
    public class WorkCenter : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 工作中心名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工作中心描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 工作中心类型
        /// </summary>
        public WorkCenterType Type { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 工作中心位置
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 所属车间ID
        /// </summary>
        public Guid WorkshopId { get; set; }

        /// <summary>
        /// 工作中心下的设备列表
        /// </summary>
        public virtual ICollection<Equipment.Equipment> Equipments { get; private set; }

        protected WorkCenter()
        {
            Equipments = new List<Equipment.Equipment>();
        }
    }
}