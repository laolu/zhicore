using System;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Quality
{
    /// <summary>
    /// 质量检验项目DTO
    /// </summary>
    public class QualityInspectionItemDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 检验标准
        /// </summary>
        public string Standard { get; set; }

        /// <summary>
        /// 检验方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 是否合格
        /// </summary>
        public bool IsQualified { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 所属质量检验ID
        /// </summary>
        public Guid InspectionId { get; set; }
    }

    /// <summary>
    /// 创建质量检验项目DTO
    /// </summary>
    public class CreateQualityInspectionItemDto
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 检验标准
        /// </summary>
        public string Standard { get; set; }

        /// <summary>
        /// 检验方法
        /// </summary>
        public string Method { get; set; }
    }

    /// <summary>
    /// 更新质量检验项目DTO
    /// </summary>
    public class UpdateQualityInspectionItemDto
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 检验标准
        /// </summary>
        public string Standard { get; set; }

        /// <summary>
        /// 检验方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 是否合格
        /// </summary>
        public bool IsQualified { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}