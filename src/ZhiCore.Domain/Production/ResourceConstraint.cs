using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ResourceConstraint : AuditableEntity
{
    public int ProcessResourceId { get; private set; }
    public decimal MinTemperature { get; private set; }    // 最低温度要求
    public decimal MaxTemperature { get; private set; }    // 最高温度要求
    public decimal MinHumidity { get; private set; }      // 最低湿度要求
    public decimal MaxHumidity { get; private set; }      // 最高湿度要求
    public decimal MinPressure { get; private set; }      // 最低压力要求
    public decimal MaxPressure { get; private set; }      // 最高压力要求
    public string CustomParameters { get; private set; }   // 其他自定义参数（JSON格式）
    
    private ResourceConstraint() { }
    
    public static ResourceConstraint Create(
        int processResourceId,
        decimal minTemperature,
        decimal maxTemperature,
        decimal minHumidity,
        decimal maxHumidity,
        decimal minPressure,
        decimal maxPressure,
        string customParameters = null)
    {
        if (processResourceId <= 0)
            throw new ArgumentException("工序资源ID必须大于0", nameof(processResourceId));
            
        if (minTemperature >= maxTemperature)
            throw new ArgumentException("最高温度必须大于最低温度");
            
        if (minHumidity >= maxHumidity)
            throw new ArgumentException("最高湿度必须大于最低湿度");
            
        if (minPressure >= maxPressure)
            throw new ArgumentException("最高压力必须大于最低压力");
            
        return new ResourceConstraint
        {
            ProcessResourceId = processResourceId,
            MinTemperature = minTemperature,
            MaxTemperature = maxTemperature,
            MinHumidity = minHumidity,
            MaxHumidity = maxHumidity,
            MinPressure = minPressure,
            MaxPressure = maxPressure,
            CustomParameters = customParameters
        };
    }
    
    public void UpdateTemperatureRange(decimal minTemperature, decimal maxTemperature)
    {
        if (minTemperature >= maxTemperature)
            throw new ArgumentException("最高温度必须大于最低温度");
            
        MinTemperature = minTemperature;
        MaxTemperature = maxTemperature;
    }
    
    public void UpdateHumidityRange(decimal minHumidity, decimal maxHumidity)
    {
        if (minHumidity >= maxHumidity)
            throw new ArgumentException("最高湿度必须大于最低湿度");
            
        MinHumidity = minHumidity;
        MaxHumidity = maxHumidity;
    }
    
    public void UpdatePressureRange(decimal minPressure, decimal maxPressure)
    {
        if (minPressure >= maxPressure)
            throw new ArgumentException("最高压力必须大于最低压力");
            
        MinPressure = minPressure;
        MaxPressure = maxPressure;
    }
    
    public void UpdateCustomParameters(string customParameters)
    {
        CustomParameters = customParameters;
    }
    
    public bool IsEnvironmentSuitable(
        decimal temperature,
        decimal humidity,
        decimal pressure)
    {
        return temperature >= MinTemperature && temperature <= MaxTemperature
            && humidity >= MinHumidity && humidity <= MaxHumidity
            && pressure >= MinPressure && pressure <= MaxPressure;
    }
}