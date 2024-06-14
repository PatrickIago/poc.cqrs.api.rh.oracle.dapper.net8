//using MongoDB.Driver;
//using Poc.DistributedCache.Poc.Core.API.Configuration;

//namespace Poc.DistributedCache.Poc.Core.API.MongoDb.EventCore;

//public class EventCoreCommandStore : IEventCoreCommandStore
//{
//    private readonly IMongoCollection<EventCoreModel> _model;
//    public EventCoreCommandStore(MongoDatabaseFactory dbFactory)
//        => _model = dbFactory.GetDatabase().GetCollection<EventCoreModel>("EventCore");
//    public async Task Create(IEnumerable<EventCoreModel> entities)
//        => await _model.InsertManyAsync(entities);
//}
