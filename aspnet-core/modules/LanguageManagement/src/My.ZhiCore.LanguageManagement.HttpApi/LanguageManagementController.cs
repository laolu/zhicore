using My.ZhiCore.LanguageManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace My.ZhiCore.LanguageManagement
{
    public abstract class LanguageManagementController : AbpController
    {
        protected LanguageManagementController()
        {
            LocalizationResource = typeof(LanguageManagementResource);
        }
    }
}
