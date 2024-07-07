using AutoMapper;
using Dapper;
using Poc.Contract.Command.Departament.Interfaces;
using Poc.Contract.Query.Departament.ViewModels;
using Poc.Domain.Entities.Departament;
using Poc.Domain.Entities.Region;
using Poc.Oracle.Context;
using Poc.Oracle.SQL;
using System.Data;

namespace Poc.Oracle.CommandStore;

public class DepartmentWriteOnlyRepository : IDepartmentWriteOnlyRepository
{
    private readonly OracleDbContext _dbContext;
    private readonly IMapper _mapper;

    public DepartmentWriteOnlyRepository(OracleDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<DepartmentQueryModel> Create(DepartmentEntity departament)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var departamentId = await dbConnection.QueryFirstOrDefaultAsync<decimal>(DepartamentSqlConsts.SQL_MAX);

        var parameters = new DynamicParameters();
        parameters.Add("PR_DEPARTMENT_ID", departamentId, DbType.Decimal);
        parameters.Add("PR_DEPARTMENT_NAME", departament.Name, DbType.String);
        parameters.Add("PR_MANAGER_ID", departament.ManagerId, DbType.Decimal);
        parameters.Add("PR_LOCATION_ID", departament.Location, DbType.Decimal);

        await dbConnection.ExecuteAsync(DepartamentSqlConsts.SQL_INSERT, parameters);
        departament.SetDepartamentId(departamentId);
        return _mapper.Map<DepartmentQueryModel>(departament);
    }
    public async Task<bool> Delete(decimal id)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("PR_DEPARTMENT_ID", id, DbType.Decimal);

        var affectedRows = await dbConnection.ExecuteAsync(DepartamentSqlConsts.SQL_DELETE, parameters);

        return affectedRows > 0; // Retorna verdadeiro se a operação deletou uma linha
    }

    public async Task<DepartmentEntity> Get(decimal id)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var result = await dbConnection.QueryFirstOrDefaultAsync<DepartmentEntity>(
            DepartamentSqlConsts.SQL_GET_BY_ID,
            new { PR_DEPARTMENT_ID = id }
        );
        return result;
    }

    public async Task<bool> Update(DepartmentEntity entity)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("PR_DEPARTMENT_NAME", entity.Name, DbType.String);
        parameters.Add("PR_MANAGER_ID", entity.ManagerId, DbType.Int32);
        parameters.Add("PR_LOCATION_ID", entity.Location, DbType.Int32);
        parameters.Add("PR_DEPARTMENT_ID", entity.Id, DbType.Int32);

        var affectedRows = await dbConnection.ExecuteAsync(DepartamentSqlConsts.SQL_UPDATE, parameters);

        return affectedRows > 0;
    }
}

