using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工艺路线版本
    /// </summary>
    public class RouteVersion : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 版本描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否为当前版本
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 所属工艺路线
        /// </summary>
        public virtual Route Route { get; set; }
        public Guid RouteId { get; set; }

        /// <summary>
        /// 工艺步骤列表
        /// </summary>
        public virtual ICollection<RouteStep> Steps { get; set; }

        protected RouteVersion()
        {
            Steps = new List<RouteStep>();
        }

        public RouteVersion(Guid id, Guid routeId, string version, string description = null)
            : base(id)
        {
            RouteId = routeId;
            Version = version;
            Description = description;
            IsCurrent = false;
            Steps = new List<RouteStep>();
        }
    }
}