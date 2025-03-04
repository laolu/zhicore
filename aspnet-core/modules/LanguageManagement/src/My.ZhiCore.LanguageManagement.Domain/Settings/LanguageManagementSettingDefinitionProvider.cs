namespace My.ZhiCore.LanguageManagement.Settings
{
    public class LanguageManagementSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            var languageManagementSettings = context.GetOrNull(LocalizationSettingNames.DefaultLanguage)
                .WithProperty(LanguageManagementSettings.Group.Default, LanguageManagementSettings.Group.SystemManagement)
                .WithProperty(ZhiCoreSettingConsts.ControlType.Default, ZhiCoreSettingConsts.ControlType.TypeText);
            languageManagementSettings.DefaultValue = "zh-Hans";
        }
    }
}