using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序DTO
    /// </summary>
    public class OperationDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 工序编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工序描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 工序类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 工序参数列表
        /// </summary>
        public List<OperationParameterDto> Parameters { get; set; }

        /// <summary>
        /// 工序资源列表
        /// </summary>
        public List<OperationResourceDto> Resources { get; set; }
    }

    /// <summary>
    /// 创建工序DTO
    /// </summary>
    public class CreateOperationDto
    {
        /// <summary>
        /// 工序编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工序描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 工序类型
        /// </summary>
        public string Type { get; set; }
    }

    /// <summary>
    /// 更新工序DTO
    /// </summary>
    public class UpdateOperationDto
    {
        /// <summary>
        /// 工序名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工序描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 工序类型
        /// </summary>
        public string Type { get; set; }
    }
}