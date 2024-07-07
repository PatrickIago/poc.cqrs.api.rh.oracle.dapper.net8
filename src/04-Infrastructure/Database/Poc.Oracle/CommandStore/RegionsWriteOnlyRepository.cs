using AutoMapper;
using Dapper;
using Poc.Contract.Command.Region.Interfaces;
using Poc.Contract.Query.Region.ViewModels;
using Poc.Domain.Entities.Region;
using Poc.Oracle.Context;
using Poc.Oracle.SQL;
using System.Data;

namespace Poc.Oracle.CommandStore;

public class RegionsWriteOnlyRepository : IRegionWriteOnlyRepository
{
    private readonly OracleDbContext _dbContext;
    private readonly IMapper _mapper;

    public RegionsWriteOnlyRepository(OracleDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<RegionQueryModel> Create(RegionEntity region)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        // Primeiro, execute a consulta para obter o próximo REGION_ID.
        var regionId = await dbConnection.QueryFirstOrDefaultAsync<decimal>(
            RegionSqlConsts.SQL_MAX
        );

        var parameters = new DynamicParameters();
        parameters.Add("PR_REGION_ID", regionId, DbType.Decimal);
        parameters.Add("PR_REGION_NAME", region.RegionName, DbType.String);

        // Agora, insira na base de dados com os parâmetros definidos.
        await dbConnection.ExecuteAsync(RegionSqlConsts.SQL_INSERT, parameters);

        // Atribua o ID obtido para a entidade region.
        region.SetRegionId(regionId);

        // Mapeie o resultado para RegionQueryModel e retorne.
        return _mapper.Map<RegionQueryModel>(region);
    }

    public async Task<bool> Update(RegionEntity region)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("PR_REGION_ID", region.RegionId, DbType.Int32);
        parameters.Add("PR_REGION_NAME", region.RegionName, DbType.String);

        var affectedRows = await dbConnection.ExecuteAsync(RegionSqlConsts.SQL_UPDATE, parameters);

        return affectedRows > 0; // Retorna verdadeiro se a operação atualizou uma linha
    }

    public async Task<bool> Delete(decimal regionId)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("PR_REGION_ID", regionId, DbType.Int32);

        var affectedRows = await dbConnection.ExecuteAsync(RegionSqlConsts.SQL_DELETE, parameters);

        return affectedRows > 0; // Retorna verdadeiro se a operação deletou uma linha
    }

    public async Task<RegionEntity> Get(decimal id)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var result = await dbConnection.QueryFirstOrDefaultAsync<RegionEntity>(
            RegionSqlConsts.SQL_GET_BY_ID,
            new { PR_REGION_ID = id }
        );
        return result;
    }


}