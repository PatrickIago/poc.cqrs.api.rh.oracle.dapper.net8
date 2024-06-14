//using MongoDB.Driver;
//using Poc.DistributedCache.Poc.Core.API.Configuration;

//namespace Poc.DistributedCache.Poc.Core.API.MongoDb.Auth;

//public class AuthTokenCommandStore : IAuthTokenCommandStore
//{
//    private readonly IMongoCollection<AuthTokenModel> _authToken;

//    public AuthTokenCommandStore(MongoDatabaseFactory dbFactory)
//    {
//        _authToken = dbFactory.GetDatabase().GetCollection<AuthTokenModel>("AuthToken");
//    }

//    public async Task<AuthTokenModel> GetAuthByToken(string token)
//    {
//        var filter = Builders<AuthTokenModel>.Filter.Eq(p => p.Token, token);
//        return await _authToken.Find(filter).FirstOrDefaultAsync();
//    }

//    public async Task<AuthTokenModel> GetAuthById(string authId)
//    {
//        var filter = Builders<AuthTokenModel>.Filter.Eq(p => p.Id, authId);
//        return await _authToken.Find(filter).FirstOrDefaultAsync();
//    }

//    public async Task<AuthTokenModel> Create(AuthTokenModel entity)
//    {
//        var auth = await GetAuthById(entity.Id);
//        if (auth is not null)
//            await Delete(entity.Id);

//        await _authToken.InsertOneAsync(entity);
//        return entity;
//    }

//    public async Task<AuthTokenModel> Delete(string id)
//    {
//        var filter = Builders<AuthTokenModel>.Filter.Eq(p => p.Id, id);
//        var result = await _authToken.DeleteOneAsync(filter);

//        if (result.IsAcknowledged && result.DeletedCount > 0)
//            return null;

//        return null;
//    }
//}
