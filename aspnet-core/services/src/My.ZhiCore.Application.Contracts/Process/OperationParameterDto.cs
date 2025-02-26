using System;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序参数DTO
    /// </summary>
    public class OperationParameterDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 参数编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 参数单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 所属工序ID
        /// </summary>
        public Guid OperationId { get; set; }
    }

    /// <summary>
    /// 创建工序参数DTO
    /// </summary>
    public class CreateOperationParameterDto
    {
        /// <summary>
        /// 参数编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 参数单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
    }

    /// <summary>
    /// 更新工序参数DTO
    /// </summary>
    public class UpdateOperationParameterDto
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 参数单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
    }
}