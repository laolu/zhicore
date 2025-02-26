using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工艺路线DTO
    /// </summary>
    public class RouteDto : FullAuditedEntityDto<Guid>
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
        public RouteVersionDto CurrentVersion { get; set; }

        /// <summary>
        /// 版本历史
        /// </summary>
        public List<RouteVersionDto> Versions { get; set; }
    }

    /// <summary>
    /// 创建工艺路线DTO
    /// </summary>
    public class CreateRouteDto
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
    }

    /// <summary>
    /// 更新工艺路线DTO
    /// </summary>
    public class UpdateRouteDto
    {
        /// <summary>
        /// 工艺路线名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工艺路线描述
        /// </summary>
        public string Description { get; set; }
    }
}