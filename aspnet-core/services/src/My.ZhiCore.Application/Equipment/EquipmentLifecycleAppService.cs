using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备生命周期应用服务，用于管理设备的生命周期状态和转换
    /// </summary>
    public class EquipmentLifecycleAppService : ApplicationService
    {
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly ILogger<EquipmentLifecycleAppService> _logger;

        public EquipmentLifecycleAppService(
            IRepository<Equipment, Guid> equipmentRepository,
            ILogger<EquipmentLifecycleAppService> logger)
        {
            _equipmentRepository = equipmentRepository;
            _logger = logger;
        }

        /// <summary>
        /// 启用设备
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="remarks">备注</param>
        public async Task EnableAsync(Guid equipmentId, string remarks)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            equipment.UpdateStatus(EquipmentStatus.Active);
            await _equipmentRepository.UpdateAsync(equipment);

            _logger.LogInformation($"设备 {equipment.Name} 已启用，备注：{remarks}");
        }

        /// <summary>
        /// 禁用设备
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="remarks">备注</param>
        public async Task DisableAsync(Guid equipmentId, string remarks)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            equipment.UpdateStatus(EquipmentStatus.Disabled);
            await _equipmentRepository.UpdateAsync(equipment);

            _logger.LogInformation($"设备 {equipment.Name} 已禁用，备注：{remarks}");
        }

        /// <summary>
        /// 报废设备
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="reason">报废原因</param>
        /// <param name="remarks">备注</param>
        public async Task ScrapAsync(Guid equipmentId, string reason, string remarks)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            equipment.UpdateStatus(EquipmentStatus.Scrapped);
            await _equipmentRepository.UpdateAsync(equipment);

            _logger.LogInformation($"设备 {equipment.Name} 已报废，原因：{reason}，备注：{remarks}");
        }

        /// <summary>
        /// 转移设备
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="targetDepartmentId">目标部门ID</param>
        /// <param name="remarks">备注</param>
        public async Task TransferAsync(Guid equipmentId, Guid targetDepartmentId, string remarks)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            equipment.UpdateDepartment(targetDepartmentId);
            await _equipmentRepository.UpdateAsync(equipment);

            _logger.LogInformation($"设备 {equipment.Name} 已转移到部门 {targetDepartmentId}，备注：{remarks}");
        }
    }
}