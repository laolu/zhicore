using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace My.ZhiCore.Material.BOM
{
    /// <summary>
    /// 物料清单项实体，用于描述BOM中的具体物料组成项
    /// </summary>
    public class BomItem : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>所属物料清单ID</summary>
        public Guid BillOfMaterialId { get; private set; }
        /// <summary>物料ID</summary>
        public Guid MaterialId { get; private set; }

        /// <summary>物料数量</summary>
        public decimal Quantity { get; private set; }

        /// <summary>最小允许数量</summary>
        public decimal MinQuantity { get; private set; }

        /// <summary>最大允许数量</summary>
        public decimal? MaxQuantity { get; private set; }

        /// <summary>计量单位</summary>
        public string Unit { get; private set; }

        /// <summary>安装位置</summary>
        public string Position { get; private set; }

        /// <summary>备注说明</summary>
        public string Remark { get; private set; }

        /// <summary>项目序号</summary>
        public int ItemNo { get; private set; }

        /// <summary>是否启用</summary>
        public bool IsActive { get; private set; }

        /// <summary>是否为子装配</summary>
        public bool IsSubAssembly { get; private set; }

        /// <summary>子装配BOM ID（当IsSubAssembly为true时有效）</summary>
        public Guid? SubBomId { get; private set; }

        /// <summary>替代物料ID列表</summary>
        public List<Guid> AlternativeMaterialIds { get; private set; }

        /// <summary>替代物料转换比率</summary>
        public Dictionary<Guid, decimal> ConversionRates { get; private set; }

        /// <summary>分组标识</summary>
        public string GroupCode { get; private set; }

        /// <summary>排序权重</summary>
        public int SortWeight { get; private set; }

        /// <summary>生效日期</summary>
        public DateTime EffectiveDate { get; private set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpirationDate { get; private set; }

        /// <summary>生效日期</summary>
        public DateTime EffectiveDate { get; private set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpirationDate { get; private set; }

        /// <summary>
        /// 保护的构造函数，供ORM使用
        /// </summary>
        protected BomItem() 
        { 
            AlternativeMaterialIds = new List<Guid>();
            ConversionRates = new Dictionary<Guid, decimal>();
        }

        /// <summary>
        /// 创建物料清单项
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <param name="billOfMaterialId">所属物料清单ID</param>
        /// <param name="materialId">物料ID</param>
        /// <param name="quantity">物料数量</param>
        /// <param name="unit">计量单位</param>
        /// <param name="position">安装位置</param>
        /// <param name="remark">备注说明</param>
        /// <param name="itemNo">项目序号</param>
        public BomItem(
            Guid id,
            Guid billOfMaterialId,
            Guid materialId,
            decimal quantity,
            string unit,
            string position,
            string remark,
            int itemNo,
            bool isSubAssembly = false,
            Guid? subBomId = null,
            string groupCode = null,
            int sortWeight = 0,
            DateTime? effectiveDate = null,
            DateTime? expirationDate = null) : base(id)
        {
            BillOfMaterialId = billOfMaterialId;
            MaterialId = materialId;
            Quantity = quantity;
            Unit = unit;
            Position = position;
            Remark = remark;
            ItemNo = itemNo;
            IsActive = true;
            IsSubAssembly = isSubAssembly;
            SubBomId = subBomId;
            GroupCode = groupCode;
            SortWeight = sortWeight;
            EffectiveDate = effectiveDate ?? DateTime.UtcNow;
            ExpirationDate = expirationDate;
            AlternativeMaterialIds = new List<Guid>();
            ConversionRates = new Dictionary<Guid, decimal>();
        }

        /// <summary>
        /// 更新物料数量
        /// </summary>
        /// <param name="newQuantity">新的物料数量（必须大于0）</param>
        /// <exception cref="ArgumentException">当数量小于或等于0时抛出</exception>
        public void UpdateQuantity(decimal newQuantity)
        {
            if (newQuantity < MinQuantity)
                throw new ArgumentException($"Quantity must be greater than or equal to {MinQuantity}.", nameof(newQuantity));

            if (MaxQuantity.HasValue && newQuantity > MaxQuantity.Value)
                throw new ArgumentException($"Quantity must be less than or equal to {MaxQuantity}.", nameof(newQuantity));

            Quantity = newQuantity;
        }

        /// <summary>
        /// 设置数量限制
        /// </summary>
        /// <param name="minQuantity">最小允许数量</param>
        /// <param name="maxQuantity">最大允许数量（可选）</param>
        public void SetQuantityLimits(decimal minQuantity, decimal? maxQuantity = null)
        {
            if (minQuantity <= 0)
                throw new ArgumentException("Minimum quantity must be greater than zero.", nameof(minQuantity));

            if (maxQuantity.HasValue && maxQuantity.Value <= minQuantity)
                throw new ArgumentException("Maximum quantity must be greater than minimum quantity.", nameof(maxQuantity));

            MinQuantity = minQuantity;
            MaxQuantity = maxQuantity;

            // 确保当前数量符合新的限制
            if (Quantity < MinQuantity || (MaxQuantity.HasValue && Quantity > MaxQuantity.Value))
            {
                Quantity = MinQuantity;
            }
        }

        /// <summary>
        /// 更新安装位置
        /// </summary>
        /// <param name="newPosition">新的安装位置</param>
        public void UpdatePosition(string newPosition)
        {
            Position = newPosition;
        }

        /// <summary>
        /// 更新备注说明
        /// </summary>
        /// <param name="newRemark">新的备注说明</param>
        public void UpdateRemark(string newRemark)
        {
            Remark = newRemark;
        }

        /// <summary>
        /// 启用物料清单项
        /// </summary>
        public void Activate()
        {
            IsActive = true;
        }

        /// <summary>
        /// 禁用物料清单项
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
        }

        /// <summary>
        /// 设置为子装配
        /// </summary>
        /// <param name="subBomId">子装配BOM ID</param>
        /// <exception cref="InvalidOperationException">当设置的子装配BOM会导致循环引用时抛出</exception>
        public void SetAsSubAssembly(Guid subBomId)
        {
            if (subBomId == BillOfMaterialId)
                throw new InvalidOperationException("Cannot set self as sub-assembly.");

            IsSubAssembly = true;
            SubBomId = subBomId;
        }

        /// <summary>
        /// 取消子装配设置
        /// </summary>
        public void UnsetSubAssembly()
        {
            IsSubAssembly = false;
            SubBomId = null;
        }

        /// <summary>
        /// 更新计量单位
        /// </summary>
        /// <param name="newUnit">新的计量单位</param>
        public void UpdateUnit(string newUnit)
        {
            if (string.IsNullOrWhiteSpace(newUnit))
                throw new ArgumentException("Unit cannot be empty.", nameof(newUnit));

            // 验证单位的有效性（可以根据实际业务需求扩展）
            if (newUnit.Length > 20)
                throw new ArgumentException("Unit length cannot exceed 20 characters.", nameof(newUnit));

            if (!System.Text.RegularExpressions.Regex.IsMatch(newUnit, @"^[a-zA-Z0-9/\-]+$"))
                throw new ArgumentException("Unit can only contain letters, numbers, forward slashes and hyphens.", nameof(newUnit));

            Unit = newUnit;
        }

        /// <summary>
        /// 添加替代物料
        /// </summary>
        /// <param name="alternativeMaterialId">替代物料ID</param>
        /// <param name="conversionRate">转换比率（1单位主物料=多少单位替代物料）</param>
        public void AddAlternativeMaterial(Guid alternativeMaterialId, decimal conversionRate)
        {
            if (alternativeMaterialId == MaterialId)
                throw new InvalidOperationException("Cannot add main material as alternative material.");

            if (conversionRate <= 0)
                throw new ArgumentException("Conversion rate must be greater than zero.", nameof(conversionRate));

            // 检查是否已经存在循环替代关系
            if (IsCircularSubstitution(alternativeMaterialId))
                throw new InvalidOperationException("Adding this alternative material would create a circular substitution.");

            // 检查替代物料数量是否超过限制
            if (AlternativeMaterialIds.Count >= 10)
                throw new InvalidOperationException("Cannot add more than 10 alternative materials.");

            if (!AlternativeMaterialIds.Contains(alternativeMaterialId))
            {
                AlternativeMaterialIds.Add(alternativeMaterialId);
                ConversionRates[alternativeMaterialId] = conversionRate;
            }
        }

        /// <summary>
        /// 检查是否存在循环替代关系
        /// </summary>
        /// <param name="newAlternativeMaterialId">待添加的替代物料ID</param>
        /// <returns>是否存在循环替代</returns>
        private bool IsCircularSubstitution(Guid newAlternativeMaterialId)
        {
            // 这里需要通过领域服务来实现完整的循环检测
            // 当前仅做简单的直接循环检测
            return AlternativeMaterialIds.Contains(newAlternativeMaterialId);
        }

        /// <summary>
        /// 移除替代物料
        /// </summary>
        /// <param name="alternativeMaterialId">替代物料ID</param>
        public void RemoveAlternativeMaterial(Guid alternativeMaterialId)
        {
            if (AlternativeMaterialIds.Remove(alternativeMaterialId))
            {
                ConversionRates.Remove(alternativeMaterialId);
            }
        }

        /// <summary>
        /// 更新分组信息
        /// </summary>
        /// <param name="groupCode">分组标识</param>
        /// <param name="sortWeight">排序权重</param>
        public void UpdateGrouping(string groupCode, int sortWeight)
        {
            GroupCode = groupCode;
            SortWeight = sortWeight;
        }

        /// <summary>
        /// 更新生效期
        /// </summary>
        /// <param name="effectiveDate">生效日期</param>
        /// <param name="expirationDate">失效日期</param>
        public void UpdateEffectivePeriod(DateTime effectiveDate, DateTime? expirationDate)
        {
            if (expirationDate.HasValue && effectiveDate >= expirationDate.Value)
                throw new ArgumentException("Effective date must be earlier than expiration date.");

            EffectiveDate = effectiveDate;
            ExpirationDate = expirationDate;
        }

        /// <summary>
        /// 检查BOM项是否在指定日期有效
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>是否有效</returns>
        public bool IsEffectiveAt(DateTime date)
        {
            return IsActive && 
                   date >= EffectiveDate && 
                   (!ExpirationDate.HasValue || date <= ExpirationDate.Value);
        }

        /// <summary>
        /// 更新生效期
        /// </summary>
        /// <param name="effectiveDate">生效日期</param>
        /// <param name="expirationDate">失效日期</param>
        public void UpdateEffectivePeriod(DateTime effectiveDate, DateTime? expirationDate)
        {
            if (expirationDate.HasValue && effectiveDate >= expirationDate.Value)
                throw new ArgumentException("Effective date must be earlier than expiration date.");

            EffectiveDate = effectiveDate;
            ExpirationDate = expirationDate;
        }

        /// <summary>
        /// 检查BOM项是否在指定日期有效
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>是否有效</returns>
        public bool IsEffectiveAt(DateTime date)
        {
            return IsActive && 
                   date >= EffectiveDate && 
                   (!ExpirationDate.HasValue || date <= ExpirationDate.Value);
        }
    }
}