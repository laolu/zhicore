using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Production.Shift
{
    /// <summary>
    /// 班次人员实体 - 用于管理班次中的人员信息
    /// </summary>
    public class ShiftPersonnel : Entity<Guid>
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public Guid EmployeeId { get; private set; }

        /// <summary>
        /// 班次ID
        /// </summary>
        public Guid ShiftId { get; private set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; private set; }

        protected ShiftPersonnel() { }

        public ShiftPersonnel(
            Guid id,
            Guid employeeId,
            Guid shiftId,
            string position,
            string remarks = null)
        {
            Id = id;
            EmployeeId = employeeId;
            ShiftId = shiftId;
            Position = position;
            Remarks = remarks;
        }
    }
}