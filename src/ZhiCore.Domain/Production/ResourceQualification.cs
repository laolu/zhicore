using System;
using ZhiCore.Domain.Common;

namespace ZhiCore.Domain.Production;

public class ResourceQualification : AuditableEntity
{
    public int ProcessResourceId { get; private set; }
    public string RequiredCertifications { get; private set; }    // 所需证书（JSON格式）
    public int MinSkillLevel { get; private set; }              // 最低技能等级
    public int MinExperienceYears { get; private set; }         // 最低工作年限
    public string SpecialRequirements { get; private set; }     // 特殊要求（JSON格式）
    
    private ResourceQualification() { }
    
    public static ResourceQualification Create(
        int processResourceId,
        string requiredCertifications,
        int minSkillLevel,
        int minExperienceYears,
        string specialRequirements = null)
    {
        if (processResourceId <= 0)
            throw new ArgumentException("工序资源ID必须大于0", nameof(processResourceId));
            
        if (minSkillLevel < 0)
            throw new ArgumentException("最低技能等级不能为负数", nameof(minSkillLevel));
            
        if (minExperienceYears < 0)
            throw new ArgumentException("最低工作年限不能为负数", nameof(minExperienceYears));
            
        return new ResourceQualification
        {
            ProcessResourceId = processResourceId,
            RequiredCertifications = requiredCertifications,
            MinSkillLevel = minSkillLevel,
            MinExperienceYears = minExperienceYears,
            SpecialRequirements = specialRequirements
        };
    }
    
    public void UpdateRequiredCertifications(string certifications)
    {
        RequiredCertifications = certifications;
    }
    
    public void UpdateSkillLevel(int minSkillLevel)
    {
        if (minSkillLevel < 0)
            throw new ArgumentException("最低技能等级不能为负数", nameof(minSkillLevel));
            
        MinSkillLevel = minSkillLevel;
    }
    
    public void UpdateExperienceYears(int minExperienceYears)
    {
        if (minExperienceYears < 0)
            throw new ArgumentException("最低工作年限不能为负数", nameof(minExperienceYears));
            
        MinExperienceYears = minExperienceYears;
    }
    
    public void UpdateSpecialRequirements(string requirements)
    {
        SpecialRequirements = requirements;
    }
    
    public bool MeetsQualification(
        string certifications,
        int skillLevel,
        int experienceYears)
    {
        // 这里需要实现证书的具体匹配逻辑
        bool certificationsMatch = true; // 临时返回true，实际需要解析JSON并比对
        
        return certificationsMatch
            && skillLevel >= MinSkillLevel
            && experienceYears >= MinExperienceYears;
    }
}