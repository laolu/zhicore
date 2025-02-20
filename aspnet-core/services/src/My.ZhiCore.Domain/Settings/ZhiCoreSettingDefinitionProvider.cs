namespace My.ZhiCore.Settings
{
    public class ZhiCoreSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            ConfigEmail(context);
        }

       private static void ConfigEmail(ISettingDefinitionContext context)
        {
            context.GetOrNull(EmailSettingNames.Smtp.Host)
                .WithProperty(ZhiCoreSettings.Group.Default,
                    ZhiCoreSettings.Group.EmailManagement)
                .WithProperty(ZhiCoreSettings.ControlType.Default,
                    ZhiCoreSettings.ControlType.TypeText);

            context.GetOrNull(EmailSettingNames.Smtp.Port)
                .WithProperty(ZhiCoreSettings.Group.Default,
                    ZhiCoreSettings.Group.EmailManagement)
                .WithProperty(ZhiCoreSettings.ControlType.Default,
                    ZhiCoreSettings.ControlType.Number);

            context.GetOrNull(EmailSettingNames.Smtp.UserName)
                .WithProperty(ZhiCoreSettings.Group.Default,
                    ZhiCoreSettings.Group.EmailManagement)
                .WithProperty(ZhiCoreSettings.ControlType.Default,
                    ZhiCoreSettings.ControlType.TypeText);

            context.GetOrNull(EmailSettingNames.Smtp.Password)
                .WithProperty(ZhiCoreSettings.Group.Default,
                    ZhiCoreSettings.Group.EmailManagement)
                .WithProperty(ZhiCoreSettings.ControlType.Default,
                    ZhiCoreSettings.ControlType.TypeText);
            

            context.GetOrNull(EmailSettingNames.Smtp.EnableSsl)
                .WithProperty(ZhiCoreSettings.Group.Default,
                    ZhiCoreSettings.Group.EmailManagement)
                .WithProperty(ZhiCoreSettings.ControlType.Default,
                    ZhiCoreSettings.ControlType.TypeCheckBox);

            context.GetOrNull(EmailSettingNames.Smtp.UseDefaultCredentials)
                .WithProperty(ZhiCoreSettings.Group.Default,
                    ZhiCoreSettings.Group.EmailManagement)
                .WithProperty(ZhiCoreSettings.ControlType.Default,
                    ZhiCoreSettings.ControlType.TypeCheckBox);

            context.GetOrNull(EmailSettingNames.DefaultFromAddress)
                .WithProperty(ZhiCoreSettings.Group.Default,
                    ZhiCoreSettings.Group.EmailManagement)
                .WithProperty(ZhiCoreSettings.ControlType.Default,
                    ZhiCoreSettings.ControlType.TypeText);
            
            context.GetOrNull(EmailSettingNames.DefaultFromDisplayName)
                .WithProperty(ZhiCoreSettings.Group.Default,
                    ZhiCoreSettings.Group.EmailManagement)
                .WithProperty(ZhiCoreSettings.ControlType.Default,
                    ZhiCoreSettings.ControlType.TypeText);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ZhiCoreResource>(name);
        }
    }
}