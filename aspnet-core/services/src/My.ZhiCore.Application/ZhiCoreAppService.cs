namespace My.ZhiCore
{
    /* Inherit your application services from this class.
     */
    public abstract class ZhiCoreAppService : ApplicationService
    {
        protected ZhiCoreAppService()
        {
            LocalizationResource = typeof(ZhiCoreResource);
        }
    }
}
