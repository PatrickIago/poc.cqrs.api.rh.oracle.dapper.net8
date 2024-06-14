using MongoDB.Driver;
using poc.core.api.net8.Abstractions;
using poc.core.api.net8.Interface;

namespace Poc.DistributedCache.MongoDb;

public class MongoDbCacheService : IMongoDbCacheService
{
    private readonly IMongoCollection<IEntityMongoDb> _collection;

    public MongoDbCacheService(IMongoDatabase database)
    {
        _collection = database.GetCollection<IEntityMongoDb>(typeof(IEntityMongoDb).Name);
    }

    public async Task ConfigureTtlIndexForExpiresAtAsync()
    {
        var indexOptions = new CreateIndexOptions { ExpireAfter = TimeSpan.FromSeconds(0) }; // Documentos expiram quando 'ExpiresAt' é atingido
        var indexDefinition = Builders<IEntityMongoDb>.IndexKeys.Ascending(x => x.ExpiresAt);
        var indexModel = new CreateIndexModel<IEntityMongoDb>(indexDefinition, indexOptions);
        await _collection.Indexes.CreateOneAsync(indexModel);
    }

    public async Task<IEntityMongoDb> CreateAsync(IEntityMongoDb entity, TimeSpan? ttl = null)
    {
        if (ttl.HasValue)
        {
            // Define o campo 'ExpiresAt' com base no TTL fornecido
            entity.ExpiresAt = DateTime.UtcNow.Add(ttl.Value);
        }

        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<IEntityMongoDb> CreateAsync(IEntityMongoDb entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<IEntityMongoDb> UpdateAsync(IEntityMongoDb entity)
    {
        var filter = Builders<IEntityMongoDb>.Filter.Eq(p => p.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter, entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var filter = Builders<IEntityMongoDb>.Filter.Eq(p => p.Id, id);
        var deleteResult = await _collection.DeleteOneAsync(filter);
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount == 1;
    }

    public async Task<IEntityMongoDb> GetByIdAsync(string id)
    {
        var filter = Builders<IEntityMongoDb>.Filter.Eq(p => p.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<IEntityMongoDb>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
}



