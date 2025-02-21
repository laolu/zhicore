using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备使用记录管理器
    /// </summary>
    public class EquipmentUsageRecordManager : DomainService
    {
        private readonly IRepository<EquipmentUsageRecord, Guid> _usageRecordRepository;
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly ILocalEventBus _localEventBus;

        public EquipmentUsageRecordManager(
            IRepository<EquipmentUsageRecord, Guid> usageRecordRepository,
            IRepository<Equipment, Guid> equipmentRepository,
            ILocalEventBus localEventBus)
        {
            _usageRecordRepository = usageRecordRepository;
            _equipmentRepository = equipmentRepository;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建新的设备使用记录
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="operatorName">操作人姓名</param>
        /// <param name="startTime">使用开始时间</param>
        /// <param name="purpose">使用目的</param>
        /// <param name="notes">备注信息（可选）</param>
        /// <returns>创建的使用记录</returns>
        public async Task<EquipmentUsageRecord> CreateUsageRecordAsync(
            Guid equipmentId,
            string operatorId,
            string operatorName,
            DateTime startTime,
            string purpose,
            string notes = null)
        {
            // 检查设备是否存在
            var equipment = await _equipmentRepository.GetAsync(equipmentId);

            // 检查是否有未完成的使用记录
            var hasActiveUsage = await HasActiveUsageAsync(equipmentId);
            if (hasActiveUsage)
            {
                throw new BusinessException("Equipment:EquipmentInUse")
                    .WithData("equipmentId", equipmentId);
            }

            var usageRecord = new EquipmentUsageRecord(
                GuidGenerator.Create(),
                equipmentId,
                operatorId,
                operatorName,
                startTime,
                purpose,
                notes
            );

            await _usageRecordRepository.InsertAsync(usageRecord);

            // 发布设备使用开始事件
            await _localEventBus.PublishAsync(new EquipmentUsageStartedEto
            {
                UsageRecordId = usageRecord.Id,
                EquipmentId = equipmentId,
                OperatorId = operatorId,
                StartTime = startTime
            });

            return usageRecord;
        }

        /// <summary>
        /// 完成设备使用记录
        /// </summary>
        /// <param name="usageRecordId">使用记录ID</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>更新后的使用记录</returns>
        public async Task<EquipmentUsageRecord> CompleteUsageRecordAsync(
            Guid usageRecordId,
            DateTime endTime)
        {
            var usageRecord = await _usageRecordRepository.GetAsync(usageRecordId);

            usageRecord.CompleteUsage(endTime);
            await _usageRecordRepository.UpdateAsync(usageRecord);

            // 发布设备使用结束事件
            await _localEventBus.PublishAsync(new EquipmentUsageCompletedEto
            {
                UsageRecordId = usageRecord.Id,
                EquipmentId = usageRecord.EquipmentId,
                EndTime = endTime,
                Duration = usageRecord.Duration.Value
            });

            return usageRecord;
        }

        /// <summary>
        /// 检查设备是否有未完成的使用记录
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <returns>如果有未完成的使用记录返回true，否则返回false</returns>
        private async Task<bool> HasActiveUsageAsync(Guid equipmentId)
        {
            return await _usageRecordRepository.AnyAsync(x =>
                x.EquipmentId == equipmentId && x.EndTime == null);
        }
    }
}