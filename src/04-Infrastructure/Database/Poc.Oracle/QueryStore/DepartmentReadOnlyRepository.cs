using AutoMapper;
using Dapper;
using Poc.Contract.Query.Departament.Interfaces;
using Poc.Contract.Query.Departament.ViewModels;
using Poc.Domain.Entities.Departament;
using Poc.Oracle.Context;
using Poc.Oracle.SQL;
using System.Data;

namespace Poc.Oracle.QueryStore;

public class DepartmentReadOnlyRepository : IDepartmentReadOnlyRepository
{
    private readonly OracleDbContext _dbContext;
    private readonly IMapper _mapper;
    public DepartmentReadOnlyRepository(OracleDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<DepartmentQueryModel>> Get()
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var result = await dbConnection.QueryAsync<DepartmentEntity>(DepartamentSqlConsts.SQL_GET);
        var mapper = _mapper.Map<List<DepartmentQueryModel>>(result);
        return mapper.AsList();
    }

    public async Task<DepartmentQueryModel> Get(decimal id)
    {
        // Cria uma conexão com o banco de dados.
        using IDbConnection dbConnection = _dbContext.CreateConnection();

        // Abre a conexão com o banco de dados.
        dbConnection.Open();

        var result = await dbConnection.QueryFirstOrDefaultAsync<DepartmentEntity>(
            DepartamentSqlConsts.SQL_GET_BY_ID, new { PR_DEPARTMENT_ID = id });

        // Verifica se o resultado é nulo.
        if (result == null)
        {
            return null;
        }

        var mapper = _mapper.Map<DepartmentQueryModel>(result);

        // Retorna o modelo mapeado.
        return mapper;
    }
}

