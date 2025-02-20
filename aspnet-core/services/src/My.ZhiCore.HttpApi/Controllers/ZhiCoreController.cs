namespace My.ZhiCore.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class ZhiCoreController : AbpController
    {
        protected ZhiCoreController()
        {
            LocalizationResource = typeof(ZhiCoreResource);
        }
    }
}