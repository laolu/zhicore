using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工艺路线
    /// </summary>
    public class Route : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 工艺路线编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 工艺路线名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工艺路线描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 当前版本
        /// </summary>
        public virtual RouteVersion CurrentVersion { get; set; }

        /// <summary>
        /// 版本历史
        /// </summary>
        public virtual ICollection<RouteVersion> Versions { get; set; }

        protected Route()
        {
            Versions = new List<RouteVersion>();
        }

        public Route(Guid id, string code, string name, string description = null)
            : base(id)
        {
            Code = code;
            Name = name;
            Description = description;
            Versions = new List<RouteVersion>();
        }
    }
}