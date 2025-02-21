using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.EventBus.Local;
using My.ZhiCore.Equipment.Events;

namespace My.ZhiCore.Equipment
{
    /// <summary>
    /// 设备实体类，用于管理和跟踪设备的基本信息、维护计划和运行状态
    /// </summary>
    /// <remarks>
    /// 该类包含设备的以下主要功能：
    /// - 基本信息管理（名称、编码、型号等）
    /// - 维护计划管理（维护间隔、下次维护日期等）
    /// - 运行状态管理（待机、运行、维护、故障等）
    /// - 预警管理（基于维护计划的预警）
    /// </remarks>
    public class Equipment : FullAuditedAggregateRoot<Guid>
    {
        protected ILocalEventBus LocalEventBus => LazyServiceProvider.LazyGetRequiredService<ILocalEventBus>();

    {
        /// <summary>设备名称</summary>
        public string Name { get; private set; }
        /// <summary>设备编码，用于唯一标识设备</summary>
        public string Code { get; private set; }
        /// <summary>设备型号</summary>
        public string Model { get; private set; }
        /// <summary>制造商名称</summary>
        public string Manufacturer { get; private set; }
        /// <summary>购买日期</summary>
        public DateTime PurchaseDate { get; private set; }
        /// <summary>上次维护日期</summary>
        public DateTime? LastMaintenanceDate { get; private set; }
        /// <summary>下次维护日期，由系统根据维护间隔自动计算</summary>
        public DateTime? NextMaintenanceDate { get; private set; }
        /// <summary>设备所在位置</summary>
        public string Location { get; private set; }
        /// <summary>设备描述</summary>
        public string Description { get; private set; }
        /// <summary>维护间隔值</summary>
        public decimal MaintenanceInterval { get; private set; }
        /// <summary>维护间隔单位（day/week/month/year）</summary>
        public string MaintenanceUnit { get; private set; }
        /// <summary>维护预警阈值，用于提前提醒维护</summary>
        public decimal WarningThreshold { get; private set; }
        /// <summary>设备当前状态</summary>
        public EquipmentStatus Status { get; private set; }
        /// <summary>设备分类ID</summary>
        public Guid CategoryId { get; private set; }
        /// <summary>工作中心ID</summary>
        public Guid? WorkCenterId { get; private set; }
        /// <summary>是否处于预警状态</summary>
        public bool IsWarning { get; private set; }

        /// <summary>采购成本</summary>
        public decimal PurchaseCost { get; private set; }
        /// <summary>累计维护成本</summary>
        public decimal TotalMaintenanceCost { get; private set; }
        /// <summary>年折旧率</summary>
        public decimal DepreciationRate { get; private set; }
        /// <summary>当前设备净值</summary>
        public decimal CurrentValue { get; private set; }

        /// <summary>设备利用率</summary>
        public decimal UtilizationRate { get; private set; }
        /// <summary>设备综合效率(OEE)</summary>
        public decimal OverallEquipmentEffectiveness { get; private set; }
        /// <summary>累计运行时间（小时）</summary>
        public decimal TotalRunningHours { get; private set; }
        /// <summary>累计停机时间（小时）</summary>
        public decimal TotalDowntimeHours { get; private set; }

        /// <summary>关联的备件清单ID列表</summary>
        public string SparePartIds { get; private set; }
        /// <summary>设备说明书URL</summary>
        public string ManualUrl { get; private set; }
        /// <summary>维护手册URL</summary>
        public string MaintenanceManualUrl { get; private set; }
        /// <summary>操作指南URL</summary>
        public string OperationGuideUrl { get; private set; }

        /// <summary>受保护的构造函数，用于ORM</summary>
        protected Equipment()
        {
        }

        /// <summary>
        /// 创建新的设备实例
        /// </summary>
        /// <param name="id">设备ID</param>
        /// <param name="name">设备名称</param>
        /// <param name="code">设备编码</param>
        /// <param name="model">设备型号</param>
        /// <param name="manufacturer">制造商</param>
        /// <param name="purchaseDate">购买日期</param>
        /// <param name="location">设备位置</param>
        /// <param name="description">设备描述</param>
        /// <param name="maintenanceInterval">维护间隔值</param>
        /// <param name="maintenanceUnit">维护间隔单位（day/week/month/year）</param>
        /// <param name="warningThreshold">维护预警阈值</param>
        /// <param name="categoryId">设备分类ID</param>
        public Equipment(
            Guid id,
            string name,
            string code,
            string model,
            string manufacturer,
            DateTime purchaseDate,
            string location,
            string description,
            decimal maintenanceInterval,
            string maintenanceUnit,
            decimal warningThreshold,
            Guid categoryId,
            Guid? workCenterId = null) : base(id)
        {
            Name = name;
            Code = code;
            Model = model;
            Manufacturer = manufacturer;
            PurchaseDate = purchaseDate;
            Location = location;
            Description = description;
            MaintenanceInterval = maintenanceInterval;
            MaintenanceUnit = maintenanceUnit;
            WarningThreshold = warningThreshold;
            CategoryId = categoryId;
            WorkCenterId = workCenterId;
            var oldStatus = Status;
            var oldStatus = Status;
            Status = EquipmentStatus.Standby;
            
            // 发布维护完成事件
            LocalEventBus.Publish(new EquipmentMaintenanceCompletedEventData(
                Id, Code, Name, maintenanceDate, NextMaintenanceDate, TotalMaintenanceCost));
            
            // 发布状态变更事件
            LocalEventBus.Publish(new EquipmentStatusChangedEventData(
                Id, Code, Name, oldStatus, Status, "维护完成"));
            
            // 发布状态变更事件
            LocalEventBus.Publish(new EquipmentStatusChangedEventData(
                Id, Code, Name, oldStatus, Status, "设备停止运行"));
            IsWarning = false;

            // 初始化成本相关字段
            PurchaseCost = 0;
            TotalMaintenanceCost = 0;
            DepreciationRate = 0;
            CurrentValue = 0;

            // 初始化效率相关字段
            UtilizationRate = 0;
            OverallEquipmentEffectiveness = 0;
            TotalRunningHours = 0;
            TotalDowntimeHours = 0;

            // 初始化备件和文档相关字段
            SparePartIds = "";
            ManualUrl = "";
            MaintenanceManualUrl = "";
            OperationGuideUrl = "";
        }

        /// <summary>
        /// 开始设备运行
        /// </summary>
        /// <exception cref="InvalidOperationException">当设备不在待机状态时抛出</exception>
        /// <remarks>
        /// 此方法将设备状态从待机切换为运行状态。
        /// 只有处于待机状态的设备才能开始运行。
        /// </remarks>
        public void StartOperation()
        {
            if (Status != EquipmentStatus.Standby)
                throw new InvalidOperationException("Equipment must be in standby status to start operation.");

            var oldStatus = Status;
            Status = EquipmentStatus.Running;
            
            // 发布状态变更事件
            LocalEventBus.Publish(new EquipmentStatusChangedEventData(
                Id, Code, Name, oldStatus, Status, "设备开始运行"));
        }

        /// <summary>
        /// 停止设备运行
        /// </summary>
        /// <exception cref="InvalidOperationException">当设备不在运行状态时抛出</exception>
        /// <remarks>
        /// 此方法将设备状态从运行切换为待机状态。
        /// 只有处于运行状态的设备才能被停止。
        /// </remarks>
        public void StopOperation()
        {
            if (Status != EquipmentStatus.Running)
                throw new InvalidOperationException("Equipment must be in running status to stop operation.");

            var oldStatus = Status;
            var oldStatus = Status;
            Status = EquipmentStatus.Standby;
            
            // 发布维护完成事件
            LocalEventBus.Publish(new EquipmentMaintenanceCompletedEventData(
                Id, Code, Name, maintenanceDate, NextMaintenanceDate, TotalMaintenanceCost));
            
            // 发布状态变更事件
            LocalEventBus.Publish(new EquipmentStatusChangedEventData(
                Id, Code, Name, oldStatus, Status, "维护完成"));
            
            // 发布状态变更事件
            LocalEventBus.Publish(new EquipmentStatusChangedEventData(
                Id, Code, Name, oldStatus, Status, "设备停止运行"));
        }

        /// <summary>
        /// 开始设备维护
        /// </summary>
        /// <exception cref="InvalidOperationException">当设备处于运行状态时抛出</exception>
        /// <remarks>
        /// 此方法将设备状态切换为维护状态。
        /// 设备不能在运行状态下进行维护。
        /// </remarks>
        public void StartMaintenance()
        {
            if (Status == EquipmentStatus.Running)
                throw new InvalidOperationException("Cannot start maintenance while equipment is running.");

            var oldStatus = Status;
            Status = EquipmentStatus.Maintenance;
            
            // 发布状态变更事件
            LocalEventBus.Publish(new EquipmentStatusChangedEventData(
                Id, Code, Name, oldStatus, Status, "开始设备维护"));
        }

        /// <summary>
        /// 记录设备故障
        /// </summary>
        /// <param name="faultDescription">故障描述</param>
        /// <exception cref="ArgumentException">当故障描述为空时抛出</exception>
        /// <remarks>
        /// 此方法记录设备故障并将设备状态切换为故障状态。
        /// </remarks>
        public void RecordFault(string faultDescription)
        {
            if (string.IsNullOrWhiteSpace(faultDescription))
                throw new ArgumentException("Fault description cannot be empty.", nameof(faultDescription));

            var oldStatus = Status;
            Status = EquipmentStatus.Fault;
            
            // 发布状态变更事件
            LocalEventBus.Publish(new EquipmentStatusChangedEventData(
                Id, Code, Name, oldStatus, Status, faultDescription));
        }

        /// <summary>
        /// 更新设备维护计划
        /// </summary>
        /// <param name="maintenanceInterval">维护间隔值</param>
        /// <param name="maintenanceUnit">维护间隔单位（day/week/month/year）</param>
        /// <param name="warningThreshold">维护预警阈值</param>
        /// <exception cref="ArgumentException">当参数值无效时抛出</exception>
        /// <remarks>
        /// 此方法更新设备的维护计划参数。
        /// - 维护间隔必须大于零
        /// - 维护单位不能为空
        /// - 预警阈值必须大于零
        /// </remarks>
        public void UpdateMaintenanceSchedule(
            decimal maintenanceInterval,
            string maintenanceUnit,
            decimal warningThreshold)
        {
            if (maintenanceInterval <= 0)
                throw new ArgumentException("Maintenance interval must be greater than zero.", nameof(maintenanceInterval));

            if (string.IsNullOrWhiteSpace(maintenanceUnit))
                throw new ArgumentException("Maintenance unit cannot be empty.", nameof(maintenanceUnit));

            if (warningThreshold <= 0)
                throw new ArgumentException("Warning threshold must be greater than zero.", nameof(warningThreshold));

            MaintenanceInterval = maintenanceInterval;
            MaintenanceUnit = maintenanceUnit;
            WarningThreshold = warningThreshold;
        }

        /// <summary>
        /// 完成设备维护
        /// </summary>
        /// <param name="maintenanceDate">维护完成日期</param>
        /// <exception cref="InvalidOperationException">当设备处于故障状态时抛出</exception>
        /// <exception cref="ArgumentException">当维护日期在未来时抛出</exception>
        /// <remarks>
        /// 此方法记录维护完成信息，并：
        /// - 更新上次维护日期
        /// - 计算下次维护日期
        /// - 将设备状态切换为待机
        /// </remarks>
        public void CompleteMaintenance(DateTime maintenanceDate)
        {
            if (Status == EquipmentStatus.Fault)
                throw new InvalidOperationException("Cannot complete maintenance while equipment is in fault status.");

            if (maintenanceDate.Date > DateTime.Now.Date)
                throw new ArgumentException("Maintenance date cannot be in the future.", nameof(maintenanceDate));

            LastMaintenanceDate = maintenanceDate;
            CalculateNextMaintenanceDate();

            var oldStatus = Status;
            var oldStatus = Status;
            Status = EquipmentStatus.Standby;
            
            // 发布维护完成事件
            LocalEventBus.Publish(new EquipmentMaintenanceCompletedEventData(
                Id, Code, Name, maintenanceDate, NextMaintenanceDate, TotalMaintenanceCost));
            
            // 发布状态变更事件
            LocalEventBus.Publish(new EquipmentStatusChangedEventData(
                Id, Code, Name, oldStatus, Status, "维护完成"));
            
            // 发布状态变更事件
            LocalEventBus.Publish(new EquipmentStatusChangedEventData(
                Id, Code, Name, oldStatus, Status, "设备停止运行"));
        }

        /// <summary>
        /// 计算下次维护日期
        /// </summary>
        /// <remarks>
        /// 根据维护间隔和单位计算下次维护日期。
        /// 支持的维护单位：day（天）、week（周）、month（月）、year（年）
        /// </remarks>
        private void CalculateNextMaintenanceDate()
        {
            if (!LastMaintenanceDate.HasValue)
                return;

            NextMaintenanceDate = MaintenanceUnit.ToLower() switch
            {
                "day" => LastMaintenanceDate.Value.AddDays((double)MaintenanceInterval),
                "week" => LastMaintenanceDate.Value.AddDays((double)MaintenanceInterval * 7),
                "month" => LastMaintenanceDate.Value.AddMonths((int)MaintenanceInterval),
                "year" => LastMaintenanceDate.Value.AddYears((int)MaintenanceInterval),
                _ => throw new InvalidOperationException($"Unsupported maintenance unit: {MaintenanceUnit}")
            };

            CheckMaintenanceWarning();
        }

        /// <summary>
        /// 检查是否需要发出维护预警
        /// </summary>
        /// <remarks>
        /// 根据下次维护日期和预警阈值计算是否需要发出预警。
        /// 预警阈值会根据维护单位自动转换为天数进行比较。
        /// </remarks>
        private void CheckMaintenanceWarning()
        {
            if (!NextMaintenanceDate.HasValue)
                return;

            var daysUntilMaintenance = (NextMaintenanceDate.Value - DateTime.Now).TotalDays;
            var warningDays = MaintenanceUnit.ToLower() switch
            {
                "day" => WarningThreshold,
                "week" => WarningThreshold * 7,
                "month" => WarningThreshold * 30,
                "year" => WarningThreshold * 365,
                _ => throw new InvalidOperationException($"Unsupported maintenance unit: {MaintenanceUnit}")
            };

            IsWarning = daysUntilMaintenance <= warningDays;
        }

        /// <summary>
        /// 更新设备成本信息
        /// </summary>
        /// <param name="maintenanceCost">本次维护成本</param>
        /// <param name="depreciationRate">年折旧率</param>
        /// <exception cref="ArgumentException">当参数值无效时抛出</exception>
        public void UpdateCostInformation(decimal maintenanceCost, decimal depreciationRate)
        {
            if (maintenanceCost < 0)
                throw new ArgumentException("Maintenance cost cannot be negative.", nameof(maintenanceCost));

            if (depreciationRate < 0 || depreciationRate > 1)
                throw new ArgumentException("Depreciation rate must be between 0 and 1.", nameof(depreciationRate));

            TotalMaintenanceCost += maintenanceCost;
            DepreciationRate = depreciationRate;
            
            // 计算当前设备净值
            var yearsFromPurchase = (DateTime.Now - PurchaseDate).TotalDays / 365.0;
            CurrentValue = PurchaseCost * (decimal)(Math.Pow(1 - (double)DepreciationRate, yearsFromPurchase));
        }

        /// <summary>
        /// 更新设备效率指标
        /// </summary>
        /// <param name="runningHours">运行时间（小时）</param>
        /// <param name="downtimeHours">停机时间（小时）</param>
        /// <param name="oee">设备综合效率</param>
        /// <exception cref="ArgumentException">当参数值无效时抛出</exception>
        public void UpdateEfficiencyMetrics(decimal runningHours, decimal downtimeHours, decimal oee)
        {
            if (runningHours < 0)
                throw new ArgumentException("Running hours cannot be negative.", nameof(runningHours));

            if (downtimeHours < 0)
                throw new ArgumentException("Downtime hours cannot be negative.", nameof(downtimeHours));

            if (oee < 0 || oee > 1)
                throw new ArgumentException("OEE must be between 0 and 1.", nameof(oee));

            TotalRunningHours += runningHours;
            TotalDowntimeHours += downtimeHours;
            OverallEquipmentEffectiveness = oee;

            // 计算设备利用率
            var totalTime = TotalRunningHours + TotalDowntimeHours;
            UtilizationRate = totalTime > 0 ? TotalRunningHours / totalTime : 0;
        }

        /// <summary>
        /// 更新设备文档信息
        /// </summary>
        /// <param name="manualUrl">说明书URL</param>
        /// <param name="maintenanceManualUrl">维护手册URL</param>
        /// <param name="operationGuideUrl">操作指南URL</param>
        public void UpdateDocumentUrls(
            string manualUrl,
            string maintenanceManualUrl,
            string operationGuideUrl)
        {
            ManualUrl = manualUrl ?? "";
            MaintenanceManualUrl = maintenanceManualUrl ?? "";
            OperationGuideUrl = operationGuideUrl ?? "";
        }

        /// <summary>
        /// 更新备件清单
        /// </summary>
        /// <param name="sparePartIds">备件ID列表，以逗号分隔</param>
        public void UpdateSpareParts(string sparePartIds)
        {
            SparePartIds = sparePartIds ?? "";
        }
    }
}