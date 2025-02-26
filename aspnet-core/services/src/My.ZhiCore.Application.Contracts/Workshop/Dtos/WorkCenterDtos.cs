using System;
using Volo.Abp.Application.Dtos;

namespace My.ZhiCore.Workshop.Dtos
{
    /// <summary>
    /// 工作中心基础DTO
    /// </summary>
    public class WorkCenterBaseDto : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 工作中心编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 工作中心名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工作中心类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 工作中心状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 所属车间ID
        /// </summary>
        public Guid WorkshopId { get; set; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        public Guid ManagerId { get; set; }

        /// <summary>
        /// 设备数量
        /// </summary>
        public int DeviceCount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 创建工作中心DTO
    /// </summary>
    public class CreateWorkCenterDto
    {
        /// <summary>
        /// 工作中心编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 工作中心名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工作中心类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 所属车间ID
        /// </summary>
        public Guid WorkshopId { get; set; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        public Guid ManagerId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 更新工作中心DTO
    /// </summary>
    public class UpdateWorkCenterDto
    {
        /// <summary>
        /// 工作中心名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工作中心类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        public Guid ManagerId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}