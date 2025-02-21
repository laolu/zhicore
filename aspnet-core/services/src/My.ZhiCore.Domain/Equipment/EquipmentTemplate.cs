using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备模板 - 用于标准化设备的创建和管理
    /// </summary>
    public class EquipmentTemplate : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 模板编码
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string EquipmentType { get; private set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; private set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public string Manufacturer { get; private set; }

        /// <summary>
        /// 标准维护周期（天）
        /// </summary>
        public int MaintenancePeriod { get; private set; }

        /// <summary>
        /// 技术参数
        /// </summary>
        public Dictionary<string, string> TechnicalParameters { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; private set; }

        protected EquipmentTemplate()
        {
            TechnicalParameters = new Dictionary<string, string>();
        }

        public EquipmentTemplate(
            Guid id,
            string code,
            string name,
            string equipmentType,
            string specification,
            string manufacturer,
            int maintenancePeriod,
            string remark = null) : base(id)
        {
            Code = code;
            Name = name;
            EquipmentType = equipmentType;
            Specification = specification;
            Manufacturer = manufacturer;
            MaintenancePeriod = maintenancePeriod;
            Remark = remark;
            TechnicalParameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// 更新模板信息
        /// </summary>
        public void Update(
            string name,
            string equipmentType,
            string specification,
            string manufacturer,
            int maintenancePeriod,
            string remark = null)
        {
            Name = name;
            EquipmentType = equipmentType;
            Specification = specification;
            Manufacturer = manufacturer;
            MaintenancePeriod = maintenancePeriod;
            Remark = remark;
        }

        /// <summary>
        /// 添加技术参数
        /// </summary>
        public void AddTechnicalParameter(string key, string value)
        {
            TechnicalParameters[key] = value;
        }

        /// <summary>
        /// 移除技术参数
        /// </summary>
        public void RemoveTechnicalParameter(string key)
        {
            TechnicalParameters.Remove(key);
        }

        /// <summary>
        /// 清空技术参数
        /// </summary>
        public void ClearTechnicalParameters()
        {
            TechnicalParameters.Clear();
        }
    }
}