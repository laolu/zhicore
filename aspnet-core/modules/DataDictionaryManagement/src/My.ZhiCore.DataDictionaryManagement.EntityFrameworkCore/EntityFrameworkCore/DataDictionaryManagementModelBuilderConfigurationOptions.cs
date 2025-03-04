namespace My.ZhiCore.DataDictionaryManagement.EntityFrameworkCore
{
    public class DataDictionaryManagementModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public DataDictionaryManagementModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}