using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备维护记录管理器，负责处理设备维护记录相关的领域逻辑
    /// </summary>
    public class MaintenanceRecordManager : DomainService
    {
        private readonly IRepository<MaintenanceRecord, Guid> _maintenanceRecordRepository;
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly ILocalEventBus _localEventBus;

        public MaintenanceRecordManager(
            IRepository<MaintenanceRecord, Guid> maintenanceRecordRepository,
            IRepository<Equipment, Guid> equipmentRepository,
            ILocalEventBus localEventBus)
        {
            _maintenanceRecordRepository = maintenanceRecordRepository;
            _equipmentRepository = equipmentRepository;
            _localEventBus = localEventBus;
        }

        /// <summary>
        /// 创建维护记录
        /// </summary>
        public async Task<MaintenanceRecord> CreateAsync(
            Guid equipmentId,
            string description,
            string maintainer,
            MaintenanceType maintenanceType,
            string result,
            DateTime? nextMaintenanceTime = null)
        {
            // 验证设备是否存在
            var equipment = await _equipmentRepository.GetAsync(equipmentId);

            var maintenanceRecord = new MaintenanceRecord(
                GuidGenerator.Create(),
                equipmentId,
                description,
                maintainer,
                maintenanceType,
                result,
                nextMaintenanceTime
            );

            await _maintenanceRecordRepository.InsertAsync(maintenanceRecord);

            // 发布维护记录创建事件
            await _localEventBus.PublishAsync(new MaintenanceRecordCreatedEventData(
                maintenanceRecord.Id,
                maintenanceRecord.EquipmentId,
                maintenanceRecord.MaintenanceType,
                maintenanceRecord.MaintenanceTime,
                maintenanceRecord.NextMaintenanceTime
            ));

            return maintenanceRecord;
        }

        /// <summary>
        /// 更新下次维护时间
        /// </summary>
        public async Task UpdateNextMaintenanceTimeAsync(
            Guid maintenanceRecordId,
            DateTime nextMaintenanceTime)
        {
            var maintenanceRecord = await _maintenanceRecordRepository.GetAsync(maintenanceRecordId);

            // 验证新的维护时间是否有效
            if (nextMaintenanceTime <= maintenanceRecord.MaintenanceTime)
            {
                throw new ArgumentException("下次维护时间必须晚于当前维护时间", nameof(nextMaintenanceTime));
            }

            // 更新维护时间
            var updatedRecord = new MaintenanceRecord(
                maintenanceRecord.Id,
                maintenanceRecord.EquipmentId,
                maintenanceRecord.Description,
                maintenanceRecord.Maintainer,
                maintenanceRecord.MaintenanceType,
                maintenanceRecord.Result,
                nextMaintenanceTime
            );

            await _maintenanceRecordRepository.UpdateAsync(updatedRecord);

            // 发布维护计划更新事件
            await _localEventBus.PublishAsync(new MaintenanceScheduleUpdatedEventData(
                maintenanceRecord.Id,
                maintenanceRecord.EquipmentId,
                nextMaintenanceTime
            ));
        }
    }

    /// <summary>
    /// 维护记录创建事件数据
    /// </summary>
    public class MaintenanceRecordCreatedEventData
    {
        public Guid MaintenanceId { get; }
        public Guid EquipmentId { get; }
        public MaintenanceType MaintenanceType { get; }
        public DateTime MaintenanceTime { get; }
        public DateTime? NextMaintenanceTime { get; }

        public MaintenanceRecordCreatedEventData(
            Guid maintenanceId,
            Guid equipmentId,
            MaintenanceType maintenanceType,
            DateTime maintenanceTime,
            DateTime? nextMaintenanceTime)
        {
            MaintenanceId = maintenanceId;
            EquipmentId = equipmentId;
            MaintenanceType = maintenanceType;
            MaintenanceTime = maintenanceTime;
            NextMaintenanceTime = nextMaintenanceTime;
        }
    }

    /// <summary>
    /// 维护计划更新事件数据
    /// </summary>
    public class MaintenanceScheduleUpdatedEventData
    {
        public Guid MaintenanceId { get; }
        public Guid EquipmentId { get; }
        public DateTime NextMaintenanceTime { get; }

        public MaintenanceScheduleUpdatedEventData(
            Guid maintenanceId,
            Guid equipmentId,
            DateTime nextMaintenanceTime)
        {
            MaintenanceId = maintenanceId;
            EquipmentId = equipmentId;
            NextMaintenanceTime = nextMaintenanceTime;
        }
    }
}