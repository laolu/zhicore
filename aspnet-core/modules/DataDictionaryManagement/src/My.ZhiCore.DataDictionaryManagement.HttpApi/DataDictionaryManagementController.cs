using My.ZhiCore.DataDictionaryManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace My.ZhiCore.DataDictionaryManagement
{
    public abstract class DataDictionaryManagementController : AbpController
    {
        protected DataDictionaryManagementController()
        {
            LocalizationResource = typeof(DataDictionaryManagementResource);
        }
    }
}
