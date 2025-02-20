using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Production.Shift
{
    /// <summary>
    /// 班次实体 - 用于管理生产班次和人员排班
    /// </summary>
    public class Shift : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 班次编号
        /// </summary>
        public string ShiftNumber { get; private set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeSpan StartTime { get; private set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public TimeSpan EndTime { get; private set; }

        /// <summary>
        /// 生产线ID
        /// </summary>
        public Guid ProductionLineId { get; private set; }

        /// <summary>
        /// 班次状态
        /// </summary>
        public ShiftStatus Status { get; private set; }

        /// <summary>
        /// 是否为班次模板
        /// </summary>
        public bool IsTemplate { get; private set; }

        /// <summary>
        /// 出勤率
        /// </summary>
        public decimal AttendanceRate { get; private set; }

        /// <summary>
        /// 生产效率
        /// </summary>
        public decimal ProductionEfficiency { get; private set; }

        /// <summary>
        /// 班次人员列表
        /// </summary>
        private readonly List<ShiftPersonnel> _personnel;
        public IReadOnlyList<ShiftPersonnel> Personnel => _personnel.AsReadOnly();

        /// <summary>
        /// 交接班记录列表
        /// </summary>
        private readonly List<ShiftHandover> _handovers;
        public IReadOnlyList<ShiftHandover> Handovers => _handovers.AsReadOnly();

        /// <summary>
        /// 异常记录列表
        /// </summary>
        private readonly List<ShiftException> _exceptions;
        public IReadOnlyList<ShiftException> Exceptions => _exceptions.AsReadOnly();

        protected Shift()
        {
            _personnel = new List<ShiftPersonnel>();
            _handovers = new List<ShiftHandover>();
            _exceptions = new List<ShiftException>();
            AttendanceRate = 0;
            ProductionEfficiency = 0;
            IsTemplate = false;
        }

        public Shift(
            Guid id,
            string shiftNumber,
            string name,
            TimeSpan startTime,
            TimeSpan endTime,
            Guid productionLineId)
        {
            Id = id;
            ShiftNumber = shiftNumber;
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            ProductionLineId = productionLineId;
            Status = ShiftStatus.Planned;
            _personnel = new List<ShiftPersonnel>();
            _handovers = new List<ShiftHandover>();
            _exceptions = new List<ShiftException>();
            AttendanceRate = 0;
            ProductionEfficiency = 0;
            IsTemplate = false;
        }

        /// <summary>
        /// 创建班次模板
        /// </summary>
        public static Shift CreateTemplate(
            Guid id,
            string shiftNumber,
            string name,
            TimeSpan startTime,
            TimeSpan endTime,
            Guid productionLineId)
        {
            var template = new Shift(id, shiftNumber, name, startTime, endTime, productionLineId);
            template.IsTemplate = true;
            return template;
        }

        /// <summary>
        /// 从模板创建班次
        /// </summary>
        public Shift CreateFromTemplate(Guid newId, string newShiftNumber)
        {
            if (!IsTemplate)
                throw new InvalidOperationException("只有班次模板才能创建新班次");

            return new Shift(newId, newShiftNumber, Name, StartTime, EndTime, ProductionLineId);
        }

        /// <summary>
        /// 添加班次人员
        /// </summary>
        public void AddPersonnel(ShiftPersonnel personnel)
        {
            if (Status != ShiftStatus.Planned)
                throw new InvalidOperationException("只有计划状态的班次才能添加人员");

            _personnel.Add(personnel);
        }

        /// <summary>
        /// 移除班次人员
        /// </summary>
        public void RemovePersonnel(ShiftPersonnel personnel)
        {
            if (Status != ShiftStatus.Planned)
                throw new InvalidOperationException("只有计划状态的班次才能移除人员");

            _personnel.Remove(personnel);
        }

        /// <summary>
        /// 开始班次
        /// </summary>
        public void Start()
        {
            if (Status != ShiftStatus.Planned)
                throw new InvalidOperationException("只有计划状态的班次才能开始");

            if (_personnel.Count == 0)
                throw new InvalidOperationException("班次必须包含至少一名人员");

            Status = ShiftStatus.InProgress;
        }

        /// <summary>
        /// 结束班次
        /// </summary>
        public void End(ShiftHandover handover)
        {
            if (Status != ShiftStatus.InProgress)
                throw new InvalidOperationException("只有进行中的班次才能结束");

            _handovers.Add(handover);
            UpdateStatistics();
            Status = ShiftStatus.Completed;
        }

        /// <summary>
        /// 记录班次异常
        /// </summary>
        public void RecordException(ShiftException exception)
        {
            if (Status != ShiftStatus.InProgress)
                throw new InvalidOperationException("只有进行中的班次才能记录异常");

            _exceptions.Add(exception);
        }

        /// <summary>
        /// 更新统计指标
        /// </summary>
        private void UpdateStatistics()
        {
            // 计算出勤率
            var totalScheduled = _personnel.Count;
            var actualAttendance = _personnel.Count(p => p.IsPresent);
            AttendanceRate = totalScheduled > 0 ? (decimal)actualAttendance / totalScheduled * 100 : 0;

            // 计算生产效率（示例：基于完成的生产任务数与计划数的比率）
            // 实际项目中可能需要更复杂的计算逻辑
            var completedTasks = _handovers.Sum(h => h.CompletedTaskCount);
            var plannedTasks = _handovers.Sum(h => h.PlannedTaskCount);
            ProductionEfficiency = plannedTasks > 0 ? (decimal)completedTasks / plannedTasks * 100 : 0;
        }

        /// <summary>
        /// 取消班次
        /// </summary>
        public void Cancel()
        {
            if (Status == ShiftStatus.Completed)
                throw new InvalidOperationException("已完成的班次不能取消");

            Status = ShiftStatus.Cancelled;
        }
    }
}