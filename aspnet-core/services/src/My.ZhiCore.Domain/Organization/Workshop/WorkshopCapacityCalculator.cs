using System;

namespace My.ZhiCore.Organization.Workshop
{
    /// <summary>
    /// 车间产能计算器
    /// </summary>
    public class WorkshopCapacityCalculator
    {
        private readonly Workshop _workshop;

        public WorkshopCapacityCalculator(Workshop workshop)
        {
            _workshop = workshop ?? throw new ArgumentNullException(nameof(workshop));
        }

        /// <summary>
        /// 计算实际产能（考虑工作时间和效率因素）
        /// </summary>
        /// <param name="workingHours">实际工作小时数</param>
        /// <param name="efficiency">工作效率（0-1之间）</param>
        /// <returns>实际产能（件/小时）</returns>
        public int CalculateActualCapacity(double workingHours, double efficiency)
        {
            if (workingHours <= 0)
            {
                throw new ArgumentException("工作时间必须大于0", nameof(workingHours));
            }

            if (efficiency <= 0 || efficiency > 1)
            {
                throw new ArgumentException("工作效率必须在0-1之间", nameof(efficiency));
            }

            return (int)(_workshop.StandardCapacity * efficiency);
        }

        /// <summary>
        /// 计算产能利用率
        /// </summary>
        /// <param name="actualOutput">实际产出数量</param>
        /// <param name="timeSpan">统计时间段（小时）</param>
        /// <returns>产能利用率（0-1之间）</returns>
        public double CalculateCapacityUtilization(int actualOutput, double timeSpan)
        {
            if (actualOutput < 0)
            {
                throw new ArgumentException("实际产出不能为负数", nameof(actualOutput));
            }

            if (timeSpan <= 0)
            {
                throw new ArgumentException("统计时间必须大于0", nameof(timeSpan));
            }

            double theoreticalOutput = _workshop.StandardCapacity * timeSpan;
            return Math.Min(1.0, actualOutput / theoreticalOutput);
        }

        /// <summary>
        /// 计算剩余产能
        /// </summary>
        /// <param name="currentLoad">当前负载（件/小时）</param>
        /// <returns>剩余产能（件/小时）</returns>
        public int CalculateRemainingCapacity(int currentLoad)
        {
            if (currentLoad < 0)
            {
                throw new ArgumentException("当前负载不能为负数", nameof(currentLoad));
            }

            return Math.Max(0, _workshop.MaxCapacity - currentLoad);
        }

        /// <summary>
        /// 评估是否可以接受新的生产任务
        /// </summary>
        /// <param name="requiredCapacity">所需产能（件/小时）</param>
        /// <param name="currentLoad">当前负载（件/小时）</param>
        /// <returns>是否可以接受新任务</returns>
        public bool CanAcceptNewTask(int requiredCapacity, int currentLoad)
        {
            if (requiredCapacity <= 0)
            {
                throw new ArgumentException("所需产能必须大于0", nameof(requiredCapacity));
            }

            if (currentLoad < 0)
            {
                throw new ArgumentException("当前负载不能为负数", nameof(currentLoad));
            }

            return CalculateRemainingCapacity(currentLoad) >= requiredCapacity;
        }
    }
}