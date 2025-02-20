namespace My.ZhiCore.DataDictionaryManagement
{
    public abstract class DataDictionaryManagementAppService : ApplicationService
    {
        protected DataDictionaryManagementAppService()
        {
            LocalizationResource = typeof(DataDictionaryManagementResource);
            ObjectMapperContext = typeof(DataDictionaryManagementApplicationModule);
        }
    }
}
