using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备校准应用服务，用于管理设备的校准记录和计划
    /// </summary>
    public class EquipmentCalibrationAppService : ApplicationService
    {
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly ILogger<EquipmentCalibrationAppService> _logger;

        public EquipmentCalibrationAppService(
            IRepository<Equipment, Guid> equipmentRepository,
            ILogger<EquipmentCalibrationAppService> logger)
        {
            _equipmentRepository = equipmentRepository;
            _logger = logger;
        }

        /// <summary>
        /// 创建校准计划
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="planDate">计划校准日期</param>
        /// <param name="calibrationType">校准类型</param>
        /// <param name="description">说明</param>
        public async Task CreateCalibrationPlanAsync(
            Guid equipmentId,
            DateTime planDate,
            string calibrationType,
            string description)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            
            var calibrationPlan = new EquipmentCalibrationPlan(
                GuidGenerator.Create(),
                equipmentId,
                planDate,
                calibrationType,
                description);

            await Repository.InsertAsync(calibrationPlan);
            _logger.LogInformation($"已为设备 {equipment.Name} 创建校准计划，计划日期：{planDate:yyyy-MM-dd}");
        }

        /// <summary>
        /// 记录校准结果
        /// </summary>
        /// <param name="planId">校准计划ID</param>
        /// <param name="calibrationResult">校准结果</param>
        /// <param name="actualDate">实际校准日期</param>
        /// <param name="remarks">备注</param>
        public async Task RecordCalibrationResultAsync(
            Guid planId,
            string calibrationResult,
            DateTime actualDate,
            string remarks)
        {
            var plan = await Repository.GetAsync<EquipmentCalibrationPlan>(planId);
            var equipment = await _equipmentRepository.GetAsync(plan.EquipmentId);

            var calibrationRecord = new EquipmentCalibrationRecord(
                GuidGenerator.Create(),
                plan.EquipmentId,
                planId,
                calibrationResult,
                actualDate,
                remarks);

            await Repository.InsertAsync(calibrationRecord);
            plan.Complete(actualDate, calibrationResult);
            await Repository.UpdateAsync(plan);

            _logger.LogInformation($"设备 {equipment.Name} 的校准计划已完成，实际校准日期：{actualDate:yyyy-MM-dd}");
        }

        /// <summary>
        /// 获取设备的校准计划列表
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="includeCompleted">是否包含已完成的计划</param>
        public async Task<List<EquipmentCalibrationPlan>> GetCalibrationPlansAsync(
            Guid equipmentId,
            bool includeCompleted = false)
        {
            return await Repository.GetListAsync<EquipmentCalibrationPlan>(
                p => p.EquipmentId == equipmentId && 
                     (includeCompleted || !p.IsCompleted));
        }

        /// <summary>
        /// 获取设备的校准记录
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public async Task<List<EquipmentCalibrationRecord>> GetCalibrationRecordsAsync(
            Guid equipmentId,
            DateTime startTime,
            DateTime endTime)
        {
            return await Repository.GetListAsync<EquipmentCalibrationRecord>(
                r => r.EquipmentId == equipmentId &&
                     r.ActualCalibrationDate >= startTime &&
                     r.ActualCalibrationDate <= endTime);
        }
    }
}