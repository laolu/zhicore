using Volo.Abp.Features;
using Volo.Abp.Validation.StringValues;

namespace My.ZhiCore.Features;

public class ZhiCoreFeatureDefinitionProvider : FeatureDefinitionProvider
{
    public override void Define(IFeatureDefinitionContext context)
    {
       
        var group = context.AddGroup(ZhiCoreFeatures.GroupName,L("Feature:TestGroup"));
        
        // ToggleStringValueType bool类型 前端渲染为checkbox
        group.AddFeature(ZhiCoreFeatures.TestEnable,
            "true",
            L("Feature:TestEnable"),
            L("Feature:TestEnable"),
            new ToggleStringValueType());
        
        // ToggleStringValueType string类型 前端渲染为input
        group.AddFeature(ZhiCoreFeatures.TestString,
            "输入需要设定的值",
            L("Feature:TestString"),
            L("Feature:TestString"),
            new FreeTextStringValueType());
        
        // todo SelectionStringValueType select标签待定
    }
    
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ZhiCoreResource>(name);
    }
}