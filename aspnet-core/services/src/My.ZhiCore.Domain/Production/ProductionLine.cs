using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Production
{
    /// <summary>
    /// 生产线实体 - 用于管理工厂中的生产线资源
    /// </summary>
    /// <remarks>
    /// 该实体包含生产线的基本信息、当前状态和运行情况，支持以下核心业务功能：
    /// - 生产线的基本信息管理（编号、名称、描述等）
    /// - 生产线状态管理（空闲、运行中、维护中）
    /// - 生产任务的分配和执行
    /// - 生产线维护管理
    /// </remarks>
    public class ProductionLine : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 生产线编号
        /// </summary>
        /// <remarks>用于唯一标识生产线的业务编号，通常按照工厂编码规则生成</remarks>
        public string LineNumber { get; private set; }

        /// <summary>
        /// 生产线名称
        /// </summary>
        /// <remarks>生产线的显示名称，用于在系统中展示和识别</remarks>
        public string Name { get; private set; }

        /// <summary>
        /// 生产线描述
        /// </summary>
        /// <remarks>可选的描述信息，用于记录生产线的详细信息，如位置、用途等</remarks>
        public string Description { get; private set; }

        /// <summary>
        /// 生产线状态
        /// </summary>
        /// <remarks>
        /// 表示生产线当前的运行状态：
        /// - Idle：空闲状态，可以分配新的生产任务
        /// - Running：运行中，正在执行生产任务
        /// - Maintenance：维护中，不能执行生产任务
        /// </remarks>
        public ProductionLineStatus Status { get; private set; }

        /// <summary>
        /// 当前生产的工单ID
        /// </summary>
        /// <remarks>记录当前正在生产的工单ID，仅在Running状态下有值</remarks>
        public Guid? CurrentWorkOrderId { get; private set; }

        protected ProductionLine() { }

        /// <summary>
        /// 创建新的生产线
        /// </summary>
        /// <param name="id">生产线唯一标识</param>
        /// <param name="lineNumber">生产线编号</param>
        /// <param name="name">生产线名称</param>
        /// <param name="description">生产线描述（可选）</param>
        /// <remarks>新创建的生产线默认处于空闲状态</remarks>
        public ProductionLine(
            Guid id,
            string lineNumber,
            string name,
            string description = null)
        {
            Id = id;
            LineNumber = lineNumber;
            Name = name;
            Description = description;
            Status = ProductionLineStatus.Idle;
        }

        /// <summary>
        /// 开始生产
        /// </summary>
        /// <param name="workOrderId">要生产的工单ID</param>
        /// <remarks>
        /// 将生产线状态切换为运行中，并记录当前生产的工单ID
        /// 仅当生产线处于空闲状态时才能开始生产
        /// </remarks>
        /// <exception cref="InvalidOperationException">当生产线不处于空闲状态时抛出</exception>
        public void StartProduction(Guid workOrderId)
        {
            if (Status != ProductionLineStatus.Idle)
                throw new InvalidOperationException("只有空闲状态的生产线才能开始生产");

            Status = ProductionLineStatus.Running;
            CurrentWorkOrderId = workOrderId;
        }

        /// <summary>
        /// 停止生产
        /// </summary>
        /// <remarks>
        /// 将生产线状态切换为空闲，并清除当前工单ID
        /// 仅当生产线处于运行状态时才能停止生产
        /// </remarks>
        /// <exception cref="InvalidOperationException">当生产线不处于运行状态时抛出</exception>
        public void StopProduction()
        {
            if (Status != ProductionLineStatus.Running)
                throw new InvalidOperationException("只有运行中的生产线才能停止生产");

            Status = ProductionLineStatus.Idle;
            CurrentWorkOrderId = null;
        }

        /// <summary>
        /// 进入维护状态
        /// </summary>
        /// <remarks>
        /// 将生产线状态切换为维护中
        /// 运行中的生产线不能直接进入维护状态，需要先停止生产
        /// </remarks>
        /// <exception cref="InvalidOperationException">当生产线处于运行状态时抛出</exception>
        public void Maintain()
        {
            if (Status == ProductionLineStatus.Running)
                throw new InvalidOperationException("运行中的生产线不能进入维护状态");

            Status = ProductionLineStatus.Maintenance;
            CurrentWorkOrderId = null;
        }

        /// <summary>
        /// 完成维护
        /// </summary>
        /// <remarks>
        /// 将生产线状态从维护中切换为空闲状态
        /// 仅当生产线处于维护状态时才能完成维护
        /// </remarks>
        /// <exception cref="InvalidOperationException">当生产线不处于维护状态时抛出</exception>
        public void CompleteMaintenance()
        {
            if (Status != ProductionLineStatus.Maintenance)
                throw new InvalidOperationException("只有维护状态的生产线才能完成维护");

            Status = ProductionLineStatus.Idle;
        }
    }
}