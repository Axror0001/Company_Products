using Microsoft.AspNetCore.Connections;
using StackExchange.Redis;

namespace Products.RedisCacheService
{
    public class CompanyCacheService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _dbContext;
        public CompanyCacheService(string connectionString)
        {
            var options = ConfigurationOptions.Parse(connectionString);
            options.AbortOnConnectFail = false;
            _redis = ConnectionMultiplexer.Connect(options);
            _dbContext = _redis.GetDatabase();

            /*_redis = ConnectionMultiplexer.Connect(connectionString);
            _dbContext = _redis.GetDatabase();*/
        }

        public void SaveCompanyCache(string key, string value)
        {
            _dbContext.StringSet(key, value);
        }

        public string GetCompanyCache(string key)
        {
            return _dbContext.StringGet(key);

        }

        public void DeleteCompanyCache(string key)
        {
            _dbContext.KeyDelete(key);
        }
    }
}
