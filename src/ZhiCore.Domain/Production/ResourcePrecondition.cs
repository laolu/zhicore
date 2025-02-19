using System.Collections.Generic;

namespace ZhiCore.Domain.Production;

public class ResourcePrecondition
{
    public string EquipmentStatus { get; private set; }  // 设备状态要求
    public string OperatorQualification { get; private set; }  // 操作人员资质要求
    public string EnvironmentCondition { get; private set; }  // 环境条件要求
    public List<int> DependentResourceIds { get; private set; }  // 依赖资源ID列表
    
    private ResourcePrecondition() 
    {
        DependentResourceIds = new List<int>();
    }
    
    public static ResourcePrecondition Create(
        string equipmentStatus = null,
        string operatorQualification = null,
        string environmentCondition = null,
        List<int> dependentResourceIds = null)
    {
        var precondition = new ResourcePrecondition
        {
            EquipmentStatus = equipmentStatus,
            OperatorQualification = operatorQualification,
            EnvironmentCondition = environmentCondition
        };
        
        if (dependentResourceIds != null)
        {
            precondition.DependentResourceIds.AddRange(dependentResourceIds);
        }
        
        return precondition;
    }
    
    public void UpdateEquipmentStatus(string status)
    {
        EquipmentStatus = status;
    }
    
    public void UpdateOperatorQualification(string qualification)
    {
        OperatorQualification = qualification;
    }
    
    public void UpdateEnvironmentCondition(string condition)
    {
        EnvironmentCondition = condition;
    }
    
    public void AddDependentResource(int resourceId)
    {
        if (!DependentResourceIds.Contains(resourceId))
        {
            DependentResourceIds.Add(resourceId);
        }
    }
    
    public void RemoveDependentResource(int resourceId)
    {
        DependentResourceIds.Remove(resourceId);
    }
}