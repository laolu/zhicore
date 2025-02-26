using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工艺步骤DTO
    /// </summary>
    public class RouteStepDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 步骤编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 步骤顺序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 所属工艺路线版本ID
        /// </summary>
        public Guid RouteVersionId { get; set; }

        /// <summary>
        /// 关联的工序信息
        /// </summary>
        public OperationDto Operation { get; set; }

        /// <summary>
        /// 关联的工序ID
        /// </summary>
        public Guid OperationId { get; set; }

        /// <summary>
        /// 前置步骤列表
        /// </summary>
        public List<RouteStepDto> PreviousSteps { get; set; }

        /// <summary>
        /// 后续步骤列表
        /// </summary>
        public List<RouteStepDto> NextSteps { get; set; }
    }

    /// <summary>
    /// 创建工艺步骤DTO
    /// </summary>
    public class CreateRouteStepDto
    {
        /// <summary>
        /// 步骤编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 步骤顺序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 关联的工序ID
        /// </summary>
        public Guid OperationId { get; set; }

        /// <summary>
        /// 前置步骤ID列表
        /// </summary>
        public List<Guid> PreviousStepIds { get; set; }
    }

    /// <summary>
    /// 更新工艺步骤DTO
    /// </summary>
    public class UpdateRouteStepDto
    {
        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 步骤顺序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 关联的工序ID
        /// </summary>
        public Guid OperationId { get; set; }

        /// <summary>
        /// 前置步骤ID列表
        /// </summary>
        public List<Guid> PreviousStepIds { get; set; }
    }
}