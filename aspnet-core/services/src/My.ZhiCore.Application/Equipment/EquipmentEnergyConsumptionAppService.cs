using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备能耗应用服务，用于分析和管理设备的能源消耗情况
    /// </summary>
    public class EquipmentEnergyConsumptionAppService : ApplicationService
    {
        private readonly IRepository<Equipment, Guid> _equipmentRepository;
        private readonly ILogger<EquipmentEnergyConsumptionAppService> _logger;

        public EquipmentEnergyConsumptionAppService(
            IRepository<Equipment, Guid> equipmentRepository,
            ILogger<EquipmentEnergyConsumptionAppService> logger)
        {
            _equipmentRepository = equipmentRepository;
            _logger = logger;
        }

        /// <summary>
        /// 记录设备能耗数据
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="energyType">能源类型（如：电力、水、气等）</param>
        /// <param name="consumption">消耗量</param>
        /// <param name="unit">单位</param>
        /// <param name="recordTime">记录时间</param>
        public async Task RecordEnergyConsumptionAsync(
            Guid equipmentId,
            string energyType,
            decimal consumption,
            string unit,
            DateTime recordTime)
        {
            var equipment = await _equipmentRepository.GetAsync(equipmentId);
            
            var consumptionRecord = new EquipmentEnergyConsumptionRecord(
                GuidGenerator.Create(),
                equipmentId,
                energyType,
                consumption,
                unit,
                recordTime);

            await Repository.InsertAsync(consumptionRecord);
            _logger.LogInformation($"已记录设备 {equipment.Name} 的{energyType}能耗数据：{consumption}{unit}");
        }

        /// <summary>
        /// 获取设备能耗统计
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="energyType">能源类型</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public async Task<EquipmentEnergyConsumptionStatistics> GetConsumptionStatisticsAsync(
            Guid equipmentId,
            string energyType,
            DateTime startTime,
            DateTime endTime)
        {
            var records = await Repository.GetListAsync<EquipmentEnergyConsumptionRecord>(
                r => r.EquipmentId == equipmentId &&
                     r.EnergyType == energyType &&
                     r.RecordTime >= startTime &&
                     r.RecordTime <= endTime);

            var totalConsumption = records.Sum(r => r.Consumption);
            var averageConsumption = records.Any() ? totalConsumption / records.Count : 0;

            return new EquipmentEnergyConsumptionStatistics
            {
                EquipmentId = equipmentId,
                EnergyType = energyType,
                TotalConsumption = totalConsumption,
                AverageConsumption = averageConsumption,
                Unit = records.FirstOrDefault()?.Unit,
                StartTime = startTime,
                EndTime = endTime
            };
        }

        /// <summary>
        /// 分析设备能耗趋势
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <param name="energyType">能源类型</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="interval">统计间隔（小时）</param>
        public async Task<List<EquipmentEnergyConsumptionTrend>> AnalyzeConsumptionTrendAsync(
            Guid equipmentId,
            string energyType,
            DateTime startTime,
            DateTime endTime,
            int interval)
        {
            var records = await Repository.GetListAsync<EquipmentEnergyConsumptionRecord>(
                r => r.EquipmentId == equipmentId &&
                     r.EnergyType == energyType &&
                     r.RecordTime >= startTime &&
                     r.RecordTime <= endTime);

            var trends = records
                .GroupBy(r => new DateTime(
                    r.RecordTime.Year,
                    r.RecordTime.Month,
                    r.RecordTime.Day,
                    r.RecordTime.Hour / interval * interval,
                    0,
                    0))
                .Select(g => new EquipmentEnergyConsumptionTrend
                {
                    TimePoint = g.Key,
                    Consumption = g.Sum(r => r.Consumption),
                    Unit = g.First().Unit
                })
                .OrderBy(t => t.TimePoint)
                .ToList();

            return trends;
        }
    }
}