using System;
using System.Collections.Generic;
using System.Linq;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class BillOfMaterial : AuditableEntity
{
    public int ProductId { get; private set; }
    public string BomCode { get; private set; }
    public string Description { get; private set; }
    public string Version { get; private set; }
    public BomStatus Status { get; private set; }
    public DateTime EffectiveDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public decimal StandardCost { get; private set; }
    public string Notes { get; private set; }
    
    // 添加索引属性，用于优化查询性能
    public string ProductCategory { get; private set; }
    public string MaterialType { get; private set; }
    
    // 缓存相关属性
    public string CacheKey => $"{BomCode}_{Version}";
    public DateTime LastCalculatedAt { get; private set; }
    
    private readonly List<BomComponent> _components = new();
    public IReadOnlyCollection<BomComponent> Components => _components.AsReadOnly();

    private BillOfMaterial() { }

    public static BillOfMaterial Create(
        int productId,
        string bomCode,
        string description,
        string version,
        DateTime effectiveDate,
        string productCategory = null,
        string materialType = null,
        DateTime? expirationDate = null,
        string notes = null)
    {
        if (productId <= 0)
            throw new ArgumentException("产品ID必须大于0", nameof(productId));

        if (string.IsNullOrWhiteSpace(bomCode))
            throw new ArgumentException("BOM编码不能为空", nameof(bomCode));

        if (string.IsNullOrWhiteSpace(version))
            throw new ArgumentException("版本号不能为空", nameof(version));

        return new BillOfMaterial
        {
            ProductId = productId,
            BomCode = bomCode,
            Description = description,
            Version = version,
            Status = BomStatus.Draft,
            EffectiveDate = effectiveDate,
            ExpirationDate = expirationDate,
            ProductCategory = productCategory,
            MaterialType = materialType,
            Notes = notes,
            StandardCost = 0,
            LastCalculatedAt = DateTime.UtcNow
        };
    }

    public void AddComponent(
        int componentId,
        decimal quantity,
        string position = null,
        string notes = null)
    {
        var component = BomComponent.Create(
            componentId,
            quantity,
            position,
            notes);

        _components.Add(component);
        LastCalculatedAt = DateTime.UtcNow;
    }

    // 批量添加组件，减少数据库操作次数
    public void AddComponents(IEnumerable<(int ComponentId, decimal Quantity, string Position, string Notes)> components)
    {
        foreach (var (componentId, quantity, position, notes) in components)
        {
            var component = BomComponent.Create(
                componentId,
                quantity,
                position,
                notes);
            _components.Add(component);
        }
        LastCalculatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(BomStatus newStatus)
    {
        if (newStatus == Status)
            return;

        ValidateStatusTransition(newStatus);
        Status = newStatus;
    }

    // 批量更新组件数量
    public void UpdateComponentQuantities(Dictionary<int, decimal> componentQuantities)
    {
        foreach (var (componentId, newQuantity) in componentQuantities)
        {
            var component = _components.FirstOrDefault(c => c.ComponentId == componentId);
            if (component != null)
            {
                // 假设BomComponent有UpdateQuantity方法
                component.UpdateQuantity(newQuantity);
            }
        }
        LastCalculatedAt = DateTime.UtcNow;
    }

    public void UpdateStandardCost(decimal newCost)
    {
        if (newCost < 0)
            throw new ArgumentException("标准成本不能为负数", nameof(newCost));

        StandardCost = newCost;
        LastCalculatedAt = DateTime.UtcNow;
    }

    private void ValidateStatusTransition(BomStatus newStatus)
    {
        switch (Status)
        {
            case BomStatus.Draft when newStatus != BomStatus.Active:
                throw new InvalidOperationException("草稿状态只能转换为生效状态");
            case BomStatus.Active when newStatus != BomStatus.Inactive:
                throw new InvalidOperationException("生效状态只能转换为失效状态");
            case BomStatus.Inactive:
                throw new InvalidOperationException("失效状态不能转换为其他状态");
        }
    }
}

public enum BomStatus
{
    Draft = 1,    // 草稿
    Active = 2,   // 生效
    Inactive = 3  // 失效
}