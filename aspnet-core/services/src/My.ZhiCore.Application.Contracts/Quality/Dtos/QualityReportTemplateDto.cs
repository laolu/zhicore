using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Quality.Dtos
{
    /// <summary>
    /// 质量报表模板DTO
    /// </summary>
    public class QualityReportTemplateDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模板描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 模板类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 模板参数定义
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }

    /// <summary>
    /// 创建质量报表模板DTO
    /// </summary>
    public class CreateQualityReportTemplateDto
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模板描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 模板类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 模板参数定义
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }

    /// <summary>
    /// 更新质量报表模板DTO
    /// </summary>
    public class UpdateQualityReportTemplateDto
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模板描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 模板类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 模板参数定义
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}