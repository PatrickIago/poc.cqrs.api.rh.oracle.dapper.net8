using AutoMapper;
using Dapper;
using Poc.Contract.Query.Region.Interfaces;
using Poc.Contract.Query.Region.ViewModels;
using Poc.Domain.Entities.Region;
using Poc.Oracle.Context;
using Poc.Oracle.SQL;
using System.Data;

namespace Poc.Oracle.QueryStore;

public class RegionsReadOnlyRepository : IRegionsReadOnlyRepository
{
    private readonly OracleDbContext _dbContext;
    private readonly IMapper _mapper;

    public RegionsReadOnlyRepository(OracleDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<RegionQueryModel> Get(decimal id)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var result = await dbConnection.QueryFirstOrDefaultAsync<RegionEntity>(
            RegionSqlConsts.SQL_GET_BY_ID,
            new { PR_REGION_ID = id }
        );

        var mapper = _mapper.Map<RegionQueryModel>(result);
        return mapper;
    }

    public async Task<List<RegionQueryModel>> Get()
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();
        var result = await dbConnection.QueryAsync<RegionEntity>(RegionSqlConsts.SQL_GET);
        var mapper = _mapper.Map<List<RegionQueryModel>>(result);
        return mapper.AsList();
    }
}
