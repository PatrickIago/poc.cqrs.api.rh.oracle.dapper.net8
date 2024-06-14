//using MongoDB.Driver;
//using Poc.DistributedCache.Poc.Core.API.Configuration;

//namespace Poc.DistributedCache.Poc.Core.API.MongoDb.Auth;

//public class AuthResetCommandStore : IAuthResetCommandStore
//{
//    private readonly IMongoCollection<AuthResetModel> _authToken;

//    public AuthResetCommandStore(MongoDatabaseFactory dbFactory)
//    {
//        _authToken = dbFactory.GetDatabase().GetCollection<AuthResetModel>("AuthResetPassword");
//    }

//    public async Task<AuthResetModel> GetAuthByToken(string token)
//    {
//        var filter = Builders<AuthResetModel>.Filter.Eq(p => p.Token, token);
//        return await _authToken.Find(filter).FirstOrDefaultAsync();
//    }

//    public async Task<AuthResetModel> GetAuthById(string authId)
//    {
//        var filter = Builders<AuthResetModel>.Filter.Eq(p => p.Id, authId);
//        return await _authToken.Find(filter).FirstOrDefaultAsync();
//    }

//    public async Task<AuthResetModel> Create(AuthResetModel entity)
//    {
//        var auth = await GetAuthById(entity.Id);
//        if (auth is not null)
//            await Delete(entity.Id);

//        await _authToken.InsertOneAsync(entity);
//        return entity;
//    }

//    public async Task<AuthResetModel> Delete(string id)
//    {
//        var filter = Builders<AuthResetModel>.Filter.Eq(p => p.Id, id);
//        var result = await _authToken.DeleteOneAsync(filter);

//        if (result.IsAcknowledged && result.DeletedCount > 0)
//            return null;

//        return null;
//    }
//}
