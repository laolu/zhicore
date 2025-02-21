using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备备件使用记录管理器
    /// </summary>
    public class EquipmentSparepartUsageRecordManager : DomainService
    {
        private readonly IRepository<EquipmentSparepartUsageRecord, Guid> _equipmentSparepartUsageRecordRepository;

        public EquipmentSparepartUsageRecordManager(
            IRepository<EquipmentSparepartUsageRecord, Guid> equipmentSparepartUsageRecordRepository)
        {
            _equipmentSparepartUsageRecordRepository = equipmentSparepartUsageRecordRepository;
        }

        /// <summary>
        /// 创建备件使用记录
        /// </summary>
        public async Task<EquipmentSparepartUsageRecord> CreateAsync(
            Guid sparepartId,
            int quantity,
            DateTime usageTime,
            string usageDescription,
            Guid? equipmentId = null,
            Guid? maintenanceWorkOrderId = null)
        {
            // 验证参数
            if (quantity <= 0)
            {
                throw new ArgumentException("使用数量必须大于0", nameof(quantity));
            }

            if (string.IsNullOrWhiteSpace(usageDescription))
            {
                throw new ArgumentException("用途描述不能为空", nameof(usageDescription));
            }

            // 创建使用记录
            var usageRecord = new EquipmentSparepartUsageRecord(
                GuidGenerator.Create(),
                sparepartId,
                quantity,
                usageTime,
                usageDescription,
                equipmentId,
                maintenanceWorkOrderId
            );

            // 保存到仓储
            await _equipmentSparepartUsageRecordRepository.InsertAsync(usageRecord);

            return usageRecord;
        }

        /// <summary>
        /// 更新备件使用记录
        /// </summary>
        public async Task<EquipmentSparepartUsageRecord> UpdateAsync(
            Guid id,
            int quantity,
            DateTime usageTime,
            string usageDescription,
            Guid? equipmentId = null,
            Guid? maintenanceWorkOrderId = null)
        {
            // 验证参数
            if (quantity <= 0)
            {
                throw new ArgumentException("使用数量必须大于0", nameof(quantity));
            }

            if (string.IsNullOrWhiteSpace(usageDescription))
            {
                throw new ArgumentException("用途描述不能为空", nameof(usageDescription));
            }

            // 获取使用记录
            var usageRecord = await _equipmentSparepartUsageRecordRepository.GetAsync(id);

            // 更新记录
            usageRecord.Update(
                quantity,
                usageTime,
                usageDescription,
                equipmentId,
                maintenanceWorkOrderId
            );

            // 保存更新
            await _equipmentSparepartUsageRecordRepository.UpdateAsync(usageRecord);

            return usageRecord;
        }
    }
}