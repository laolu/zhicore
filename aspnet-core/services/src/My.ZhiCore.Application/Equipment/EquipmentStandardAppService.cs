using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备标准应用服务，用于管理设备的标准和规范
    /// </summary>
    public class EquipmentStandardAppService : ApplicationService
    {
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly ILogger<EquipmentStandardAppService> _logger;

        public EquipmentStandardAppService(
            IRepository<Equipment, Guid> equipmentRepository,
            ILogger<EquipmentStandardAppService> logger)
        {
            _equipmentRepository = equipmentRepository;
            _logger = logger;
        }

        /// <summary>
        /// 设置设备标准参数
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="standardParameters">标准参数</param>
        public async Task SetStandardParametersAsync(
            Guid equipmentId,
            Dictionary<string, string> standardParameters)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            equipment.UpdateStandardParameters(standardParameters);
            await _equipmentRepository.UpdateAsync(equipment);

            _logger.LogInformation($"设备 {equipment.Name} 的标准参数已更新");
        }

        /// <summary>
        /// 设置设备质量标准
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="qualityStandards">质量标准</param>
        public async Task SetQualityStandardsAsync(
            Guid equipmentId,
            Dictionary<string, string> qualityStandards)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            equipment.UpdateQualityStandards(qualityStandards);
            await _equipmentRepository.UpdateAsync(equipment);

            _logger.LogInformation($"设备 {equipment.Name} 的质量标准已更新");
        }

        /// <summary>
        /// 设置设备安全标准
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="safetyStandards">安全标准</param>
        public async Task SetSafetyStandardsAsync(
            Guid equipmentId,
            Dictionary<string, string> safetyStandards)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            equipment.UpdateSafetyStandards(safetyStandards);
            await _equipmentRepository.UpdateAsync(equipment);

            _logger.LogInformation($"设备 {equipment.Name} 的安全标准已更新");
        }

        /// <summary>
        /// 设置设备维护标准
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="maintenanceStandards">维护标准</param>
        public async Task SetMaintenanceStandardsAsync(
            Guid equipmentId,
            Dictionary<string, string> maintenanceStandards)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            equipment.UpdateMaintenanceStandards(maintenanceStandards);
            await _equipmentRepository.UpdateAsync(equipment);

            _logger.LogInformation($"设备 {equipment.Name} 的维护标准已更新");
        }
    }
}