using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using poc.core.api.net8.Interface;
using Poc.DistributedCache.Configuration;
using Poc.DistributedCache.MongoDb;
using Poc.DistributedCache.Redis;
using StackExchange.Redis;

namespace Poc.DistributedCache;

public class DistributedCacheInitializer
{
    public static void Initialize(IServiceCollection services, IConfiguration configuration)
    {
        //services.ConfigureAppSettings();
        //services.AddDistributedMemoryCache();
        var settings = configuration.GetSection("MongoDB").Get<MongoDbSettings>();
        services.AddSingleton(settings);
        services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(settings.ConnectionString));
        services.AddScoped(sp => new MongoDatabaseFactory(sp.GetRequiredService<IMongoClient>(), settings.Database));
        services.AddScoped(sp => sp.GetRequiredService<MongoDatabaseFactory>().GetDatabase());



        // Auth
        //services.AddScoped<ICacheService, DistributedCacheService>();
        services.AddSingleton<IConfiguration>(provider => configuration);
        services.AddSingleton<RedisConnection>();

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetConnectionString("CacheConnection")));
        services.AddScoped(typeof(IRedisCacheService<>), typeof(RedisCacheService<>));


        // Generica
        services.AddTransient<IMongoDbCacheService, MongoDbCacheService>();
        services.AddTransient<IDomainEventDistributedCacheService, DomainEventDistributedCacheService>();
        //services.AddTransient<IEventCoreCommandStore, EventCoreCommandStore>();
        //services.AddTransient<IEventMKTCommandStore, EventMKTCommandStore>();

        //services.AddTransient<IAuthTokenCommandStore, AuthTokenCommandStore>();
        //services.AddTransient<IAuthResetCommandStore, AuthResetCommandStore>();
    }

}
