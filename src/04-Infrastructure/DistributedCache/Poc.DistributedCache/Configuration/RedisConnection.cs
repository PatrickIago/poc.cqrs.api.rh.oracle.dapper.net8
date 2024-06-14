using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Poc.DistributedCache.Configuration;

public class RedisConnection
{
    private static ConnectionMultiplexer _redisConnection;
    private static readonly object padlock = new object();

    public RedisConnection(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CacheConnection");
        lock (padlock)
        {
            if (_redisConnection == null || !_redisConnection.IsConnected)
            {
                _redisConnection = ConnectionMultiplexer.Connect(connectionString);
            }
        }
    }

    public IDatabase GetDatabase()
    {
        return _redisConnection.GetDatabase();
    }

    // Modo de uso
    //var userService = new RedisCacheService<User>();

    //// Adicionar um usuário no cache
    //await userService.SetAsync("user:1", new User { Id = 1, Name = "João" });

    //// Recuperar um usuário do cache
    //var user = await userService.GetAsync("user:1");

    //// Excluir um usuário do cache
    //await userService.DeleteAsync("user:1");
}
