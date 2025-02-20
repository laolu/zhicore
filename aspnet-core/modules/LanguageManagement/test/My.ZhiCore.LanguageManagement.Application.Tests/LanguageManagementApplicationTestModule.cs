namespace My.ZhiCore.LanguageManagement
{
    [DependsOn(
        typeof(LanguageManagementApplicationModule),
        typeof(LanguageManagementDomainTestModule)
        )]
    public class LanguageManagementApplicationTestModule : AbpModule
    {

    }
}
