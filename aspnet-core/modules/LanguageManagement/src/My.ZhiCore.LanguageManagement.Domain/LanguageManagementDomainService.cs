using IObjectMapper = Volo.Abp.ObjectMapping.IObjectMapper;

namespace My.ZhiCore.LanguageManagement
{
    public abstract class LanguageManagementDomainService : DomainService
    {
        protected Type ObjectMapperContext { get; set; }

        /// <summary>
        /// 工作单元管理器
        /// </summary>
        protected IUnitOfWorkManager UnitOfWorkManager =>
            LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>();

        /// <summary>
        /// 分布式事件总线
        /// </summary>
        protected IDistributedEventBus DistributedEventBus =>
            LazyServiceProvider.LazyGetRequiredService<IDistributedEventBus>();

        /// <summary>
        /// 对象映射器
        /// </summary>
        protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetService<IObjectMapper>(
            provider =>
                ObjectMapperContext == null
                    ? provider.GetRequiredService<IObjectMapper>()
                    : (IObjectMapper)provider.GetRequiredService(
                        typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext)));
    }
}