using StackExchange.Redis;

namespace Products.RedisCacheService
{
    public class ProductCacheService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _dbContext;
        public ProductCacheService(string connectionString)
        {
            var options = ConfigurationOptions.Parse(connectionString);
            options.AbortOnConnectFail = false;
            _redis = ConnectionMultiplexer.Connect(options);
            _dbContext = _redis.GetDatabase();

            /*_redis = ConnectionMultiplexer.Connect(connectionString);
            _dbContext = _redis.GetDatabase();*/
        }
        public void SaveProductCache(string key, string value)
        {
            _dbContext.StringSet(key, value);
        }

        public string GetProductCache(string key)
        {
            return _dbContext.StringGet(key);
            
        }

        public void DeleteProductCache(string key)
        {
            _dbContext.KeyDelete(key);
        }
    }
}
