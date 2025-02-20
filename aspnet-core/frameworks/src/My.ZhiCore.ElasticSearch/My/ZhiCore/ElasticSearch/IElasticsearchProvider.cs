namespace My.ZhiCore.ElasticSearch;

public interface IElasticsearchProvider
{
    IElasticClient GetClient();
}