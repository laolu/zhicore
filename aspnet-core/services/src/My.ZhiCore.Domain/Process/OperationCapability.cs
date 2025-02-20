using System;
using Volo.Abp.Domain.Entities;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工序能力评估实体
    /// </summary>
    public class ProcessStepCapability : Entity<Guid>
    {
        /// <summary>
        /// 所属工序Id
        /// </summary>
        public Guid ProcessStepId { get; private set; }

        /// <summary>
        /// 标准工时（分钟）
        /// </summary>
        public decimal StandardTime { get; private set; }

        /// <summary>
        /// 理论小时产能
        /// </summary>
        public int TheoreticalHourlyCapacity { get; private set; }

        /// <summary>
        /// 实际小时产能
        /// </summary>
        public int ActualHourlyCapacity { get; private set; }

        /// <summary>
        /// 设备效率（OEE）
        /// </summary>
        public decimal EquipmentEfficiency { get; private set; }

        /// <summary>
        /// 人员效率
        /// </summary>
        public decimal LaborEfficiency { get; private set; }

        /// <summary>
        /// 瓶颈系数（0-1之间，1表示是瓶颈工序）
        /// </summary>
        public decimal BottleneckFactor { get; private set; }

        /// <summary>
        /// 评估日期
        /// </summary>
        public DateTime EvaluationDate { get; private set; }

        /// <summary>
        /// 评估人
        /// </summary>
        public string Evaluator { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; private set; }

        protected ProcessStepCapability()
        {
        }

        public ProcessStepCapability(
            Guid id,
            Guid processStepId,
            decimal standardTime,
            int theoreticalHourlyCapacity,
            int actualHourlyCapacity,
            decimal equipmentEfficiency,
            decimal laborEfficiency,
            decimal bottleneckFactor,
            DateTime evaluationDate,
            string evaluator,
            string remarks)
        {
            Id = id;
            ProcessStepId = processStepId;
            SetStandardTime(standardTime);
            SetCapacities(theoreticalHourlyCapacity, actualHourlyCapacity);
            SetEfficiencies(equipmentEfficiency, laborEfficiency);
            SetBottleneckFactor(bottleneckFactor);
            SetEvaluationInfo(evaluationDate, evaluator);
            SetRemarks(remarks);
        }

        private void SetStandardTime(decimal standardTime)
        {
            if (standardTime <= 0)
            {
                throw new ArgumentException("标准工时必须大于0", nameof(standardTime));
            }

            StandardTime = standardTime;
        }

        private void SetCapacities(int theoreticalHourlyCapacity, int actualHourlyCapacity)
        {
            if (theoreticalHourlyCapacity <= 0)
            {
                throw new ArgumentException("理论小时产能必须大于0", nameof(theoreticalHourlyCapacity));
            }

            if (actualHourlyCapacity <= 0)
            {
                throw new ArgumentException("实际小时产能必须大于0", nameof(actualHourlyCapacity));
            }

            if (actualHourlyCapacity > theoreticalHourlyCapacity)
            {
                throw new ArgumentException("实际小时产能不能大于理论小时产能");
            }

            TheoreticalHourlyCapacity = theoreticalHourlyCapacity;
            ActualHourlyCapacity = actualHourlyCapacity;
        }

        private void SetEfficiencies(decimal equipmentEfficiency, decimal laborEfficiency)
        {
            if (equipmentEfficiency <= 0 || equipmentEfficiency > 1)
            {
                throw new ArgumentException("设备效率必须在0-1之间", nameof(equipmentEfficiency));
            }

            if (laborEfficiency <= 0 || laborEfficiency > 1)
            {
                throw new ArgumentException("人员效率必须在0-1之间", nameof(laborEfficiency));
            }

            EquipmentEfficiency = equipmentEfficiency;
            LaborEfficiency = laborEfficiency;
        }

        private void SetBottleneckFactor(decimal bottleneckFactor)
        {
            if (bottleneckFactor <= 0 || bottleneckFactor > 1)
            {
                throw new ArgumentException("瓶颈系数必须在0-1之间", nameof(bottleneckFactor));
            }

            BottleneckFactor = bottleneckFactor;
        }

        private void SetEvaluationInfo(DateTime evaluationDate, string evaluator)
        {
            if (evaluationDate > DateTime.Now)
            {
                throw new ArgumentException("评估日期不能大于当前日期", nameof(evaluationDate));
            }

            if (string.IsNullOrWhiteSpace(evaluator))
            {
                throw new ArgumentException("评估人不能为空", nameof(evaluator));
            }

            if (evaluator.Length > 50)
            {
                throw new ArgumentException("评估人长度不能超过50个字符", nameof(evaluator));
            }

            EvaluationDate = evaluationDate;
            Evaluator = evaluator;
        }

        private void SetRemarks(string remarks)
        {
            if (!string.IsNullOrEmpty(remarks) && remarks.Length > 500)
            {
                throw new ArgumentException("备注长度不能超过500个字符", nameof(remarks));
            }

            Remarks = remarks;
        }

        /// <summary>
        /// 更新工序能力评估信息
        /// </summary>
        public void Update(
            decimal standardTime,
            int theoreticalHourlyCapacity,
            int actualHourlyCapacity,
            decimal equipmentEfficiency,
            decimal laborEfficiency,
            decimal bottleneckFactor,
            DateTime evaluationDate,
            string evaluator,
            string remarks)
        {
            SetStandardTime(standardTime);
            SetCapacities(theoreticalHourlyCapacity, actualHourlyCapacity);
            SetEfficiencies(equipmentEfficiency, laborEfficiency);
            SetBottleneckFactor(bottleneckFactor);
            SetEvaluationInfo(evaluationDate, evaluator);
            SetRemarks(remarks);
        }
    }
}