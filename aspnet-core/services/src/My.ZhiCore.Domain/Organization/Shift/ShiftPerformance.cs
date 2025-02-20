using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Production.Shift
{
    /// <summary>
    /// 班次绩效记录实体 - 用于记录班次的生产绩效指标
    /// </summary>
    public class ShiftPerformance : Entity<Guid>
    {
        /// <summary>
        /// 班次ID
        /// </summary>
        public Guid ShiftId { get; private set; }

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatisticsDate { get; private set; }

        /// <summary>
        /// 计划生产数量
        /// </summary>
        public int PlannedProductionCount { get; private set; }

        /// <summary>
        /// 实际生产数量
        /// </summary>
        public int ActualProductionCount { get; private set; }

        /// <summary>
        /// 合格品数量
        /// </summary>
        public int QualifiedCount { get; private set; }

        /// <summary>
        /// 不合格品数量
        /// </summary>
        public int UnqualifiedCount { get; private set; }

        /// <summary>
        /// 生产任务完成率
        /// </summary>
        public decimal TaskCompletionRate { get; private set; }

        /// <summary>
        /// 产品合格率
        /// </summary>
        public decimal QualificationRate { get; private set; }

        /// <summary>
        /// 设备运行时间（分钟）
        /// </summary>
        public int EquipmentRunningTime { get; private set; }

        /// <summary>
        /// 设备故障时间（分钟）
        /// </summary>
        public int EquipmentFailureTime { get; private set; }

        /// <summary>
        /// 设备利用率
        /// </summary>
        public decimal EquipmentUtilizationRate { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; private set; }

        protected ShiftPerformance() { }

        public ShiftPerformance(
            Guid id,
            Guid shiftId,
            DateTime statisticsDate,
            int plannedProductionCount,
            int actualProductionCount,
            int qualifiedCount,
            int unqualifiedCount,
            int equipmentRunningTime,
            int equipmentFailureTime,
            string remarks = null)
        {
            Id = id;
            ShiftId = shiftId;
            StatisticsDate = statisticsDate;
            PlannedProductionCount = plannedProductionCount;
            ActualProductionCount = actualProductionCount;
            QualifiedCount = qualifiedCount;
            UnqualifiedCount = unqualifiedCount;
            EquipmentRunningTime = equipmentRunningTime;
            EquipmentFailureTime = equipmentFailureTime;
            Remarks = remarks;

            UpdatePerformanceIndicators();
        }

        /// <summary>
        /// 更新绩效指标
        /// </summary>
        private void UpdatePerformanceIndicators()
        {
            // 计算生产任务完成率
            TaskCompletionRate = PlannedProductionCount > 0
                ? (decimal)ActualProductionCount / PlannedProductionCount * 100
                : 0;

            // 计算产品合格率
            QualificationRate = ActualProductionCount > 0
                ? (decimal)QualifiedCount / ActualProductionCount * 100
                : 0;

            // 计算设备利用率
            var totalTime = EquipmentRunningTime + EquipmentFailureTime;
            EquipmentUtilizationRate = totalTime > 0
                ? (decimal)EquipmentRunningTime / totalTime * 100
                : 0;
        }
    }
}