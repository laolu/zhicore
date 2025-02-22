using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Process
{
    /// <summary>
    /// 工艺路线步骤类型
    /// </summary>
    public enum RouteStepType
    {
        /// <summary>
        /// 串行执行
        /// </summary>
        Serial = 0,

        /// <summary>
        /// 并行执行
        /// </summary>
        Parallel = 1
    }

    /// <summary>
    /// 工艺路线步骤状态
    /// </summary>
    public enum RouteStepStatus
    {
        /// <summary>
        /// 未开始
        /// </summary>
        NotStarted = 0,

        /// <summary>
        /// 等待前置步骤完成
        /// </summary>
        WaitingForPredecessors = 1,

        /// <summary>
        /// 就绪
        /// </summary>
        Ready = 2,

        /// <summary>
        /// 执行中
        /// </summary>
        InProgress = 3,

        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 4,

        /// <summary>
        /// 已跳过
        /// </summary>
        Skipped = 5,

        /// <summary>
        /// 失败
        /// </summary>
        Failed = 6
    }

    /// <summary>
    /// 工艺路线步骤
    /// </summary>
    public class RouteStep : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 步骤序号
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 步骤描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 步骤类型
        /// </summary>
        public RouteStepType StepType { get; set; }

        /// <summary>
        /// 步骤状态
        /// </summary>
        public RouteStepStatus Status { get; set; }

        /// <summary>
        /// 所属工艺路线版本
        /// </summary>
        public virtual RouteVersion RouteVersion { get; set; }
        public Guid RouteVersionId { get; set; }

        /// <summary>
        /// 关联工序
        /// </summary>
        public virtual Operation Operation { get; set; }
        public Guid OperationId { get; set; }

        /// <summary>
        /// 前置步骤列表
        /// </summary>
        public virtual ICollection<RouteStep> PredecessorSteps { get; private set; }

        /// <summary>
        /// 后续步骤列表
        /// </summary>
        public virtual ICollection<RouteStep> SuccessorSteps { get; private set; }

        protected RouteStep()
        {
            PredecessorSteps = new List<RouteStep>();
            SuccessorSteps = new List<RouteStep>();
            Status = RouteStepStatus.NotStarted;
            StepType = RouteStepType.Serial;
        }

        public RouteStep(Guid id, Guid routeVersionId, Guid operationId, int sequence, string name, RouteStepType stepType = RouteStepType.Serial, string description = null)
            : base(id)
        {
            RouteVersionId = routeVersionId;
            OperationId = operationId;
            Sequence = sequence;
            Name = name;
            Description = description;
            StepType = stepType;
            Status = RouteStepStatus.NotStarted;
            PredecessorSteps = new List<RouteStep>();
            SuccessorSteps = new List<RouteStep>();
        }
    }
}