using MongoDB.Driver;
using poc.core.api.net8.Interface;
using poc.core.api.net8.Model;
using Poc.DistributedCache.Configuration;

namespace Poc.DistributedCache.MongoDb;

public class DomainEventDistributedCacheService : IDomainEventDistributedCacheService
{
    private readonly IMongoCollection<DomainEventDistributedCache> _domainEventDistributedCache;

    public DomainEventDistributedCacheService(MongoDatabaseFactory dbFactory)
    {
        _domainEventDistributedCache = dbFactory.GetDatabase().GetCollection<DomainEventDistributedCache>("DomainEventDistributedCache");
    }

    public async Task<DomainEventDistributedCache> Get(string id)
    {
        var filter = Builders<DomainEventDistributedCache>.Filter.Eq(p => p.Id, id);
        return await _domainEventDistributedCache.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<DomainEventDistributedCache>> Get()
        => await _domainEventDistributedCache.Find(_ => true).ToListAsync();

    public async Task<DomainEventDistributedCache> Create(DomainEventDistributedCache entity)
    {
        await _domainEventDistributedCache.InsertOneAsync(entity);
        return entity;
    }

    public async Task<DomainEventDistributedCache> Update(DomainEventDistributedCache entity)
    {
        var filter = Builders<DomainEventDistributedCache>.Filter.Eq(p => p.Id, entity.Id);
        var updateResult = await _domainEventDistributedCache.ReplaceOneAsync(filter, entity);

        if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            return entity;

        return null;
    }

    public async Task<DomainEventDistributedCache> Delete(string id)
    {
        var filter = Builders<DomainEventDistributedCache>.Filter.Eq(p => p.Id, id);
        var result = await _domainEventDistributedCache.DeleteOneAsync(filter);

        if (result.IsAcknowledged && result.DeletedCount > 0)
            return null;

        return null;
    }


}
