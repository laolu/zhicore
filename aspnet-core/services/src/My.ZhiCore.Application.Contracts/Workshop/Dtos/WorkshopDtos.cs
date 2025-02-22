using System;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Workshop.Dtos
{
    /// <summary>
    /// 车间基础DTO
    /// </summary>
    public class WorkshopBaseDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 车间编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 车间名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 车间类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 车间状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        public Guid ManagerId { get; set; }

        /// <summary>
        /// 所属部门ID
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 创建车间DTO
    /// </summary>
    public class CreateWorkshopDto
    {
        /// <summary>
        /// 车间编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 车间名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 车间类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        public Guid ManagerId { get; set; }

        /// <summary>
        /// 所属部门ID
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 更新车间DTO
    /// </summary>
    public class UpdateWorkshopDto
    {
        /// <summary>
        /// 车间名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 车间类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        public Guid ManagerId { get; set; }

        /// <summary>
        /// 所属部门ID
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}