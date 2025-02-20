using My.ZhiCore.BasicManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace My.ZhiCore.BasicManagement;

public abstract class BasicManagementController : AbpControllerBase
{
    protected BasicManagementController()
    {
        LocalizationResource = typeof(BasicManagementResource);
    }
}
