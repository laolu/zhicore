using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;
using My.ZhiCore.Equipment.Events;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备管理器 - 负责设备实体的生命周期管理和业务规则验证
    /// </summary>
    public class EquipmentManager : DomainService
    {
        private readonly ILocalEventBus _localEventBus;
        private readonly IEquipmentRepository _equipmentRepository;

        public EquipmentManager(
            ILocalEventBus localEventBus,
            IEquipmentRepository equipmentRepository)
        {
            _localEventBus = localEventBus;
            _equipmentRepository = equipmentRepository;
        }

        /// <summary>
        /// 创建新设备
        /// </summary>
        /// <param name="code">设备编码</param>
        /// <param name="name">设备名称</param>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="operatorName">操作人名称</param>
        /// <param name="reason">创建原因</param>
        /// <param name="remark">备注</param>
        /// <returns>新创建的设备实体</returns>
        public async Task<Equipment> CreateAsync(
            string code,
            string name,
            Guid? operatorId = null,
            string operatorName = null,
            string reason = null,
            string remark = null)
        {
            await ValidateEquipmentCodeAsync(code);

            var equipment = new Equipment(GuidGenerator.Create(), code, name);

            await _equipmentRepository.InsertAsync(equipment);

            await _localEventBus.PublishAsync(
                new EquipmentCreatedEventData(
                    equipment.Id,
                    equipment.Code,
                    equipment.Name,
                    operatorId,
                    operatorName,
                    reason,
                    remark
                ));

            return equipment;
        }

        /// <summary>
        /// 更新设备信息
        /// </summary>
        /// <param name="equipment">设备实体</param>
        /// <param name="name">新的设备名称</param>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="operatorName">操作人名称</param>
        /// <param name="reason">更新原因</param>
        /// <param name="remark">备注</param>
        /// <returns>更新后的设备实体</returns>
        public async Task<Equipment> UpdateAsync(
            Equipment equipment,
            string name,
            Guid? operatorId = null,
            string operatorName = null,
            string reason = null,
            string remark = null)
        {
            equipment.ChangeName(name);

            await _equipmentRepository.UpdateAsync(equipment);

            await _localEventBus.PublishAsync(
                new EquipmentUpdatedEventData(
                    equipment.Id,
                    equipment.Code,
                    equipment.Name,
                    operatorId,
                    operatorName,
                    reason,
                    remark
                ));

            return equipment;
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="equipment">设备实体</param>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="operatorName">操作人名称</param>
        /// <param name="reason">删除原因</param>
        /// <param name="remark">备注</param>
        public async Task DeleteAsync(
            Equipment equipment,
            Guid? operatorId = null,
            string operatorName = null,
            string reason = null,
            string remark = null)
        {
            await _equipmentRepository.DeleteAsync(equipment);

            await _localEventBus.PublishAsync(
                new EquipmentDeletedEventData(
                    equipment.Id,
                    equipment.Code,
                    equipment.Name,
                    operatorId,
                    operatorName,
                    reason,
                    remark
                ));
        }

        /// <summary>
        /// 验证设备编码是否唯一
        /// </summary>
        /// <param name="code">设备编码</param>
        private async Task ValidateEquipmentCodeAsync(string code)
        {
            var existingEquipment = await _equipmentRepository.FindByCodeAsync(code);
            if (existingEquipment != null)
            {
                throw new EquipmentCodeAlreadyExistsException(code);
            }
        }

        /// <summary>
        /// 更新设备状态
        /// </summary>
        /// <param name="equipment">设备实体</param>
        /// <param name="status">新状态</param>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="operatorName">操作人名称</param>
        /// <param name="reason">状态变更原因</param>
        /// <param name="remark">备注</param>
        /// <returns>更新后的设备实体</returns>
        public async Task<Equipment> UpdateStatusAsync(
            Equipment equipment,
            EquipmentStatus status,
            Guid? operatorId = null,
            string operatorName = null,
            string reason = null,
            string remark = null)
        {
            equipment.ChangeStatus(status);

            await _equipmentRepository.UpdateAsync(equipment);

            await _localEventBus.PublishAsync(
                new EquipmentStatusChangedEventData(
                    equipment.Id,
                    equipment.Code,
                    equipment.Status,
                    operatorId,
                    operatorName,
                    reason,
                    remark
                ));

            return equipment;
        }

        /// <summary>
        /// 记录设备维护信息
        /// </summary>
        /// <param name="equipment">设备实体</param>
        /// <param name="maintenanceType">维护类型</param>
        /// <param name="maintenanceDate">维护日期</param>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="operatorName">操作人名称</param>
        /// <param name="description">维护描述</param>
        /// <param name="remark">备注</param>
        /// <returns>更新后的设备实体</returns>
        public async Task<Equipment> RecordMaintenanceAsync(
            Equipment equipment,
            MaintenanceType maintenanceType,
            DateTime maintenanceDate,
            Guid? operatorId = null,
            string operatorName = null,
            string description = null,
            string remark = null)
        {
            equipment.AddMaintenanceRecord(maintenanceType, maintenanceDate, description);

            await _equipmentRepository.UpdateAsync(equipment);

            await _localEventBus.PublishAsync(
                new EquipmentMaintenanceRecordedEventData(
                    equipment.Id,
                    equipment.Code,
                    maintenanceType,
                    maintenanceDate,
                    operatorId,
                    operatorName,
                    description,
                    remark
                ));

            return equipment;
        }

        /// <summary>
        /// 记录设备运行数据
        /// </summary>
        /// <param name="equipment">设备实体</param>
        /// <param name="operationData">运行数据</param>
        /// <param name="recordTime">记录时间</param>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="operatorName">操作人名称</param>
        /// <param name="remark">备注</param>
        /// <returns>更新后的设备实体</returns>
        public async Task<Equipment> RecordOperationDataAsync(
            Equipment equipment,
            EquipmentOperationData operationData,
            DateTime recordTime,
            Guid? operatorId = null,
            string operatorName = null,
            string remark = null)
        {
            equipment.AddOperationRecord(operationData, recordTime);

            await _equipmentRepository.UpdateAsync(equipment);

            await _localEventBus.PublishAsync(
                new EquipmentOperationDataRecordedEventData(
                    equipment.Id,
                    equipment.Code,
                    operationData,
                    recordTime,
                    operatorId,
                    operatorName,
                    remark
                ));

            return equipment;
        }
    }
}