using AutoMapper;
using Dapper;
using Poc.Contract.Query.Employee.Interfaces;
using Poc.Contract.Query.Employee.ViewModels;
using Poc.Domain.Entities.Employee;
using Poc.Oracle.Context;
using Poc.Oracle.SQL;
using System.Data;

namespace Poc.Oracle.QueryStore;
public class EmployeeReadOnlyRepository : IEmployeeReadOnlyRepository
{
    
    private readonly OracleDbContext _dbContext;
    private readonly IMapper _mapper;

    public EmployeeReadOnlyRepository(OracleDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    // Método assíncrono para obter uma lista de EmployeeQueryModel.
    public async Task<List<EmployeeQueryModel>> Get()
    {
        // Cria uma conexão com o banco de dados.
        using IDbConnection dbConnection = _dbContext.CreateConnection();

        // Abre a conexão com o banco de dados.
        dbConnection.Open();

        // Executa a consulta SQL para obter uma lista de EmployeeEntity.
        var result = await dbConnection.QueryAsync<EmployeeEntity>(EmployeeSqlConsts.SQL_GET);

        // Mapeia a lista de EmployeeEntity para uma lista de EmployeeQueryModel.
        var mapper = _mapper.Map<List<EmployeeQueryModel>>(result);

        // Retorna a lista mapeada.
        return mapper.AsList();
    }

    // Método assíncrono para obter um EmployeeQueryModel por ID.
    public async Task<EmployeeQueryModel> Get(int id)
    {
        // Cria uma conexão com o banco de dados.
        using IDbConnection dbConnection = _dbContext.CreateConnection();

        // Abre a conexão com o banco de dados.
        dbConnection.Open();

        // Define parâmetros para a consulta SQL.
        var parameters = new { Id = id };

        // Executa a consulta SQL para obter um único EmployeeEntity pelo ID.
        var result = await dbConnection.QueryFirstOrDefaultAsync<EmployeeEntity>(EmployeeSqlConsts.SQL_GET_BY_ID, parameters);

        // Verifica se o resultado é nulo.
        if (result == null)
        {
            return null;
        }

        // Mapeia o EmployeeEntity para EmployeeQueryModel.
        var mapper = _mapper.Map<EmployeeQueryModel>(result);

        // Retorna o modelo mapeado.
        return mapper;
    }
}