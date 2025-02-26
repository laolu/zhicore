using System;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序资源DTO
    /// </summary>
    public class OperationResourceDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 资源编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 资源描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 资源规格
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 所属工序ID
        /// </summary>
        public Guid OperationId { get; set; }
    }

    /// <summary>
    /// 创建工序资源DTO
    /// </summary>
    public class CreateOperationResourceDto
    {
        /// <summary>
        /// 资源编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 资源描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 资源规格
        /// </summary>
        public string Specification { get; set; }
    }

    /// <summary>
    /// 更新工序资源DTO
    /// </summary>
    public class UpdateOperationResourceDto
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 资源描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 资源规格
        /// </summary>
        public string Specification { get; set; }
    }
}